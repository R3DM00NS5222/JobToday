using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobToday.Data;
using JobToday.Models;
using Microsoft.AspNetCore.Authorization;

namespace JobToday.Controllers
{
    public class JobPostingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPostings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobPostings.Include(j => j.Company).Include(j => j.Tag);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings
                .Include(j => j.Company)
                .Include(j => j.Tag)
                .FirstOrDefaultAsync(m => m.JobPostingId == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        [Authorize(Roles = "Administrator")]
        // GET: JobPostings/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName");
            return View();
        }

        // POST: JobPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Create([Bind("JobPostingId,TagId,CompanyId,PostedBy,JobPostName,CompanyName,JobPostStatus,JobPay,JobPostDescription")] JobPosting jobPosting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPosting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", jobPosting.CompanyId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", jobPosting.TagId);
            return View(jobPosting);
        }

        // GET: JobPostings/Edit/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", jobPosting.CompanyId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", jobPosting.TagId);
            return View(jobPosting);
        }

        // POST: JobPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("JobPostingId,TagId,CompanyId,PostedBy,JobPostName,CompanyName,JobPostStatus,JobPay,JobPostDescription")] JobPosting jobPosting)
        {
            if (id != jobPosting.JobPostingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPosting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostingExists(jobPosting.JobPostingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", jobPosting.CompanyId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", jobPosting.TagId);
            return View(jobPosting);
        }

        // GET: JobPostings/Delete/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings
                .Include(j => j.Company)
                .Include(j => j.Tag)
                .FirstOrDefaultAsync(m => m.JobPostingId == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobPostings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JobPostings'  is null.");
            }
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting != null)
            {
                _context.JobPostings.Remove(jobPosting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostingExists(int id)
        {
          return _context.JobPostings.Any(e => e.JobPostingId == id);
        }
    }
}
