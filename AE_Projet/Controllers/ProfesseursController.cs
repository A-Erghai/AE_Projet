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
    public class ProfesseursController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProfesseursController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Professeurs
        public async Task<IActionResult> IndexProf()
        {
            return View(await _db.professeurs.ToListAsync());
        }

        
        // GET: Professeurs/Create
        public IActionResult AjtProf()
        {
            return View();
        }

        // POST: Professeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtProf([Bind("Id_Prof,Nom_Prof,Prenom_Prof,CIN_Prof,Email_Prof,Mdp")] Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                _db.Add(professeur);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexProf");
            }
            return View(professeur);
        }

        // GET: Professeurs/Edit/5
        public async Task<IActionResult> MdfProf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _db.professeurs.FindAsync(id);
            if (professeur == null)
            {
                return NotFound();
            }
            return View(professeur);
        }

        // POST: Professeurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfProf(int id, Professeur professeur)
        {
            if (id != professeur.Id_Prof)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(professeur);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesseurExists(professeur.Id_Prof))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexProf");
            }
            return View(professeur);
        }

        // GET: Professeurs/Delete/5
        public async Task<IActionResult> SuppProf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _db.professeurs
                .FirstOrDefaultAsync(m => m.Id_Prof == id);
            if (professeur == null)
            {
                return NotFound();
            }

            return View(professeur);
        }

        // POST: Professeurs/Delete/5
        [HttpPost, ActionName("SuppProf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppProfConfirmed(int id)
        {
            var professeur = await _db.professeurs.FindAsync(id);
            _db.professeurs.Remove(professeur);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexProf");
        }

        private bool ProfesseurExists(int id)
        {
            return _db.professeurs.Any(e => e.Id_Prof == id);
        }
    }
}
