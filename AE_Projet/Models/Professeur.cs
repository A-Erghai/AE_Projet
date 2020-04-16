using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Professeur
    {
        [Key]
        public int Id_Prof { get; set; }
        public string Nom_Prof { get; set; }
        public string Prenom_Prof { get; set; }
        public string CIN_Prof { get; set; }
        public string Email_Prof { get; set; }
        public string Mdp { get; set; }
    }
}
