using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class Role : IRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ThinFrontUser> Users { get; set; }
    }
}
