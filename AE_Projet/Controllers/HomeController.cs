using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AE_Projet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AE_Projet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("Home")]
        public IActionResult AjtPre()
        {
           
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd");
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle");
            return View();
        }

        // POST: Presences/Ajt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjtPre([Bind("Id_Presence,Matricule,Date,Id_Salle")] Presence presence)
        {
            if (ModelState.IsValid)
            {
                presence.Date = DateTime.Now;
                _db.Add(presence);
                await _db.SaveChangesAsync();
                return RedirectToAction("AjtPre");
            }
            ViewData["Matricule"] = new SelectList(_db.etudiants, "Matricule", "Nom_Etd", presence.Matricule);
            ViewData["Nom_Salle"] = new SelectList(_db.salles, "Id_Salle", "Nom_Salle", presence.Id_Salle);
            return View(presence);
        }

        
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
