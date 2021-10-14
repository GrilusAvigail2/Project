using Common;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public interface IPointService
    {
        public int GetNumPoints();
        public List<CPoint> GetListPoints();
        public CPoint GetPointById(int id);
        public CPoint GetPointByName(string name);
    }
}
