
using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IEdgeRepository : IReadWriteRepository<Edges>
    {
        public Edges GetBySourceAndTarget(int sourceId, int targetId);
    }
}
