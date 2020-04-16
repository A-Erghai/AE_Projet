using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Matiere
    {
        [Key]
        public int Id_Matiere { get; set; }
        public string Nom_Matiere { get; set; }
        public int Id_Prof { get; set; }

        [ForeignKey("Id_Prof")]
        public Professeur professeur { get; set; }
    }
}
