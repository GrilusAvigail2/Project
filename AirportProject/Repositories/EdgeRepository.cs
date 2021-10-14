using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class EdgeRepository : IEdgeRepository
    {
        MainDataContext context;

        //services שיוזרק מה  DBContext שימוש ב
        public EdgeRepository(MainDataContext context)
        {
            this.context = context;
        }

        //Edges מחזיר את מספר הקשתות בגרף-מספר הקשתות בטבלת 
        public int GetNumOfItems()
        {
            return context.Edges.Count();
        }

        //פונקציה שמחזירה רשימה של כל הקשתות
        public List<Edges> GetAll()
        {
            return context.Edges.ToList();
        }
        // id פונקציה שמחזירה קשת לפי ה
        public Edges GetById(int id)
        {
            return context.Edges.Where(e => e.EdgeId == id).FirstOrDefault();
        }
        //של נקודות המקור והיעד של הקשת id פונקציה שמחזירה קשת לפי ה  
        public Edges GetBySourceAndTarget(int sourceId,int targetId)
        {
            return context.Edges.Where(e => e.SourceId == sourceId && e.TargetId == targetId).FirstOrDefault();
        }
    }
}


