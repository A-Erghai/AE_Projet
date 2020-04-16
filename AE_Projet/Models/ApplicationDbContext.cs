using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Matiere> matieres { get; set; }
        public DbSet<Professeur> professeurs { get; set; }
        public DbSet<Seance> seances { get; set; }
        public DbSet<Filiere> filieres { get; set; }
        public DbSet<Salle> salles { get; set; }
        public DbSet<Presence> presences { get; set; }
        public DbSet<Etudiant> etudiants { get; set; }
    }
}
