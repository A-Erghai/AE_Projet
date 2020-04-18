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
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admins
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _db.admins.ToListAsync());
        }

        
        // GET: Admins/Create
        public IActionResult AjouterAdmin()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _db.Add(admin);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexAdmin");
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> ModifierAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _db.admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierAdmin(int id, Admin admin)
        {
            if (id != admin.Id_Admin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(admin);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id_Admin))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexAdmin");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> SuppAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _db.admins
                .FirstOrDefaultAsync(m => m.Id_Admin == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("SuppAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppAdminConfirmed(int id)
        {
            var admin = await _db.admins.FindAsync(id);
            _db.admins.Remove(admin);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexAdmin");
        }

        private bool AdminExists(int id)
        {
            return _db.admins.Any(e => e.Id_Admin == id);
        }
        // GET: Filieres
        public async Task<IActionResult> IndexF()
        {
            return View(await _db.filieres.ToListAsync());
        }


        // GET: Filieres/Create
        public IActionResult AjtF()
        {
            return View();
        }

        // POST: Filieres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtF(Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                _db.Add(filiere);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexF");
            }
            return View(filiere);
        }

        // GET: Filieres/Edit/5
        public async Task<IActionResult> MdfF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _db.filieres.FindAsync(id);
            if (filiere == null)
            {
                return NotFound();
            }
            return View(filiere);
        }

        // POST: Filieres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfF(int id, Filiere filiere)
        {
            if (id != filiere.Id_Filiere)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(filiere);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiliereExists(filiere.Id_Filiere))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexF");
            }
            return View(filiere);
        }

        // GET: Filieres/Delete/5
        public async Task<IActionResult> SuppF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _db.filieres
                .FirstOrDefaultAsync(m => m.Id_Filiere == id);
            if (filiere == null)
            {
                return NotFound();
            }

            return View(filiere);
        }

        // POST: Filieres/Delete/5
        [HttpPost, ActionName("SuppF")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppFConfirmed(int id)
        {
            var filiere = await _db.filieres.FindAsync(id);
            _db.filieres.Remove(filiere);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexF");
        }

        private bool FiliereExists(int id)
        {
            return _db.filieres.Any(e => e.Id_Filiere == id);
        }
        // GET: Etudiants
        public async Task<IActionResult> IndexEtd()
        {
            var applicationDbContext = _db.etudiants.Include(e => e.filiere);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Etudiants/Create
        public IActionResult AjouterEtd()
        {
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Nom_Filiere", "Nom_Filiere");
            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterEtd( Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                _db.Add(etudiant);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexEtd");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Nom_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public async Task<IActionResult> ModifierEtd(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _db.etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Nom_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierEtd(string id, Etudiant etudiant)
        {
            if (id != etudiant.Matricule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(etudiant);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtudiantExists(etudiant.Matricule))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexEtd");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Nom_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // GET: Etudiants/Delete/5
        public async Task<IActionResult> SuppEtd(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _db.etudiants
                .Include(e => e.filiere)
                .FirstOrDefaultAsync(m => m.Matricule == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("SuppEtd")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppEtdConfirmed(string id)
        {
            var etudiant = await _db.etudiants.FindAsync(id);
            _db.etudiants.Remove(etudiant);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexEtd");
        }

        private bool EtudiantExists(string id)
        {
            return _db.etudiants.Any(e => e.Matricule == id);
        }
    }
}
