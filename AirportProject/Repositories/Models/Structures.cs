using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Structures
    {
        public Structures()
        {
            Arms = new HashSet<Arms>();
        }

        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public int FloorNumber { get; set; }
        public int DesignedForId { get; set; }

        public virtual DesignedFor DesignedFor { get; set; }
        public virtual ICollection<Arms> Arms { get; set; }
    }
}
