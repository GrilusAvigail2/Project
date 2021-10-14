using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppServices
{
    /// <summary>
    /// מחלקה המחשבת מרחק של מסלול ומשך זמן ההליכה שלו
    /// </summary>
    public static class TimeCalculation
    {
        /// <summary>
        /// פונקציה המחשבת את אורך המרחק מנקודת המקור אל נקודת היעד
        /// </summary>
        /// <param name="graph">גרף מפת שדה התעופה</param>
        /// <param name="pointsList">רשימת קודקודים המרכיבים יחד מסלול </param>
        /// <returns></returns>
        public static double CalculateDistance(Graph graph,List<Vertex> pointsList)
        {
            double distance = 0;
            //מעבר על כל קשתות המסלול וחישוב אורך המסלול, לפי רשימת הקודקודים שהתקבלה
            for (int i = 0; i < pointsList.Count()-1; i ++)
            {
                distance += graph.SearchEdge(pointsList[i], pointsList[i + 1]).GetWeight();
            }
            return distance;
        }

        /// <summary>
        ///פנקציה המחשבת את משך הזמן שלוקח ללכת את המרחק שהתקבלה
        ///החישוב הוא לפי 1.125 שניות למטר
        /// </summary>
        /// <param name="distance"> המרחק במטרים </param>
        /// <returns></returns>
        public static TimeSpan CalculateTime(double distance)
        {
            int hours;
            //זמן הליכת המרחק בשניות
            double distanceInSeconds = distance * 1.125;    
            hours = Convert.ToInt32(Math.Floor(distanceInSeconds / 3600));
            distanceInSeconds = distanceInSeconds - hours*3600;            
            int minutes = Convert.ToInt32(Math.Floor(distanceInSeconds / 60));
            distanceInSeconds = distanceInSeconds - minutes * 60;
            int seconds = Convert.ToInt32(Math.Floor(distanceInSeconds % 60));
            TimeSpan time =  new TimeSpan(hours,minutes,seconds);
            return time;
        }

        /// <summary>
        /// פונקציה המחשבת את אורך ומשך זמן ההליכה הכולל של המסלול שהתקבל
        /// </summary>
        /// <param name="graph">מפת שדה התעופהגרף </param>
        /// <param name="pointsList">רשימת קודקודים המרכיבים יחד מסלול</param>
        /// <returns></returns>
        public static TimeSpan CalculateTimeForDistance(Graph graph, List<Vertex> pointsList)
        {
            double distance = CalculateDistance(graph, pointsList);
            TimeSpan time = CalculateTime(distance);
            return time;
        }
    }
}
