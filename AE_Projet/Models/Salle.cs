using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Salle
    {

        [Key]
        public int Id_Salle { get; set; }
        public string Nom_Salle { get; set; }

        
    }
}
