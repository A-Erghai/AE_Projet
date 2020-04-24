using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AE_Projet.Models;
using AE_Projet.Migrations;

namespace AE_Projet.Controllers
{
    public class PresencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.presences.Include(p => p.etudiant).Include(p => p.salle).ToListAsync();
            return View(await applicationDbContext);
        }

        // GET: Presences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.presences
                .Include(p => p.etudiant)
                .Include(p => p.salle)
                .FirstOrDefaultAsync(m => m.Id_Presence == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // GET: Presences/Create
        public IActionResult Create()
        {
            ViewData["Matricule"] = new SelectList(_context.etudiants, "Matricule", "Nom_Etd");
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle");
            return View();
        }

        // POST: Presences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Presence,Matricule,Date,Id_Salle")] Presence presence)
        {
            if (ModelState.IsValid)
            {
                presence.Date = DateTime.Now;
                _context.Add(presence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Matricule"] = new SelectList(_context.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: Presences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.presences.FindAsync(id);
            if (presence == null)
            {
                return NotFound();
            }
            ViewData["Matricule"] = new SelectList(_context.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", presence.Id_Salle);
            return View(presence);
        }

        // POST: Presences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Presence,Matricule,Date,Id_Salle")] Presence presence)
        {
            if (id != presence.Id_Presence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenceExists(presence.Id_Presence))
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
            ViewData["Matricule"] = new SelectList(_context.etudiants, "Matricule", "Matricule", presence.Matricule);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: Presences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.presences
                .Include(p => p.etudiant)
                .Include(p => p.salle)
                .FirstOrDefaultAsync(m => m.Id_Presence == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // POST: Presences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presence = await _context.presences.FindAsync(id);
            _context.presences.Remove(presence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenceExists(int id)
        {
            return _context.presences.Any(e => e.Id_Presence == id);
        }
    }
}
