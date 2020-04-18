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
    public class SeancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.seances.Include(s => s.filiere).Include(s => s.matiere).Include(s => s.salle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Seances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.seances
                .Include(s => s.filiere)
                .Include(s => s.matiere)
                .Include(s => s.salle)
                .FirstOrDefaultAsync(m => m.Id_Seance == id);
            if (seance == null)
            {
                return NotFound();
            }

            return View(seance);
        }

        // GET: Seances/Create
        public IActionResult Create()
        {
            ViewData["Id_Filiere"] = new SelectList(_context.filieres, "Id_Filiere", "Id_Filiere");
            ViewData["Id_Matiere"] = new SelectList(_context.matieres, "Id_Matiere", "Id_Matiere");
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle");
            return View();
        }

        // POST: Seances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Seance,Id_Filiere,Id_Salle,Heure_D,Heure_F,Id_Matiere")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Filiere"] = new SelectList(_context.filieres, "Id_Filiere", "Id_Filiere", seance.Id_Filiere);
            ViewData["Id_Matiere"] = new SelectList(_context.matieres, "Id_Matiere", "Id_Matiere", seance.Id_Matiere);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", seance.Id_Salle);
            return View(seance);
        }

        // GET: Seances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }
            ViewData["Id_Filiere"] = new SelectList(_context.filieres, "Id_Filiere", "Id_Filiere", seance.Id_Filiere);
            ViewData["Id_Matiere"] = new SelectList(_context.matieres, "Id_Matiere", "Id_Matiere", seance.Id_Matiere);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", seance.Id_Salle);
            return View(seance);
        }

        // POST: Seances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Seance,Id_Filiere,Id_Salle,Heure_D,Heure_F,Id_Matiere")] Seance seance)
        {
            if (id != seance.Id_Seance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeanceExists(seance.Id_Seance))
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
            ViewData["Id_Filiere"] = new SelectList(_context.filieres, "Id_Filiere", "Id_Filiere", seance.Id_Filiere);
            ViewData["Id_Matiere"] = new SelectList(_context.matieres, "Id_Matiere", "Id_Matiere", seance.Id_Matiere);
            ViewData["Id_Salle"] = new SelectList(_context.salles, "Id_Salle", "Id_Salle", seance.Id_Salle);
            return View(seance);
        }

        // GET: Seances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _context.seances
                .Include(s => s.filiere)
                .Include(s => s.matiere)
                .Include(s => s.salle)
                .FirstOrDefaultAsync(m => m.Id_Seance == id);
            if (seance == null)
            {
                return NotFound();
            }

            return View(seance);
        }

        // POST: Seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seance = await _context.seances.FindAsync(id);
            _context.seances.Remove(seance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeanceExists(int id)
        {
            return _context.seances.Any(e => e.Id_Seance == id);
        }
    }
}
