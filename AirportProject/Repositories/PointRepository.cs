
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Repositories
{
    //מחלקת פונקציות שליפה מהדטה בייס
    public class PointRepository:IPointRepository
    {
        MainDataContext context;
        
        //services שיוזרק מה  DBContext שימוש ב
        public PointRepository(MainDataContext context)
        {
           this.context = context;
        }

        //Point מחזיר את מספר הקודקודים בגרף-מספר הנקודות בטבלת 
        public int GetNumOfItems()
        {
            return context.Points.Count();      
        }

        //פונקציה המחזירה רשימה של כל הנקודות-קודקודים
        public List<Points> GetAll()
        {
            return context.Points.Include(p=>p.Arm).Include(p=>p.Category).ToList();
        }

        // id פונקציה שמחזירה קודקוד לפי ה
        public Points GetById(int id)
        {
            return context.Points.Where(p => p.PointId == id).FirstOrDefault();
        }
        //פונקציה שמחזירה קודקוד לפי שם הקודקוד
        public Points GetByName(string name)
        {
            return context.Points.Where(p => p.PointName == name).FirstOrDefault();
        }

        //Point פונקציה שמוסיפה נקודה לטבלת 
        public void AddPoint(Points point)
        {
            context.Points.Add(point);
            context.SaveChanges();
        }

        //פונקציה שמחזירה רק את הנקודות שנמצאות בזרוע שקיבלה
        public List<Points> GetPointsInArm(int armId)
        {
            var points = context.Points.Where(p => p.ArmId == armId).Include(p => p.Arm).Include(p => p.Category).ToList();
            return points;
        }

        // פונקציה שמחזירה רק את הנקודות באותו קטגוריה שקיבלה
        public List<Points> GetPointsInCategory(Category category)
        {
            var points = context.Points.Where(p => p.CategoryId == category.CategoryId).ToList();
            return points;
        }

        //פונקציה שמחזירה רק את הנקודות שנמצאות במבנה שקיבלה
        //public List<Point> GetPointsInStructure(int structureId)
        //{
        //    var points = context.Point.Where(p => p.StructureId == structureId).ToList();
        //    return points;
        //}


        ////פונקציה שמחזירה רק את הנקודות שנמצאות בקומה שקיבלה
        //public List<Points> GetPointsInFloor(int floor)
        //{
        //    var points = context.Points.Where(p => p.FloorNumber == floor).ToList();
        //    return points;
        //}


        //פונקציה שמחזירה את כל הנקודות שמיועדות לאותו הגדרה
        //public List<Points> GetAllPointsDesignedFor(DesignedFor designedFor)
        //{
        //    var points = context.Points.Where(p => p.DesignedForId == designedFor.DesignedForId).ToList();
        //    return points;
        //}


    }


} 
