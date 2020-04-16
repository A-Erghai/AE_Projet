using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Admin
    {
        [Key]
        public int Id_Admin { get; set; }
        public string Nom_Admin { get; set; }
        public string Prenom_Admin { get; set; }
        public string CIN_Admin { get; set; }
        public string Email_Admin { get; set; }
        public string Mdp { get; set; }
    }
}
