using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.Data;
using Portal.Models;

namespace Portal.Controllers
{
    public class IssuesController : Controller
    {
        private readonly PortalDbContext _context;

        public IssuesController(PortalDbContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var issues = _context.Issues.Include(i => i.Location)
                                                 .Include(i => i.States);
            List<Issue> list = new List<Issue>();
            foreach(var issue in issues)
            {
                var lastState = issue.States.FirstOrDefault();
                foreach(var state in issue.States)
                {
                    if(state.Date > lastState.Date)
                    {
                        lastState = state;
                    }
                }
                if(lastState.Type == StateType.Active)
                {
                    list.Add(issue);
                }
            }
            return View(list);
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocationId,Title,Description,Location")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                var location = _context.Locations.FirstOrDefault(l => l.Latitude == issue.Location.Latitude && l.Longitude == issue.Location.Longitude);
                if (location == null)
                {
                    _context.Locations.Add(new Location { Latitude = issue.Location.Latitude, Longitude = issue.Location.Longitude });
                    _context.SaveChanges();
                    location = _context.Locations.FirstOrDefault(l => l.Latitude == issue.Location.Latitude && l.Longitude == issue.Location.Longitude);
                }
                issue.LocationId = location.Id;
                _context.Add(issue);
                _context.SaveChanges();
                var addedIssue = _context.Issues.FirstOrDefault(i => i.Title == issue.Title && i.Description == issue.Description && i.LocationId == issue.LocationId);

                var issueState = new IssueState { Date = System.DateTime.Now, IssueId = addedIssue.Id, Type = StateType.Active };
                _context.Add(issueState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", issue.LocationId);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", issue.LocationId);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationId,Title,Description,Location")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", issue.LocationId);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.SingleOrDefaultAsync(m => m.Id == id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
