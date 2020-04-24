using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AE_Projet.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.Threading;

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
        public async Task<IActionResult> IndexProf(int id)
        {

            
            var professeur = await _db.professeurs.Where(x => x.Id_Prof == id).ToListAsync();
            return View(professeur);
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Login()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(Professeur p)
        {
            Professeur prof = _db.professeurs.Where(x => x.Email_Prof == p.Email_Prof && x.Mdp == p.Mdp).SingleOrDefault();
            if (prof != null)
            {
                HttpContext.Session.SetString("username", p.Email_Prof);
                return View("Index");
            }
            else
            {
                ViewBag.error = "Invalid Account";

            }
            return View();
        }

        // GET: Admins
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        // GET: Professeurs/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Professeurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Prof,Nom_Prof,Prenom_Prof,CIN_Prof,Email_Prof,Mdp")] Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                _db.Add(professeur);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professeur);
        }

        // GET: Professeurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id_Prof,Nom_Prof,Prenom_Prof,CIN_Prof,Email_Prof,Mdp")] Professeur professeur)
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
                return RedirectToAction(nameof(Index));
            }
            return View(professeur);
        }

        // GET: Professeurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professeur = await _db.professeurs.FindAsync(id);
            _db.professeurs.Remove(professeur);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesseurExists(int id)
        {
            return _db.professeurs.Any(e => e.Id_Prof == id);
        }
        public async Task<IActionResult> IndexMat(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View("Login");
            }
            var matiere = await _db.matieres.Include(m => m.professeur).Where(x => x.Id_Prof == id).ToListAsync();
            return View(matiere);
        }
        public async Task<IActionResult> IndexSea(int id)
        {
            var seance = await _db.seances.Include(s => s.filiere).Include(s => s.matiere).Include(s => s.salle).Where(x => x.Id_Matiere == id).ToListAsync();
            return View( seance);
        }
        public async Task<IActionResult> IndexEtd(int id)
        {
            var etudiant = await _db.etudiants.Include(e => e.filiere).Where(x => x.Id_Filiere == id).ToListAsync();
            return View( etudiant);
        }
        public async Task<IActionResult> IndexPre(int id)
        {
            var presence = await _db.presences.Include(p => p.salle).Where(x => x.Id_Salle == id).ToListAsync();
            return View( presence);
        }
        public async Task<IActionResult> IndexTauxV(int id)
        {
            var nbrPr = from p in _db.presences group p by p.Id_Presence into g select new { presences = g.Count() };
            //var nbrEtd = from p in _db.etudiants group p by p.Matricule into g select new { Matr = g.Count() };
            //var taux = nbrEtd / nbrEtd;
            return View(nbrPr.ToListAsync());
        }

    }
}
