using System;
using System.Collections.Generic;

namespace KendoMVC.Models.Entities
{
    [Serializable]
    public class Role
    {
        public long Number { get; set; }

        public string RoleName { get; set; }

        public bool IsAdmin { get; set; }

        public IList<Resource> Resources { get; set; } 
    }
}
