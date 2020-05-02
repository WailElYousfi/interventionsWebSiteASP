using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class ViewModel
    {
        public List<Intervention> InterventionsTable { get; set; }
        public List<Request> RequestsTable { get; set; }
    }
}