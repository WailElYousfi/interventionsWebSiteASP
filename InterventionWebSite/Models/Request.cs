using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Type de communication")]
        public string TypeOfCommunication { get; set; }

        public DateTime? RequestDate { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Problèmes")]
        public string problems { get; set; }

        public virtual List<Intervention> Interventions { get; set; }

        public int? StaffId { get; set; }
        public int? ReceptionEmployeeId { get; set; }

        [JsonProperty("Staff")]
        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }
        [ForeignKey("ReceptionEmployeeId")]
        public virtual User ReceptionEmployee { get; set; }

        [NotMapped]
        public List<Staff> StaffCollection { get; set; }
        [NotMapped]
        public List<User> UserCollection { get; set; }
    }
}