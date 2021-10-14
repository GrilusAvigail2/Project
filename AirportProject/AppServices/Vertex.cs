using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace AppServices
{
   public  class Vertex
   {

        //מספר מזהה של צומת
        private int Index { get; set; }

        //שם הצומת
        private string Name { get; set; }

        //Points הקודקוד עצמו מסוג מחלקת 
        private readonly Points VertexPoint;
        
        //רשימת הצמתים שמתחברים לצומת הנוכחית
        private List<Edge> ConnectedVertices { get; set; }

        //בנאי של מחלקת קודקוד

        public Vertex(Points vertexPoint)
        {
            Index = vertexPoint.PointId;
            Name = vertexPoint.PointName;
            VertexPoint = new Points();
            VertexPoint = InitVertexPoint(vertexPoint);
            ConnectedVertices = new List<Edge>();
        }

        //Points פונקצייה המאתחלת את ערכי הקודקוד מסוג מחלקת 
        public Points InitVertexPoint(Points vertexPoint)
        {
            VertexPoint.PointId = vertexPoint.PointId;
            VertexPoint.PointName = vertexPoint.PointName;          
            VertexPoint.CategoryId = vertexPoint.CategoryId;        
            VertexPoint.ArmId = vertexPoint.ArmId;
            return vertexPoint;
        }

        //קבלת ערך האינדקס של הקודקוד
        public int GetIndex()
        {
            return Index;
        }

        //עריכת האינדקס של הקודקוד
        public void SetIndex(int index)
        {
            Index = index;
        }
        //קבלת שם הקודקוד
        public string GetName()
        {
            return Name;
        }

        //עריכת שם הקודקוד
        public void SetName(string name)
        {
            Name = name;
        }

        //החזרת הקשתות המחוברות לקודקוד
        public List<Edge> GetConectedVertices()
        {
            return ConnectedVertices;
        }

        //עריכת הקשתות המחוברות לקודקוד
        public void SetConectedVertices(List<Edge> connectedVertices)
        {
            ConnectedVertices = connectedVertices;
        }

        //Point החזרת הקודקוד מסוג מחלקת 
        public Points GetVertexPoint()
        {
            return VertexPoint;
        }
   }
}
