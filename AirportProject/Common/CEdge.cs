using System;
using System.Collections.Generic;
using System.Text;


namespace Common
{
    public class CEdge
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public string TargetId { get; set; }
        public int Weight { get; set; }
    }
}
