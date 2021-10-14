using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Points
    {
        public int PointId { get; set; }
        public string PointName { get; set; }
        public int CategoryId { get; set; }
        public int? ArmId { get; set; }

        public virtual Arms Arm { get; set; }
        public virtual Category Category { get; set; }
    }
}
