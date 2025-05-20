//using Xunit;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Backend.Controllers;
//using Backend.Models; // Replace with actual namespace
//using Microsoft.AspNetCore.Mvc;
//using System;

//public class EmployeeTests
//{
//    private ChinookContext GetInMemoryDbContext()
//    {
//        var options = new DbContextOptionsBuilder<ChinookContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensures a fresh DB for each test
//            .Options;

//        var context = new ChinookContext(options);
//        context.Database.EnsureCreated();
//        return context;
//    }

//    [Fact]
//    public async Task Test_GetEmployeeById_ReturnsCorrectEmployee()
//    {
//        // Arrange
//        var context = GetInMemoryDbContext();
//        context.Employees.Add(new Employee { EmployeeId = 1, FirstName = "John" });
//        await context.SaveChangesAsync();

//        // Act
//        var employee = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == 1);

//        // Assert
//        Assert.Equal("John", employee.FirstName);
//    }
//}
