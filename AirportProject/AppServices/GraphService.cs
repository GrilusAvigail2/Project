using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Models;
using Repositories;

namespace AppServices
{
    /// <summary>
    /// מחלקת אתחול ובניית הגרף
    /// </summary>
    public class GraphService:IGraphService
    {
        IPointRepository repositoryPoint;
        IEdgeRepository repositoryEdge;
        public GraphService(IPointRepository repositoryPoint, IEdgeRepository repositoryEdge)
        {
            this.repositoryPoint = repositoryPoint;
            this.repositoryEdge = repositoryEdge;

        }

        /// <summary>
        /// פונקציית אתחול ובניית הגרף
        /// </summary>
        /// <param name="graph">גרף לא מאותחל</param>
        /// <returns>גרף בנוי ומאותחל לייצוג מפת שדה התעופה</returns>
        public Graph InitGraph(Graph graph)
        {
            graph = new Graph(repositoryPoint.GetNumOfItems());

            //אתחול רשימת הקשתות
            //ייצור קשת עבור כל קשת בטבלה והוספה למערך הקשתות של הגרף 
            foreach (var edge in repositoryEdge.GetAll())
            {
                Points p = repositoryPoint.GetById(edge.TargetId);
                Vertex targetVertex = new Vertex(p);
                Edge e = new Edge(edge.Weight, targetVertex);
                graph.GetListEdge().Add(e);   
            }

            //אתחול רשימת הצמתים
            //ייצור צומת עבור כל נקודה בטבלה והוספה למערך הצמתים של הגרף 
            foreach (var point in repositoryPoint.GetAll())
            {
                Vertex vertex = new Vertex(point);
                graph.GetListVertices().Add(vertex);

                //הוספת קשתות לרשימת הקשתות היוצאות מהצומת הנוכחית
                foreach (var edge in repositoryEdge.GetAll())
                {
                    if(edge.SourceId == vertex.GetIndex())
                    {
                        Vertex v = new Vertex(repositoryPoint.GetById(edge.TargetId));
                        Edge e = new Edge(edge.Weight,v);
                        vertex.GetConectedVertices().Add(e);
                    }
                }
            }

            return graph;

        }
    }
} 