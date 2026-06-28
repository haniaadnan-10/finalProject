using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalProject.Models;

public class StudentsController : Controller
{
    private readonly StudentManagementSystemContext _context;

    public StudentsController(StudentManagementSystemContext context)
    {
        _context = context;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .Include(s => s.Department)
            .ToListAsync();

        return View(students);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        ViewData["DepartmentId"] = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName"
        );

        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("StudentId,FirstName,LastName,Gender,DateOfBirth,Email,Phone,Address,DepartmentId")]
        Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["DepartmentId"] = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            student.DepartmentId
        );

        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        ViewData["DepartmentId"] = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            student.DepartmentId
        );

        return View(student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("StudentId,FirstName,LastName,Gender,DateOfBirth,Email,Phone,Address,DepartmentId")]
        Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["DepartmentId"] = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            student.DepartmentId
        );

        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.StudentId == id);
    }
}s