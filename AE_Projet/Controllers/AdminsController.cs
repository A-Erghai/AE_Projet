using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AE_Projet.Models;
using Microsoft.AspNetCore.Http;

namespace AE_Projet.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") != a.Email_Admin)
            {
                return View("Login");
            }
            
            return View();
        }
        public IActionResult Login()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(Admin a)
        {
            Admin ad = _db.admins.Where(x => x.Email_Admin == a.Email_Admin && x.Mdp == a.Mdp).SingleOrDefault();
            if (ad!= null)
            {
                HttpContext.Session.SetString("username", a.Email_Admin);
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

        // GET: Admins
        public async Task<IActionResult> IndexAdmin(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            
            return View(await _db.admins.ToListAsync());
            
        }

        [HttpGet]
        // GET: Admins/Create
        public IActionResult AjouterAdmin(int id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View();
        }

        // POST: Admins/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterAdmin([Bind("Id_Admin,Nom_Admin,Prenom_Admin,CIN_Admin,Email_Admin,Mdp")] Admin admin)
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
        public async Task<IActionResult> ModifierAdmin(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }

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
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierAdmin(int id, [Bind("Id_Admin,Nom_Admin,Prenom_Admin,CIN_Admin,Email_Admin,Mdp")] Admin admin)
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
        public async Task<IActionResult> SuppAdmin(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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
        public async Task<IActionResult> IndexF(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View("Login");
            }
            return View(await _db.filieres.ToListAsync());
        }


        // GET: Filieres/Create
        public IActionResult AjtF(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View();
        }

        // POST: Filieres/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtF([Bind("Id_Filiere,Nom_Filiere")] Filiere filiere)
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
        public async Task<IActionResult> MdfF(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfF(int id, [Bind("Id_Filiere,Nom_Filiere")] Filiere filiere)
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
        public async Task<IActionResult> SuppF(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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
        public async Task<IActionResult> IndexEtd(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null )
            {
                return View("Login");
            }
            var applicationDbContext = _db.etudiants.Include(e => e.filiere);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Etudiants/Create
        public IActionResult AjouterEtd(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") == a.Email_Admin)
            {
                return View("Login");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterEtd([Bind("Matricule,Nom_Etd,Prenom_Etd,CIN_Etd,Id_Filiere")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                _db.Add(etudiant);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexEtd");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // GET:
        public async Task<IActionResult> ModifierEtd(string id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _db.etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierEtd(string id, [Bind("Matricule,Nom_Etd,Prenom_Etd,CIN_Etd,Id_Filiere")] Etudiant etudiant)
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
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", etudiant.Id_Filiere);
            return View(etudiant);
        }

        // GET:
        public async Task<IActionResult> SuppEtd(string id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
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

        // GET:
        public async Task<IActionResult> IndexProf(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View(await _db.professeurs.ToListAsync());
        }


        // GET: 
        public IActionResult AjtProf(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View();
        }

        // POST: 
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

        // GET: 
        public async Task<IActionResult> MdfProf(int? id, Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfProf(int id, [Bind("Id_Prof,Nom_Prof,Prenom_Prof,CIN_Prof,Email_Prof,Mdp")] Professeur professeur)
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

        // GET: 
        public async Task<IActionResult> SuppProf(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
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

        // GET: 
        public async Task<IActionResult> IndexMat(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            var applicationDbContext = _db.matieres.Include(m => m.professeur);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET:
        public IActionResult AjtMat(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Id_Prof", "Nom_Prof");
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtMat([Bind("Id_Matiere,Nom_Matiere,Id_Prof")] Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                _db.Add(matiere);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexMat");
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Id_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // GET: 
        public async Task<IActionResult> MdfMat(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _db.matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Id_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfMat(int id, [Bind("Id_Matiere,Nom_Matiere,Id_Prof")] Matiere matiere)
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
            ViewData["Nom_Prof"] = new SelectList(_db.professeurs, "Id_Prof", "Nom_Prof", matiere.Id_Prof);
            return View(matiere);
        }

        // GET: 
        public async Task<IActionResult> SuppMat(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
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
        // GET: 
        public async Task<IActionResult> IndexSa(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View(await _db.salles.ToListAsync());
        }



        // GET: 
        public IActionResult AjtSa(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            return View();
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtSa([Bind("Id_Salle,Nom_Salle")] Salle salle)
        {
            if (ModelState.IsValid)
            {
                _db.Add(salle);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexSa");
            }
            return View(salle);
        }

        // GET: 
        public async Task<IActionResult> MdfSa(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfSa(int id, [Bind("Id_Salle,Nom_Salle")]  Salle salle)
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

        // GET:
        public async Task<IActionResult> SuppSa(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
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

        // GET: 
        public async Task<IActionResult> IndexPre(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*/*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            var applicationDbContext = _db.presences.Include(p => p.etudiant).Include(p => p.salle);
            return View(await applicationDbContext.ToListAsync());
        }

       

        // GET: 
        public IActionResult AjtPre(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd");
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle");
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtPre([Bind("Id_Presence,Matricule,Date,Id_Salle")] Presence presence)
        {
            if (ModelState.IsValid)
            {
                presence.Date = DateTime.Now;
                _db.Add(presence);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexPre");
            }
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: 
        public async Task<IActionResult> MdfPre(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _db.presences.FindAsync(id);
            if (presence == null)
            {
                return NotFound();
            }
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfPre(int id, [Bind("Id_Presence,Matricule,Date,Id_Salle")] Presence presence)
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
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        // GET: 
        public async Task<IActionResult> SuppPre(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*/*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
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

        // POST: 
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

        // GET: Seances
        public async Task<IActionResult> IndexSea(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            var applicationDbContext = _db.seances.Include(s => s.filiere).Include(s => s.matiere).Include(s => s.salle);
            return View(await applicationDbContext.ToListAsync());
        }

        
        // GET: 
        public IActionResult AjtSea(Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere");
            ViewData["Nom_Matiere"] = new SelectList(_db.matieres, "Id_Matiere", "Nom_Matiere");
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle");
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtSea([Bind("Id_Seance,Id_Filiere,Id_Salle,Heure_D,Heure_F,Id_Matiere")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                _db.Add(seance);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexSea");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", seance.Id_Filiere);
            ViewData["Nom_Matiere"] = new SelectList(_db.matieres, "Id_Matiere", "Nom_Matiere", seance.Id_Matiere);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", seance.Id_Salle);
            return View(seance);
        }

        // GET: 
        public async Task<IActionResult> MdfSea(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _db.seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", seance.Id_Filiere);
            ViewData["Nom_Matiere"] = new SelectList(_db.matieres, "Id_Matiere", "Nom_Matiere", seance.Id_Matiere);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", seance.Id_Salle);
            return View(seance);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MdfSea(int id, [Bind("Id_Seance,Id_Filiere,Id_Salle,Heure_D,Heure_F,Id_Matiere")] Seance seance)
        {
            if (id != seance.Id_Seance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(seance);
                    await _db.SaveChangesAsync();
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
                return RedirectToAction("IndexSea");
            }
            ViewData["Nom_Filiere"] = new SelectList(_db.filieres, "Id_Filiere", "Nom_Filiere", seance.Id_Filiere);
            ViewData["Nom_Matiere"] = new SelectList(_db.matieres, "Id_Matiere", "Nom_Matiere", seance.Id_Matiere);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", seance.Id_Salle);
            return View(seance);
        }

        // GET: 
        public async Task<IActionResult> SuppSea(int? id,Admin a)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != a.Email_Admin*/)
            {
                return View("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var seance = await _db.seances
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

        // POST: 
        [HttpPost, ActionName("SuppSea")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuppSeaConfirmed(int id)
        {
            var seance = await _db.seances.FindAsync(id);
            _db.seances.Remove(seance);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexSea");
        }

        private bool SeanceExists(int id)
        {
            return _db.seances.Any(e => e.Id_Seance == id);
        }
        public ActionResult PreSea(int? id)
        {
            var nbrall = (from p in _db.presences join e in _db.etudiants on p.Matricule equals e.Matricule where e.Id_Filiere == id select p).Count();
            var nbretd = (from e in _db.etudiants
                          join f in _db.filieres on e.Id_Filiere equals f.Id_Filiere
                          where e.Id_Filiere == id
                          select e).Count();
            var tauxAbsencer = (from etudiant in _db.etudiants
                                join pres in _db.presences on etudiant.Matricule equals pres.Matricule
                                where etudiant.Id_Filiere == id
                                select new tauxAbsclasse
                                {
                                    nbr_pre = nbrall,
                                    nbr_etd = nbretd,
                                    Matricule = etudiant.Nom_Etd,
                                    Taux = (nbrall * 100) / nbretd
                                }
             );
            return View(tauxAbsencer.First());
        }
        [Route("Admins/EtdPre/{Matricule}")]
        [HttpGet]
        public ActionResult EtdPreA(string Matricule)
        {


            var nbrPreEtd = (from e in _db.etudiants
                             join p in _db.presences on e.Matricule equals p.Matricule
                             join s in _db.salles on p.Id_Salle equals s.Id_Salle
                             where e.Matricule == Matricule
                             select e).Count();

            var nbrallSea = (from s in _db.seances join e in _db.etudiants on s.Id_Filiere equals e.Id_Filiere where e.Matricule == Matricule select s).Count();
            var SeaPreEtd = (from p in _db.presences
                             join e in _db.etudiants on p.Matricule equals e.Matricule
                             where e.Matricule == Matricule
                             select new nombreEtudiantAbsnce
                             {
                                 nomEtudiant = e.Nom_Etd,
                                 prenomEtudiant = e.Prenom_Etd,
                                 nombre = nbrallSea - nbrPreEtd,
                                 nbrallSea = nbrallSea,
                                 Taux = ((nbrallSea - nbrPreEtd) * 100) / nbrallSea
                             }
            );
            if(SeaPreEtd == null)
            {
                return View();
            }
            return View(SeaPreEtd.First());
        }
    }

}
