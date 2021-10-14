using AutoMapper;
using Common;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class PointService:IPointService
    {
        IPointRepository repository;
        IMapper mapper;
        public PointService(IPointRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public int GetNumPoints()
        {
            return repository.GetNumOfItems();
        }
        public List<CPoint> GetListPoints()
        {
            List<Points> points = repository.GetAll();
            List<CPoint> cpoints = mapper.Map<List<CPoint>>(points);
            return cpoints;
        }
        public CPoint GetPointById(int id)
        {
            Points point = repository.GetById(id);
            CPoint cpoint = mapper.Map<CPoint>(point);
            return cpoint;
        }
        public CPoint GetPointByName(string name)
        {
            Points point = repository.GetByName(name);
            CPoint cpoint = mapper.Map<CPoint>(point);
            return cpoint;
        }

    }
}
