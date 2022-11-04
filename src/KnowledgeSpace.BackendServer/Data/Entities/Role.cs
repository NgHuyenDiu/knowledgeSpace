using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    public class Role : IdentityRole
    {
        public Role()
        {
        }

       
        public ICollection<Permission> Permisstions { get; set; }
    }
}

