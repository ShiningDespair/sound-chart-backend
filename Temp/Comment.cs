using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int EmployeeId { get; set; }

    public string Comment1 { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
