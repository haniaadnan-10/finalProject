using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finalProject.Models;

namespace finalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentManagementSystemContext _context;

        public HomeController(StudentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Statistics
            ViewBag.TotalStudents = await _context.Students.CountAsync();
            ViewBag.TotalDepartments = await _context.Departments.CountAsync();
            ViewBag.TotalCourses = await _context.Courses.CountAsync();
            ViewBag.TotalTeachers = await _context.Teachers.CountAsync();
            ViewBag.TotalEnrollments = await _context.Enrollments.CountAsync();

            // Recent Students
            ViewBag.RecentStudents = await _context.Students
                .OrderByDescending(s => s.StudentId)
                .Take(5)
                .ToListAsync();

            // Recent Enrollments
            ViewBag.RecentEnrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .OrderByDescending(e => e.EnrollmentId)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}