using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Seance
    {
        [Key]
        public int Id_Seance { get; set; }
        public int Id_Filiere { get; set; }
        public int Id_Salle { get; set; }
        public string Heure_D { get; set; }
        public string Heure_F { get; set; }
        public int Id_Matiere { get; set; }

        [ForeignKey("Id_Filiere")]
        public Filiere filiere { get; set; }

        [ForeignKey("Id_Salle")]
        public Salle salle { get; set; }

        [ForeignKey("Id_Matiere")]
        public Matiere matiere { get; set; }
    }
}
