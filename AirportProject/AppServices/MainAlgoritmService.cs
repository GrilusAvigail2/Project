using AutoMapper;
using Common;
using Newtonsoft.Json;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;

namespace AppServices
{
    public class MainAlgoritmService: IMainAlgoritmService
    {
        //יצירת גרף
        Graph airportGraph;
        DijstraService dijstra = new DijstraService();

        IPointRepository repositoryPoint;
        IEdgeRepository repositoryEdge;
        IMapper mapper;
        public MainAlgoritmService(IPointRepository repositoryPoint, IEdgeRepository repositoryEdge, IMapper mapper)
        {
            this.repositoryPoint = repositoryPoint;
            this.repositoryEdge = repositoryEdge;
            this.mapper = mapper;
        }

        /// <summary>
        /// פונקצייה ראשית הקוראת לפונקציות המאתחלות את הגרף ומחשבות את הדרך הקצרה ביותר
        /// </summary>
        /// <param name="source">קודקוד מקור</param>
        /// <param name="destination">קודקוד יעד</param>
        /// <param name="flightTime">שעת הטיסה</param>
        /// <returns></returns>
        public List<Vertex> FindWay(Vertex source,Vertex destination,DateTime flightTime)
        {
            //שליחה לפונקצית אתחול הגרף
            GraphService graphService = new GraphService(repositoryPoint, repositoryEdge);
            airportGraph = graphService.InitGraph(airportGraph);

            //שליחה לפונקצייה המפעילה את אלגוריתם דיקסטרה על הגרף המאותחל 
            //וקבלת רשימת הצמתים המרכיבים את הדרך הקצרה ביותר
            List<Vertex> lstVertices = dijstra.FindShortestWay(airportGraph,source,destination);

            //חישוב אורך המסלול הקצר שנמצא (מהמקור אל היעד הנבחר) ומשך זמן ההליכה הכולל שלו
            TimeSpan timeWay = TimeCalculation.CalculateTimeForDistance(airportGraph,lstVertices);

            //חישוב אורך המסלול מהיעד הנבחר אל מקום העליה למטוס(הנקודה שהמזהה שלה=12)ומשך זמן ההליכה הכולל שלו
            Points p = repositoryPoint.GetByName("עלייה למטוס");
            Vertex v = new Vertex(p);
            TimeSpan timeToAirport = TimeCalculation.CalculateTimeForDistance(airportGraph,
                dijstra.FindShortestWay(airportGraph,destination,v));

            //בדיקה האם סכום זמן המסלול הנבחר וזמן הדרך מהיעד אל העליה למטוס ,קטן מהזמן שנותר עד לטיסה
            //במקרה שכן-מחזיר את רשימת הקודקודים במסלול הקצר , אחרת מציג הודעה המתריעה שאין מספיק זמן
            TimeSpan timeLeaved = flightTime - DateTime.Now;
            if(timeWay + timeToAirport <= timeLeaved)
            {
               // List<CPoint> clstVertices = mapper.Map<List<CPoint>>(lstVertices);
                return lstVertices;
            }
            else
            {
               //סימן שיש להתריע למשתמש שהזמן הכולל של המסלול קטן מהזמן שנשאר עד לטיסה
                return new List<Vertex>(); 
            }
        }
        
    } 
}
