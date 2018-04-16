using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Nodo
    {
        public List<Nodo> Edges{ get; private set; }//Basicamente los hijos
        public List<int> Keys { get; private set; }//Las llaves del arbol 
        public Nodo Parent { get; set; }

        public Nodo(int key)
        {
            Keys = new List<int>();
            Keys.Add(key);
            Edges = new List<Nodo>();
                
       }

        public int HasKey(int k)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Keys[i]== k)
                {
                    return k;
                }
            }
            return -1;
        }
        public void InsertEdge(Nodo edge)
        {
            for (int x = 0; x < Edges.Count; x++)
            {
                if (Edges[x].Keys[0] > edge.Keys[0])
                {
                    Edges.Insert(x, edge);
                    return;
                }
            }

            Edges.Add(edge);
            edge.Parent = this;
        }
        public bool RemoveEdge(Nodo n)
        {
            return Edges.Remove(n);
        }
        public Nodo RemoveEdge(int position)
        {
            Nodo edge = null;
            if (Edges.Count > position)
            {
                edge = Edges[position];
                edge.Parent = null;
                Edges.RemoveAt(position);
            }

            return edge;
        }
        public Nodo GetEdge(int position)
        {
            if (position < Edges.Count)
            {
                return Edges[position];
            }
            else
            {
                return null;
            }
        }
        public int FindEdgePosition(int k)
        {
            if (Keys.Count != 0)
            {
                int left = 0;
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (left <= k && k < Keys[x])
                    {
                        return x;
                    }
                    else
                    {
                        left = Keys[x];
                    }
                }

                if (k > Keys[Keys.Count - 1])
                {
                    return Keys.Count;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
            
        }
        public void Fuse(Nodo n1)
        {
            int totalKeys = n1.Keys.Count;
            int totalEdges = n1.Edges.Count;

            totalKeys += this.Keys.Count;
            totalEdges += this.Edges.Count;

            if (totalKeys > 3)
            {
                throw new InvalidOperationException("Total keys of all nodes exceeded 3");
            }


            if (totalEdges > 4)
            {
                throw new InvalidOperationException("Total edges of all nodes exceeded 4");
            }


            for (int x = 0; x < n1.Keys.Count; x++)
            {
                int k = n1.Keys[x];
                this.Push(k);
            }

            for (int x = Edges.Count - 1; x >= 0; x--)
            {
                Nodo e = n1.RemoveEdge(x);
                this.InsertEdge(e);
            }
        }

        public void Fuse(Nodo n1, Nodo n2)
        {
            int totalKeys = n1.Keys.Count;
            int totalEdges = n1.Edges.Count;

            totalKeys += n2.Keys.Count;
            totalEdges += n2.Edges.Count;
            totalKeys += this.Keys.Count;
            totalEdges += this.Edges.Count;

            if (totalKeys > 3)
            {
                throw new InvalidOperationException("Total keys of all nodes exceeded 3");
            }

            if (totalEdges > 4)
            {
                throw new InvalidOperationException("Total edges of all nodes exceeded 4");
            }

            this.Fuse(n1);
            this.Fuse(n2);
        }
        public Nodo[] Split()
        {
            if (Keys.Count != 2)
            {
                throw new InvalidOperationException(string.Format("This node has {0} keys, can only split a 2 keys node", Keys.Count));
            }

            Nodo newRight = new Nodo(Keys[1]);

            for (int x = 2; x < Edges.Count; x++)
            {
                newRight.Edges.Add(this.Edges[x]);
            }

            for (int x = Edges.Count - 1; x >= 2; x--)
            {
                this.Edges.RemoveAt(x);
            }

            for (int x = 1; x < Keys.Count; x++)
            {
                Keys.RemoveAt(x);
            }

            return new Nodo[] { this, newRight };
        }

        public int? Pop(int position)
        {
            if (Keys.Count == 1)
            {
                throw new InvalidOperationException("Cannot pop value from a 1 key node");
            }

            if (position < Keys.Count)
            {
                int k = Keys[position];
                Keys.RemoveAt(position);

                return k;
            }

            return null;
        }

        public void Push(int k)
        {
            if (Keys.Count == 3)
            {
                throw new InvalidOperationException("Cannot push value into a 3 keys node");
            }

            if (Keys.Count == 0)
            {
                Keys.Add(k);
            }
            else
            {
                int left = 0;
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (left <= k && k < Keys[x])
                    {
                        Keys.Insert(x, k);
                        return;
                    }
                    else
                    {
                        left = Keys[x];
                    }
                }
                Keys.Add(k);
            }
        }
        public Nodo Traverse(int k)
        {
            int pos = FindEdgePosition(k);

            if (pos < Edges.Count && pos > -1)
            {
                return Edges[pos];
            }
            else
            {
                return null;
            }
        }

    }

}