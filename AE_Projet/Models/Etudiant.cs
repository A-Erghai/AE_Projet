using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Etudiant
    {
        [Key]
        public string Matricule { get; set; }
        public string Nom_Etd { get; set; }
        public string Prenom_Etd { get; set; }
        public string CIN_Etd { get; set; }
        public int Id_Filiere { get; set; }

        [ForeignKey("Id_Filiere")]
        public Filiere filiere { get; set; }
    }
}
