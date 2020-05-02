using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class Intervention
    {
        [Key]
        public int InterventionId { get; set; }

        public DateTime? ActionDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Nature")]
        public string Nature { get; set; }

        [Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int? StateId { get; set; }
        public int? AgencyId { get; set; }
        public int? DirectionId { get; set; }
        public int? RequestId { get; set; }
        public int? AssignmentManagerId { get; set; }
        public int? IntervenerId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        [ForeignKey("AgencyId")]
        public virtual Agency Agency { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }
        [ForeignKey("AssignmentManagerId")]
        public virtual User AssignmentManager { get; set; }
        [ForeignKey("IntervenerId")]
        public virtual User Intervener { get; set; }

        [NotMapped] // it is not a column in Staffs table. 
        public List<State> StateCollection { get; set; }
        [NotMapped]  
        public List<Agency> AgencyCollection { get; set; }
        [NotMapped]  
        public List<Direction> DirectionCollection { get; set; }
        [NotMapped]  
        public List<Request> RequestCollection { get; set; }
        [NotMapped] 
        public List<User> UserCollection { get; set; }

    }
}