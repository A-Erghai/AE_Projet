using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AE_Projet.Models;

namespace AE_Projet.Controllers
{
    public class PresencesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PresencesController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Presences
        public async Task<IActionResult> IndexPre()
        {
            var applicationDbContext = _db.presences.Include(p => p.salle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presences/Details/5
      /*  public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _db.presences
                .Include(p => p.salle)
                .FirstOrDefaultAsync(m => m.Id_Presence == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }*/

        // GET: Presences/Create
        public IActionResult AjtPre()
        {
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Nom_Salle", "Nom_Salle");
            return View();
        }

        // POST: Presences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtPre(Presence presence)
        {
            if (ModelState.IsValid)
            {
                _db.Add(presence);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexPre");
            }
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Nom_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: Presences/Edit/5
        public async Task<IActionResult> MdfPre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _db.presences.FindAsync(id);
            if (presence == null)
            {
                return NotFound();
            }
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Nom_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // POST: Presences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfPre(int id, Presence presence)
        {
            if (id != presence.Id_Presence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(presence);
                    await _db.SaveChangesAsync();
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
                return RedirectToAction("IndexPre");
            }
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Nom_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: Presences/Delete/5
        public async Task<IActionResult> SuppPre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _db.presences
                .Include(p => p.salle)
                .FirstOrDefaultAsync(m => m.Id_Presence == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // POST: Presences/Delete/5
        [HttpPost, ActionName("SuppPre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppPreConfirmed(int id)
        {
            var presence = await _db.presences.FindAsync(id);
            _db.presences.Remove(presence);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexPre");
        }

        private bool PresenceExists(int id)
        {
            return _db.presences.Any(e => e.Id_Presence == id);
        }
    }
}
