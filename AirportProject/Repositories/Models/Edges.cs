using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Edges
    {
        public int EdgeId { get; set; }
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public int Weight { get; set; }
    }
}
