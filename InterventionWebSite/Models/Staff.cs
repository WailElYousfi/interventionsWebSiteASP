using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Index("IMEIIndex4", IsUnique = true)]
        [StringLength(50)]
        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string Phone { get; set; }

        public int? AgencyId { get; set; }
        public int? DirectionId { get; set; }

        [ForeignKey("AgencyId")]
        public virtual Agency Agency { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        [NotMapped] // it is not a column in Staffs table. 
        public List<Agency> AgencyCollection { get; set; }
        [NotMapped] // it is not a column in Staffs table. 
        public List<Direction> DirectionCollection { get; set; }

        public virtual List<Request> Requests { get; set; }

    }
}