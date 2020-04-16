using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Presence
    {
        [Key]
        public int Id_Presence { get; set; }
        public string Matricule { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }

        public int Id_Salle { get; set; }

        [ForeignKey("Id_Salle")]
        public Salle salle { get; set; }
    }
}
