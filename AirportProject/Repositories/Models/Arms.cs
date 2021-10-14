using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Arms
    {
        public Arms()
        {
            Points = new HashSet<Points>();
        }

        public int ArmId { get; set; }
        public string ArmName { get; set; }
        public int StructureId { get; set; }
        public int FloorNumber { get; set; }

        public virtual Structures Structure { get; set; }
        public virtual ICollection<Points> Points { get; set; }
    }
}
