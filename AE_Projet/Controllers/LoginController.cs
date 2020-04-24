using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AE_Projet.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AE_Projet.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Id_Admin") == null)
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
            if (a.Email_Admin != null && a.Mdp != null)
            {
                HttpContext.Session.SetString("username", a.Email_Admin);
                return View("AjouterAdmin");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }
        public IActionResult AjouterAdmin()
        {
            if (HttpContext.Session.GetString("Id_Admin") == null)
            {
                return View("Login");
            }
            return View();
        }
       
        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
 }
