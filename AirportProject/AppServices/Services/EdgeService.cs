using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Common;
using Repositories;
using Repositories.Models;

namespace AppServices.Services
{
    public class EdgeService:IEdgeService
    {
        IEdgeRepository repository;
        IMapper mapper;
        public EdgeService(IEdgeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public int GetNumEdges()
        {
            return repository.GetNumOfItems();
        }

        public List<CEdge> GetListEdges()
        {
            List<Edges> edges = repository.GetAll();
            List<CEdge> cedges = mapper.Map<List<CEdge>>(edges);
            return cedges;
        }
        public CEdge GetEdgeBySourceAndTarget(int sourceId, int targetId)
        {
            Edges edge = repository.GetBySourceAndTarget(sourceId,targetId);
            CEdge cedge = mapper.Map<CEdge>(edge);
            return cedge;
        }
    }
}
