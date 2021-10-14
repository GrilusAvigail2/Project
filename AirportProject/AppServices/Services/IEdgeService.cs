using System;
using System.Collections.Generic;
using System.Text;
using Common;


namespace AppServices.Services
{
    public interface IEdgeService
    {
        public int GetNumEdges();
        public List<CEdge> GetListEdges();
        public CEdge GetEdgeBySourceAndTarget(int sourceId, int targetId);
    }
}
