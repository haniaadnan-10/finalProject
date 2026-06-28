using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalProject.Models;

public class TeachersController : Controller
{
    private readonly StudentManagementSystemContext _context;

    public TeachersController(StudentManagementSystemContext context)
    {
        _context = context;
    }

    // GET: TEACHERS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Teachers
            .Include(t => t.Department)
            .ToListAsync());
    }

    // GET: TEACHERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var teacher = await _context.Teachers
            .Include(t => t.Department)
            .FirstOrDefaultAsync(t => t.TeacherId == id);

        if (teacher == null)
            return NotFound();

        return View(teacher);
    }

    // GET: TEACHERS/Create
    public IActionResult Create()
    {
        ViewBag.DepartmentId = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName"
        );

        return View();
    }

    // POST: TEACHERS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("TeacherId,TeacherName,Email,DepartmentId")]
        Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.DepartmentId = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            teacher.DepartmentId
        );

        return View(teacher);
    }

    // GET: TEACHERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher == null)
            return NotFound();

        ViewBag.DepartmentId = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            teacher.DepartmentId
        );

        return View(teacher);
    }

    // POST: TEACHERS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("TeacherId,TeacherName,Email,DepartmentId")]
        Teacher teacher)
    {
        if (id != teacher.TeacherId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(teacher.TeacherId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.DepartmentId = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            teacher.DepartmentId
        );

        return View(teacher);
    }

    // GET: TEACHERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var teacher = await _context.Teachers
            .Include(t => t.Department)
            .FirstOrDefaultAsync(t => t.TeacherId == id);

        if (teacher == null)
            return NotFound();

        return View(teacher);
    }

    // POST: TEACHERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher != null)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool TeacherExists(int? id)
    {
        return _context.Teachers.Any(e => e.TeacherId == id);
    }
}