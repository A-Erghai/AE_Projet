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
    public class SallesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SallesController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Salles
        public async Task<IActionResult> IndexSa()
        {
            return View(await _db.salles.ToListAsync());
        }

       

        // GET: Salles/Create
        public IActionResult AjtSa()
        {
            return View();
        }

        // POST: Salles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtSa(Salle salle)
        {
            if (ModelState.IsValid)
            {
                _db.Add(salle);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexSa");
            }
            return View(salle);
        }

        // GET: Salles/Edit/5
        public async Task<IActionResult> MdfSa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salle = await _db.salles.FindAsync(id);
            if (salle == null)
            {
                return NotFound();
            }
            return View(salle);
        }

        // POST: Salles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfSa(int id,Salle salle)
        {
            if (id != salle.Id_Salle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(salle);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalleExists(salle.Id_Salle))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexSa");
            }
            return View(salle);
        }

        // GET: Salles/Delete/5
        public async Task<IActionResult> SuppSa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salle = await _db.salles
                .FirstOrDefaultAsync(m => m.Id_Salle == id);
            if (salle == null)
            {
                return NotFound();
            }

            return View(salle);
        }

        // POST: Salles/Delete/5
        [HttpPost, ActionName("SuppSa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppSaConfirmed(int id)
        {
            var salle = await _db.salles.FindAsync(id);
            _db.salles.Remove(salle);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexSa");
        }

        private bool SalleExists(int id)
        {
            return _db.salles.Any(e => e.Id_Salle == id);
        }
    }
}
