using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public interface IMainAlgoritmService
    {
        public List<Vertex> FindWay(Vertex source, Vertex destination, DateTime flightTime);
    }
}
