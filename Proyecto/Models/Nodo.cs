using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Nodo<T> where T : IComparable
    {
        public List<Nodo<T>> Edges { get; private set; }//Basicamente los hijos
        public List<T> Keys { get; private set; }//Las llaves del arbol 
        public Nodo<T> Parent { get; set; }

        public Nodo(T key)
        {
            Keys = new List<T>();
            Keys.Add(key);
            Edges = new List<Nodo<T>>();

        }

        public int HasKey(T k)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Keys[i].CompareTo(k) == 0)
                {
                    return 1;
                }
            }
            return -1;
        }
        public void InsertEdge(Nodo<T> edge)
        {
            for (int x = 0; x < Edges.Count; x++)
            {
                if (Edges[x].Keys[0].CompareTo(edge.Keys[0]) > 0)
                {
                    Edges.Insert(x, edge);
                    return;
                }
            }

            Edges.Add(edge);
            edge.Parent = this;
        }
        public bool RemoveEdge(Nodo<T> n)
        {
            return Edges.Remove(n);
        }
        public Nodo<T> RemoveEdge(int position)
        {
            Nodo<T> edge = null;
            if (Edges.Count > position)
            {
                edge = Edges[position];
                edge.Parent = null;
                Edges.RemoveAt(position);
            }

            return edge;
        }
        public Nodo<T> GetEdge(int position)
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
        public int FindEdgePosition(T k)
        {
            if (Keys.Count != 0)
            {
                T left = default(T);
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (k.CompareTo(Keys[x]) < 0)
                    {
                        return x;
                    }
                    else
                    {
                        left = Keys[x];
                    }
                }

                if (k.CompareTo(Keys[Keys.Count - 1]) > 0)
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
        public void Fuse(Nodo<T> n1)
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
                T k = n1.Keys[x];
                this.Push(k);
            }

            for (int x = Edges.Count - 1; x >= 0; x--)
            {
                Nodo<T> e = n1.RemoveEdge(x);
                this.InsertEdge(e);
            }
        }

        public void Fuse(Nodo<T> n1, Nodo<T> n2)
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
        public Nodo<T>[] Split()
        {
            if (Keys.Count != 2)
            {
                throw new InvalidOperationException(string.Format("This node has {0} keys, can only split a 2 keys node", Keys.Count));
            }

            Nodo<T> newRight = new Nodo<T>(Keys[1]);

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

            return new Nodo<T>[] { this, newRight };
        }

        public T Pop(int position)
        {
            if (Keys.Count == 1)
            {
                throw new InvalidOperationException("Cannot pop value from a 1 key node");
            }

            if (position < Keys.Count)
            {
                T k = Keys[position];
                Keys.RemoveAt(position);

                return k;
            }

            return default(T);
        }

        public void Push(T k)
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
                T left = default(T);
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (k.CompareTo(Keys[x]) < 0)
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
        public Nodo<T> Traverse(T k)
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