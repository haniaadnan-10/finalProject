using System;
using System.Collections.Generic;

namespace finalProject.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? TeacherName { get; set; }

    public string? Email { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
