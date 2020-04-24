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

        [Display(Name ="Date")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? Date { get; set; }

        public int Id_Salle { get; set; }

        [ForeignKey("Matricule")]
        public Etudiant etudiant { get; set; }

        [ForeignKey("Id_Salle")]
        public Salle salle { get; set; }
       

    }
}
