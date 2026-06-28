using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalProject.Models;

public class EnrollmentsController : Controller
{
    private readonly StudentManagementSystemContext _context;

    public EnrollmentsController(StudentManagementSystemContext context)
    {
        _context = context;
    }

    // GET: Enrollments
    public async Task<IActionResult> Index()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();

        return View(enrollments);
    }

    // GET: Enrollments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(m => m.EnrollmentId == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    // GET: Enrollments/Create
    public IActionResult Create()
    {
        ViewData["StudentId"] = new SelectList(
            _context.Students,
            "StudentId",
            "FirstName"
        );

        ViewData["CourseId"] = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName"
        );

        return View();
    }

    // POST: Enrollments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("EnrollmentId,StudentId,CourseId,Semester,Grade")]
        Enrollment enrollment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["StudentId"] = new SelectList(
            _context.Students,
            "StudentId",
            "FirstName",
            enrollment.StudentId
        );

        ViewData["CourseId"] = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            enrollment.CourseId
        );

        return View(enrollment);
    }

    // GET: Enrollments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
        {
            return NotFound();
        }

        ViewData["StudentId"] = new SelectList(
            _context.Students,
            "StudentId",
            "FirstName",
            enrollment.StudentId
        );

        ViewData["CourseId"] = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            enrollment.CourseId
        );

        return View(enrollment);
    }

    // POST: Enrollments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("EnrollmentId,StudentId,CourseId,Semester,Grade")]
        Enrollment enrollment)
    {
        if (id != enrollment.EnrollmentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.EnrollmentId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["StudentId"] = new SelectList(
            _context.Students,
            "StudentId",
            "FirstName",
            enrollment.StudentId
        );

        ViewData["CourseId"] = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            enrollment.CourseId
        );

        return View(enrollment);
    }

    // GET: Enrollments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(m => m.EnrollmentId == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return View(enrollment);
    }

    // POST: Enrollments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool EnrollmentExists(int id)
    {
        return _context.Enrollments.Any(e => e.EnrollmentId == id);
    }
}