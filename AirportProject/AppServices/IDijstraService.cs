using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public interface IDijstraService
    {
        public List<Vertex> FindShortestWay(Graph g, Vertex source, Vertex target);
        public double[] InitializeSingleSource(Graph graph, Vertex source);
        public void Relax(Vertex neighbor, Vertex vertex, double[] distance, double weight);
        public List<Vertex> Dijstra(Graph graph, Vertex source);
        public Vertex ExtractMin(List<Vertex> queue, double[] distance);
    }
}
