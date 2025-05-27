using System.Globalization;
using System.Text.Json;
using Backend.ML_Models;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

public static class ForecastModel
{
    public static string ForecastTopCountrySpending()
    {
        return RunForecast(
            filePath: Path.Combine(AppContext.BaseDirectory, "ML Preprocessed Data", "top_country_spending_by_month_EXPANDED.csv"),
            itemLabel: "Country",
            outputFileName: "country_forecast.json"
        );
    }

    public static string ForecastTopGenres()
    {
        return RunForecast(
            filePath: Path.Combine(AppContext.BaseDirectory, "ML Preprocessed Data", "top_genres_by_month_EXPANDED.csv"),
            itemLabel: "Genre",
            outputFileName: "genre_forecast.json"
        );
    }

    public static string RunForecast(string filePath, string itemLabel, string outputFileName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Data file not found: {filePath}");

        var lines = File.ReadAllLines(filePath).Skip(1);
        var allData = lines
            .Select(line =>
            {
                var parts = line.Split(',');
                if (parts.Length < 3) return null;

                if (!DateTime.TryParseExact(parts[1].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    return null;

                if (!float.TryParse(parts[2].Trim(), out var value))
                    return null;

                return new ForecastInput
                {
                    Name = parts[0].Trim(),
                    Date = date,
                    Value = value
                };
            })
            .Where(r => r != null)
            .ToList()!;

        var grouped = allData.GroupBy(r => r.Name);
        var mlContext = new MLContext();
        var allForecasts = new List<object>();

        foreach (var group in grouped)
        {
            var name = group.Key!;
            var dataList = group.OrderBy(r => r.Date).Select(r => new TimeSeriesInput
            {
                Date = r.Date,
                Value = r.Value
            }).ToList();

            if (dataList.Count < 6)
                continue;

            var dataView = mlContext.Data.LoadFromEnumerable(dataList);
            var pipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: nameof(ForecastOutput.ForecastedValue),
                inputColumnName: nameof(TimeSeriesInput.Value),
                windowSize: 3,
                seriesLength: 6,
                trainSize: dataList.Count,
                horizon: 3);

            try
            {
                var model = pipeline.Fit(dataView);
                var forecastEngine = model.CreateTimeSeriesEngine<TimeSeriesInput, ForecastOutput>(mlContext);
                var forecast = forecastEngine.Predict();

                var lastKnown = dataList.TakeLast(3).ToList();
                var predictions = new List<(string Month, float Value)>();
                for (int i = 0; i < forecast.ForecastedValue!.Length; i++)
                {
                    var futureDate = dataList.Last().Date.AddMonths(i + 1);
                    predictions.Add((futureDate.ToString("yyyy-MM"), forecast.ForecastedValue[i]));
                }

                var last3Avg = lastKnown.Average(r => r.Value);
                var forecast3Avg = predictions.Average(p => p.Value);

                var result = itemLabel switch
                {
                    "Country" => new
                    {
                        CountryName = name,
                        Last3MonthsAverage = Math.Round(last3Avg, 2),
                        Forecast3MonthsAverage = Math.Round(forecast3Avg, 2)
                    } as object,
                    "Genre" => new
                    {
                        GenreName = name,
                        Last3MonthsAverage = Math.Round(last3Avg, 2),
                        Forecast3MonthsAverage = Math.Round(forecast3Avg, 2)
                    } as object,
                    _ => null
                };

                if (result != null)
                    allForecasts.Add(result);
            }
            catch
            {
                continue; // skip on error
            }
        }

        if (allForecasts.Count == 0)
            throw new Exception($"No forecasts generated for {itemLabel}(s).");

        string outputFolder = Path.Combine(AppContext.BaseDirectory, "ML Results");
        Directory.CreateDirectory(outputFolder);

        string combinedOutputPath = Path.Combine(outputFolder, outputFileName);
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonContent = JsonSerializer.Serialize(allForecasts, jsonOptions);
        File.WriteAllText(combinedOutputPath, jsonContent);

        return combinedOutputPath;
    }
}
