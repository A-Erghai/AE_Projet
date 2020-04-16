using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AE_Projet.Models
{
    public class Filiere
    {
        [Key]
        public int Id_Filiere { get; set; }
        public string Nom_Filiere { get; set; }
    }
}
