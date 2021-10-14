
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Repositories
{
    public interface IPointRepository:IReadWriteRepository<Points>
    {
        public void AddPoint(Points point);
        public Points GetByName(string name);
        public List<Points> GetPointsInArm(int armId);
        public List<Points> GetPointsInCategory(Category category);
    }
}
