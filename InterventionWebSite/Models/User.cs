using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionWebSite.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Nom")]
        public string Lastname { get; set; }

//        [Index("IMEIIndex", IsUnique = true)]
        [StringLength(50)]
        [Remote("IsEmailExist", "Users", AdditionalFields = "UserId", ErrorMessage = "L'email existe déjà !")]
        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string Phone { get; set; }

        [StringLength(60, MinimumLength = 6, ErrorMessage = "Le mot de passe doit avoir une longueur minimale de 6 caractères")]
        public string Password { get; set; }

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [NotMapped]
        public List<Role> RoleCollection { get; set; }

        [InverseProperty("AssignmentManager")]
        public virtual List<Intervention> InterventionsAssignment { get; set; }
        [InverseProperty("Intervener")]
        public virtual List<Intervention> InterventionssIntervener { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}