using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Menu : Entity
    { 
        public Menu()
        {
            InverseMenuNavigation = new HashSet<Menu>();
            PageSetup = new HashSet<PageSetup>();
        }

        public string Name { get; set; }
        public string RouteLink { get; set; }
        public bool IsPublished { get; set; }
        public long? MenuId { get; set; }

        public virtual Menu MenuNavigation { get; set; }
        public virtual ICollection<Menu> InverseMenuNavigation { get; set; }
        public virtual ICollection<PageSetup> PageSetup { get; set; }
    }
}
