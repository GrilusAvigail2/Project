using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public class Edge
    {
        //משקל הקשת
        int Weight { get; set; }

        //קודקוד יעד
        Vertex TargetVertex { get; set; }
     
        //בנאי 
        public Edge(int Weight, Vertex TargetVertex)
        {
            this.Weight = Weight;
            this.TargetVertex = TargetVertex;
        }

        //החזרת משקל הקשת
        public int GetWeight()
        {
            return this.Weight;
        }
        //עדכון משקל הקשת
        public void SetWeight(int w)
        {
            this.Weight = w;
        }
        //החזרת קודקוד יעד
        public Vertex GetTargetVertex()
        {
            return this.TargetVertex;
        }
        //עדכון קודקוד יעד
        public void SetTargetVertex(Vertex vertex)
        {
            this.TargetVertex = vertex;
        }

    }
}
