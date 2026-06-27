using System;
using System.Collections.Generic;

namespace finalProject.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public string? Semester { get; set; }

    public string? Grade { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }
}
