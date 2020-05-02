using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class Direction
    {
        [Key]
        public int DirectionId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Nom de direction")]
        public string DirectionName { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual List<Intervention> Interventions { get; set; }
        public virtual List<Staff> Staffs { get; set; }
    }
}