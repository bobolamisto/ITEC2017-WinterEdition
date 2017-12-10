using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.Data;
using Portal.Models;
using Portal.Services;

namespace Portal.Controllers
{
    public class UsersController : Controller
    {
        private readonly PortalDbContext _context;
        private readonly IEmailService _emailSender;
        public UsersController(PortalDbContext context)
        {
            _context = context;
            _emailSender = new EmailService();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var portalDbContext = _context.User.Include(u => u.Location);
            return View(await portalDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexAdmin()
        {
            var portalDbContext = _context.User.Include(u => u.Location);
            return View(await portalDbContext.ToListAsync());
        }

        public async Task<IActionResult> Statistics()
        {
            return View();
        }



        public async Task<IActionResult> ArchiveIssueAsync(int id)
        {
            var issues = _context.Issues.Include(i => i.Location)
                .Include(i => i.States)
                .FirstOrDefault(i => i.Id == id);
            issues.States.Add(new IssueState { IssueId = issues.Id, Date = System.DateTime.Now, Type = StateType.Archived });
            _context.Update(issues);
            await _context.SaveChangesAsync();
            return RedirectToAction("ActiveIssue");
        }

        public async Task<IActionResult> SolveIssueAsync(int id)
        {
            var issues = _context.Issues.Include(i => i.Location)
                .Include(i => i.States)
                .FirstOrDefault(i => i.Id == id);
            issues.States.Add(new IssueState { IssueId = issues.Id, Date = System.DateTime.Now, Type = StateType.Solved });
            _context.Update(issues);
            await _context.SaveChangesAsync();
            return RedirectToAction("ActiveIssue");
        }
        public IActionResult ActiveIssue()
        {
            var issues = _context.Issues.Include(i => i.Location)
                                                 .Include(i => i.States);
            List<Issue> list = new List<Issue>();
            foreach (var issue in issues)
            {
                var lastState = issue.States.FirstOrDefault();
                foreach (var state in issue.States)
                {
                    if (state.Date > lastState.Date)
                    {
                        lastState = state;
                    }
                }
                if (lastState.Type == StateType.Active)
                {
                    list.Add(issue);
                }
            }
            return View(list);
        }

        public IActionResult ArchivedIssue()
        {
            var issues = _context.Issues.Include(i => i.Location)
                                                 .Include(i => i.States);
            List<Issue> list = new List<Issue>();
            foreach (var issue in issues)
            {
                var lastState = issue.States.FirstOrDefault();
                foreach (var state in issue.States)
                {
                    if (state.Date > lastState.Date)
                    {
                        lastState = state;
                    }
                }
                if (lastState.Type == StateType.Archived)
                {
                    list.Add(issue);
                }
            }
            return View(list);
        }

        public IActionResult SolvedIssue()
        {
            var issues = _context.Issues.Include(i => i.Location)
                                                 .Include(i => i.States);
            List<Issue> list = new List<Issue>();
            foreach (var issue in issues)
            {
                var lastState = issue.States.FirstOrDefault();
                foreach (var state in issue.States)
                {
                    if (state.Date > lastState.Date)
                    {
                        lastState = state;
                    }
                }
                if (lastState.Type == StateType.Solved)
                {
                    list.Add(issue);
                }
            }
            return View(list);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,LocationId,RadiusOfInterest,Age,Gender,Status,IsAdmin,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", user.LocationId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", user.LocationId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,LocationId,RadiusOfInterest,Age,Gender,Status,IsAdmin,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    if(user.Status == UserStatus.Accepted)
                    {
                       _emailSender.SendEmail(user.Email, "Account created", "Your account was accepted by an admin");
                    }
                    else if(user.Status == UserStatus.Rejected)
                    {
                        _emailSender.SendEmail(user.Email, "Account rejected", "Your account was rejected by an admin");

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", user.LocationId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
