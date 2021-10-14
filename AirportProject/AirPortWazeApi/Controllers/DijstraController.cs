using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppServices;
using Repositories;
using Repositories.Models;
using AppServices.Services;
using Common;
using AutoMapper;

namespace AirportWazeAPI.Controllers
{
    public class DijstraController : AppBaseController
    {
        IPointService pointService;
        IMainAlgoritmService algoritmService;
        IMapper mapper;
        public DijstraController(IMainAlgoritmService algoritmService, IPointService pointService, IMapper mapper)
        {
            this.algoritmService = algoritmService;
            this.pointService = pointService;
            this.mapper = mapper;
        }



        //הפעלת הפונקציה הראשית המחזירה רשימת צמתים של הדרך הקצרה ביותר מהמקור ליעד  שהתקבל מהמשתמש
        //api/Dijstra
        [HttpGet]
        public List<CPoint> GetWay()
        {
           // בדיקה אם הדייקסטרה עובד
            CPoint ps = pointService.GetPointById(1);
            Points psp = mapper.Map<Points>(ps);
            Vertex s = new Vertex(psp);
            CPoint pd = pointService.GetPointById(5);
            Points pdp = mapper.Map<Points>(pd);
            Vertex d = new Vertex(pdp);
            List<Vertex> list = algoritmService.FindWay(s, d, DateTime.Now.AddHours(1));
            List<CPoint> clist = new List<CPoint>() {
                new CPoint(){ PointId=list[0].GetIndex() ,PointName=list[0].GetName()},
                new CPoint(){ PointId=list[1].GetIndex() ,PointName=list[1].GetName()},
                new CPoint(){ PointId=list[2].GetIndex() ,PointName=list[2].GetName()},
                new CPoint(){ PointId=list[3].GetIndex() ,PointName=list[3].GetName()}
            };
            return clist;
        }
    }
}
