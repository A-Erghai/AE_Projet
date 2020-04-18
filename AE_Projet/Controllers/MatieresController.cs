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
    public class MatieresController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MatieresController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Matieres
        public async Task<IActionResult> IndexMat()
        {
            var applicationDbContext = _db.matieres.Include(m => m.professeur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matieres/Details/5
      /*  public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _db.matieres
                .Include(m => m.professeur)
                .FirstOrDefaultAsync(m => m.Id_Matiere == id);
            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }*/

        // GET: Matieres/Create
        public IActionResult AjtMat()
        {
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Nom_Prof", "Nom_Prof");
            return View();
        }

        // POST: Matieres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtMat( Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                _db.Add(matiere);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexMat");
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Nom_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // GET: Matieres/Edit/5
        public async Task<IActionResult> MdfMAt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _db.matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Nom_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // POST: Matieres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfMAt(int id,Matiere matiere)
        {
            if (id != matiere.Id_Matiere)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(matiere);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatiereExists(matiere.Id_Matiere))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexMat");
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Nom_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // GET: Matieres/Delete/5
        public async Task<IActionResult> SuppMat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _db.matieres
                .Include(m => m.professeur)
                .FirstOrDefaultAsync(m => m.Id_Matiere == id);
            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }

        // POST: Matieres/Delete/5
        [HttpPost, ActionName("SuppMat")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppMatConfirmed(int id)
        {
            var matiere = await _db.matieres.FindAsync(id);
            _db.matieres.Remove(matiere);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexMat");
        }

        private bool MatiereExists(int id)
        {
            return _db.matieres.Any(e => e.Id_Matiere == id);
        }
    }
}
