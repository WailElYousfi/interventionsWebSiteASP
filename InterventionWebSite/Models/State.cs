using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Statut")]
        [Index("IMEIIndex", IsUnique = true)]
        [StringLength(60)]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual List<Intervention> interventions { get; set; }
    }
}