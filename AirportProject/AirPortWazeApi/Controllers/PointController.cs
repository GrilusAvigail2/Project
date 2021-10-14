using AppServices;
using AppServices.Services;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportWazeAPI.Controllers
{
    public class PointController : AppBaseController
    {
        IPointService pointService;
        IEdgeService edgeService;
        IMainAlgoritmService algoritmService;
        IMapper mapper;

        Vertex vertexTarget = null;
        Vertex vertexSource = null;

        public PointController(IPointService service, IEdgeService edgeService , IMainAlgoritmService algoritmService, IMapper mapper)
        {
            this.pointService = service;
            this.edgeService = edgeService;
            this.algoritmService = algoritmService;
            this.mapper = mapper;
        }

        //הצגת כל הנקודות מטבלת נקודות
        //api/Point/getAll
        [HttpGet("getAll")]
        public List<CPoint> GetAll()
        {
            return pointService.GetListPoints();
        }

        //api/Point/byquery/
        [HttpGet("byquery/{query}")]
        public List<CPoint> GetByQuery(string query)
        {
            return pointService.GetListPoints().FindAll(point=> point.PointName.StartsWith(query));
        }

        //api/Point/byId/
        [HttpGet("byId/{id}")]
        public List<CPoint> GetById(int id)
        {
            return pointService.GetListPoints().FindAll(point => point.PointId == id);
        }

        //מקבלת 2 קודים של נקודות ובונה 2 טיפוסים לפי הדאטה בייס עם הקודים האלו  
        ///api/Points/viewWay/id/id
        [HttpGet("viewWay/{idSource}/{idTarget}/{flightTime}")]
        public List<CPointInWay> GetPointsInWay(int idSource, int idTarget, DateTime flightTime)
        {
            //מציאת 2 קודקודי קצה של המסלול-מקור ויעד
            CPoint s = pointService.GetPointById(idSource);
            CPoint t = pointService.GetPointById(idTarget);
            List<CPoint> sourceAndTarget = new List<CPoint>() { s, t };
            List<Points> points = mapper.Map<List<Points>>(sourceAndTarget);
            vertexSource = new Vertex(points[0]);//יצירת צומת לקודקוד המקור
            vertexTarget = new Vertex(points[1]);//יצירת צומת לקודקוד היעד

            //שליחה לפונקצית האלגוריתם הראשי שמוצאת את הדרך הקצרה
            List<Vertex> pointsInWay = new List<Vertex>();
            pointsInWay = algoritmService.FindWay(vertexSource, vertexTarget, flightTime);

            //רשימה שבה יש את השמות של הנקודות המרכיבות את המסלול
            List<CPointInWay> lstPointsInWay = new List<CPointInWay>();

            //אם יש מספיק זמן ללכת את המסלול עד לזמן הטיסה
            if (pointsInWay.Count() > 0)
            {
                for (int i = 0; i < pointsInWay.Count()-1; i++)
                {
                    int sId = pointsInWay[i].GetIndex();
                    int tId = pointsInWay[i + 1].GetIndex();
                    int length = edgeService.GetEdgeBySourceAndTarget(sId, tId).Weight;
                    if (i == 0)
                    {
                        lstPointsInWay.Add(new CPointInWay()
                        {
                            Name = pointsInWay[0].GetName(),
                            Length = 0,
                            WalkingTime = 0
                        });
                    }
                    TimeSpan time = TimeCalculation.CalculateTime(length);
                    lstPointsInWay.Add(new CPointInWay()
                    {
                        Name = pointsInWay[i + 1].GetName(),
                        Length = length,
                        WalkingTime = time.Hours * 3600 + time.Minutes * 60 + time.Seconds
                    }); ;

                }
               
            }  
            //אחרת-התרעה למשתמש
            else
            {
                lstPointsInWay.Add(new CPointInWay() { Name = "no time", Length = 0, WalkingTime = 0 });
            }
            return lstPointsInWay;
        }


      
    }
}
