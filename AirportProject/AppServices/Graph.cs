using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppServices
{
   public class Graph
   {
        //רשימת הקודקודים בגרף
        List<Vertex> Vertices { get; set; }

        //רשימת הצלעות בגרף
        List<Edge> Edges { get; set; }

        //מספר קודקודים
        int NumVertices;

        public Graph()
        {
                
        }
        //בנאי של מחלקת גרף
        public Graph(int numVertices)
        {
            NumVertices = numVertices;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        //החזרת רשימת הקודקודים
        public List<Vertex> GetListVertices()
        {
            return Vertices;
        }

        //עדכון רשימת הקודקודים
        public void SetListVertices(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        //החזרת רשימת הקשתות
        public List<Edge> GetListEdge()
        {
            return Edges;
        }
        
        //עדכון רשימת הקשתות
        public void SetListEdge(List<Edge> edges)
        {
            Edges = edges;
        }

        //החזרת מספר הקודקודים בגרף
        public int GetNumVertices()
        {
            return NumVertices;
        }
        //עריכת מספר הקודקןדים
        public void SetNumVertices()
        {
           NumVertices++;
        }

        //הוספת קודקוד
        public void AddVeretx(Vertex v)
        {
            foreach (var vertex in this.Vertices)
            {
                if (vertex.GetIndex() == v.GetIndex())
                    throw new ArgumentException("The vertex cannot be adjacent to itself");
            }
            SetNumVertices();
            Vertices.Add(v);
        }

        //הוספת קשת המחברת בין 2 קודקודים
        public void AddEdge(Vertex v1, Vertex v2, int weight = 1)
        {
            if (v1.GetConectedVertices().Count >= this.NumVertices ||
                v2.GetConectedVertices().Count >= this.NumVertices||
                v1.GetIndex() < 0 || v2.GetIndex() < 0)
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");

            if (weight < 1) throw new ArgumentException("Weight cannot be less than 1");

            //יצירת קשת חדשה ועריכת הנתונים שלה
            // v1 הוספת הקשת לרשימת הקשתות המחוברות לקודקוד 
            //הוספת הקשת רשימת הקשתות של הגרף
            Edge edge = new Edge(weight, v2);
            v1.GetConectedVertices().Add(edge);
            this.Edges.Add(edge);
        }
        
        //פונקציה המקבלת 2 קודקודים ומחזירה את משקל הקשת המחברת בינהם
        public int GetWeightBetweenTowVerteices(Vertex v1,Vertex v2)
        {
            for (int i = 0; i < v1.GetConectedVertices().Count(); i++)
            {
                if(v1.GetConectedVertices()[i].GetTargetVertex() == v2)
                {
                    return v1.GetConectedVertices()[i].GetWeight();
                }
            }
            return 0;
        }
        //פונקציית מחיקת קשת בין 2 צמתים
        public void RemoveEdge(Vertex v1, Vertex v2)
        {
            if (v1.GetConectedVertices().Count >= this.NumVertices ||
                v2.GetConectedVertices().Count >= this.NumVertices ||
                v1.GetIndex() < 0 || v2.GetIndex() < 0)
                throw new IndexOutOfRangeException();
            //מעבר על רשימת הצלעות ועבור הצלע המבוקשת להסרה
            //נסיר אותה מרשימת הצלעות של הגרף 
            // v ונסיר אותה מהרשימת הקודקודים המקושרים לקודקוד 
            for (int i = 0; i < this.Edges.Count; i++)
            {
                if (this.Edges[i].GetTargetVertex() == v2)
                {
                    this.Edges.Remove(this.Edges[i]);
                    v1.GetConectedVertices().Remove(this.Edges[i]);
                }
            }
        }

        //פונקציית עדכון משקל-צלע
        public void UpdateWeight(Vertex v1, Vertex v2, int weight = 1)
        {
            if (weight < 0)
                throw new IndexOutOfRangeException();
            //מעבר על כל רשימת הקוקודים
            //ועבור כל קודקוד נעבור על כל רשימת הקודקודים שמקושרים אליו
            //ונחפש את הצלע שמקשרת בין 2 הקוקודים שהתקבלו 
            //ולבסוף נעדכן את המשקל
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i] == v1)
                {
                    for (int j = 0; j < Vertices[i].GetConectedVertices().Count; j++)
                    {
                        if (Vertices[i].GetConectedVertices()[j].GetTargetVertex() == v2)
                            Vertices[i].GetConectedVertices()[j].SetWeight(weight);
                    }
                }
            }

        }

        //מציאת קודקוד
        public Vertex SearchVertex(Vertex v)
        {
            foreach (var vertex in this.Vertices)
            {
                if (vertex == v)
                    return vertex;
            }
            return null;
        }
        //מציאת צלע
        public Edge SearchEdge(Vertex v1, Vertex v2)
        {
            for (int i = 0; i < v1.GetConectedVertices().Count; i++)
            {
                if (v1.GetConectedVertices()[i].GetTargetVertex().GetIndex() == v2.GetIndex())
                {
                    return v1.GetConectedVertices()[i];
                }
            }
            return null;
        }
   }
}

