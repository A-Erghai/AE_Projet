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
using Microsoft.Data.SqlClient;
using SQLitePCL;

namespace AE_Projet.Controllers
{
    public class ProfesseursController : Controller
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Etudiant> etudiants { get; set; }
        public IEnumerable<Seance> seances { get; set; }

        public ProfesseursController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Professeurs
        
        
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
                return RedirectToAction("IndexMat","Professeurs",new { email = p.Email_Prof });
            }
            else
            {
                ViewBag.error = "Invalid Account";

            }
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }
        // GET: Admins
        
        private bool ProfesseurExists(int id)
        {
            return _db.professeurs.Any(e => e.Id_Prof == id);
        }
        [Route("Professeurs")]
        public async Task<IActionResult> IndexMat(string email, Professeur p)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != p.Email_Prof*/)
            {
                return View("Login");
            }
            var matiere = await _db.matieres.Include(m => m.professeur).Where(x => x.professeur.Email_Prof == email).ToListAsync();
            return View(matiere);
        }
        public async Task<IActionResult> IndexSea(int id,Professeur p)
        {
            if (HttpContext.Session.GetString("username") == null )
            {
                return View("Login");
            }
            var seance = await _db.seances.Include(s => s.filiere).Include(s => s.matiere).Include(s => s.salle).Where(x => x.Id_Matiere == id).ToListAsync();
            return View( seance);
        }
        public async Task<IActionResult> IndexEtd(int id)
        {
            if (HttpContext.Session.GetString("username") == null /*|| HttpContext.Session.GetString("username") != p.Email_Prof*/)
            {
                return View("Login");
            }
            var etudiant = await _db.etudiants.Include(e => e.filiere).Where(x => x.Id_Filiere == id).ToListAsync();
            return View( etudiant);
        }
        public async Task<IActionResult> IndexPre(int id,Professeur p)
        {
            if (HttpContext.Session.GetString("username") == null || HttpContext.Session.GetString("username") != p.Email_Prof)
            {
                return View("Login");
            }
            var presence = await _db.presences.Include(p => p.salle).Where(x => x.Id_Salle == id).ToListAsync();
            return View( presence);
        }
        
        public ActionResult PreSea(int? id)
        {
            var nbrall = (from p in _db.presences join e in _db.etudiants on p.Matricule equals e.Matricule where e.Id_Filiere == id select e).Count();
            var nbretd = (from e in _db.etudiants
                          join f in _db.filieres on e.Id_Filiere equals f.Id_Filiere
                          where e.Id_Filiere == id
                          select e).Count();
            var tauxAbsencer = (from etudiant in _db.etudiants
                                join pres in _db.presences on etudiant.Matricule equals pres.Matricule
                                where etudiant.Id_Filiere == id
                                select new tauxAbsclasse
                                {
                                    nbr_pre = nbrall-nbretd, 
                                    nbr_etd = nbretd,
                                    Matricule = etudiant.Nom_Etd,
                                    Taux = (nbretd * 100) / nbrall - nbretd
                                }
             );
            return View(tauxAbsencer.First());
        }
        [Route("Professeurs/EtdPre/{Matricule}")]
        [HttpGet]
        public ActionResult EtdPre(string Matricule)
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
            return View(SeaPreEtd.First());
        }
    }
}
