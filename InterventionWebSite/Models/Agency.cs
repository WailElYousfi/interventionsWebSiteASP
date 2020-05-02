using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class Agency
    {
        [Key]
        public int AgencyId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Nom d'agence")]
        public string AgencyName { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Adresse")]
        public string Address { get; set; }

        public virtual List<Intervention> Interventions { get; set; }
        public virtual List<Staff> Staffs { get; set; }
    }
}