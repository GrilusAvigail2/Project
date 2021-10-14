using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class DesignedFor
    {
        public DesignedFor()
        {
            Structures = new HashSet<Structures>();
        }

        public int DesignedForId { get; set; }
        public string DesignedForDescription { get; set; }

        public virtual ICollection<Structures> Structures { get; set; }
    }
}
