using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    /// <summary>
    /// מציאת המסלול הקצר ביותר - האלגוריתם הראשי
    /// </summary>
    public class DijstraService:IDijstraService
    {
        //מילון המכיל מפתח-אינדקס של צומת וערך-צומת של האבא עבור עץ המסלולים הקצרים ביותר
        Dictionary<int, Vertex> parent = new Dictionary<int, Vertex>();

        /// <summary>
        /// פונקציה למציאת הדרך הקצרה ביותר מתוך המסלולים הקצרים שהתקבלו מאלגוריתם דייקסטרה
        /// מעבר על האבות של הקודקודים המרכיבים את המסלול הקצר ביותר מהמקור ליעד
        /// </summary>
        /// <param name="g"> גרף מפת שדה התעופה</param>
        /// <param name="source">נקודת מקור</param>
        /// <param name="target">נקודת יעד</param>
        /// <returns></returns>
        public List<Vertex> FindShortestWay(Graph g, Vertex source, Vertex target)
        {
            Dijstra(g, source);
            List<Vertex> way = new List<Vertex>();
            way.Add(target);
            int i = target.GetIndex() - 1;
            while (parent[i] != null)
            {
                way.Add(parent[i]);
                i = parent[i].GetIndex() - 1;
            }
            way.Reverse();
            return way;
        }

        /// <summary>
        /// graph אתחול המרחק של כל קודקוד בגרף 
        /// </summary>
        /// <param name="graph">גרף מפת שדה התעופה</param>
        /// <param name="source">קודקוד מקור</param>
        /// <returns>מחזירה מערך מרחקים מאותחלים לכל קודקוד</returns>

        public double[] InitializeSingleSource(Graph graph,Vertex source)
        {
            //מערך מרחקים שיכיל את אמדן המרחק של כל צומת בגרף מצומת המקור
            double[] distance = new double[graph.GetNumVertices()];

            //nullאתחול האומדן של כל צומת בגרף לאינסוף וצומת האב ל
            foreach (Vertex vertex in graph.GetListVertices())
            {
                distance[vertex.GetIndex()-1] = int.MaxValue;
                parent[vertex.GetIndex()-1] = null;
            }

            //אתחול אמדן צומת המקור ל0
            distance[source.GetIndex()-1] = 0;
            return distance;
        }

        /// <summary>
        /// פונקציית הקלה
        /// הבודקת אם ניתן לשפר את המסלול הקצר ביותר שנמצא עד כה ע"י מעבר דרך קודקוד השכן  
        /// אם כן,נעדכן את האומדן של הקודקוד הנוכחי לאומדן של הקודקוד השכן לו+משקל הקשת בינהם
        /// </summary>
        /// <param name="neighbor">vertex אחד השכנים של הקודקוד </param>
        /// <param name="vertex">הקודקוד הנוכחי</param>
        /// <param name="distance">מערך המרחקים</param>
        /// <param name="weight">משקל הקשת בין 2 הקודקודים</param>
        public void Relax(Vertex neighbor,Vertex vertex,double[] distance,double weight)
        {
            //אם אומדן הקודקוד גדול מאומדן הקודקוד השכן לו בתוספת משקל הקשת ביניהם
            if(distance[vertex.GetIndex()-1] > distance[neighbor.GetIndex()-1]+ weight)
            {
                //מעדכנים את האומדן של הקודקוד לאומדן של הקודקוד השכן בתוספת משקל הקשת ביניהם
                distance[vertex.GetIndex()-1] = distance[neighbor.GetIndex()-1] + weight;
                //מעדכנים את קודקוד האבא לקודקוד השכן
                parent[vertex.GetIndex()-1] = neighbor;
            }
        }

        /// <summary>
        /// האלגוריתם הראשי-מוצא את המסלול הקצר ביותר מקודקוד מקור לכל קוקוד אחר בגרף   
        /// </summary>
        /// <param name="graph">גרף מפת שדה התעופה</param>
        /// <param name="source">קודקוד מקור</param>
        /// <returns></returns> 
        public List<Vertex> Dijstra(Graph graph,Vertex source)
        {
            //קריאה לפונקציית אתחול האומדנים והצבתם במערך המרחקים
            double[] distance = InitializeSingleSource(graph, source);

            //אתחול רשימת הקודקודים בה כבר נקבע המסלול הקצר ביותר
            List<Vertex> S = new List<Vertex>();

            //תור קדימויות שיכיל את כל הקודקודים בגרף 
            List<Vertex> queue = new List<Vertex>();

            //אתחול התור כדי שנוכל לשלוף את הקודקודים ללא שינוי הרשימה המקורית 
            foreach (Vertex v in graph.GetListVertices())
            {
                queue.Add(v);
            }

            //Sבכל איטרציה הקודקוד עם האומדן הקטן ביותר נשלף לקבוצה    
            while (queue.Count != 0)
            {
                Vertex u = ExtractMin(queue, distance);

                S.Add(u);

                //על ידי קריאה לפונקציית ההקלה u ביצוע הקלה עם כל קשת היוצאת מ
                //ועל ידי כך מתעדכן האומדן וקודקוד האב אם המסלול הקצר ביותר ניתן לשיפור 
                foreach (Edge neighbor in u.GetConectedVertices())
                {
                    Relax(u, neighbor.GetTargetVertex(), distance, neighbor.GetWeight());
                }
                //מחיקה מתור הקדימויות, קודקודים שסיימנו להשתמש בהם
                queue.Remove(u);
            }   
            return S;
        }

        /// <summary>
        /// פונקצייה ששולפת מהתור את הקודקוד עם האומדן הקטן ביותר
        /// </summary>
        /// <param name="queue">תור קדימויות של קודקודים</param>
        /// <param name="distance">מערך מרחקים של כל קודקוד</param>
        /// <returns></returns>
        public Vertex ExtractMin(List<Vertex> queue, double[] distance)
        {
            Vertex minVertex = queue[0];
            foreach (Vertex v in queue)
            {
                if (distance[minVertex.GetIndex()-1] > distance[v.GetIndex()-1])
                {
                    minVertex = v;
                }
            }
            return minVertex;
        }
    }
}
