using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class NodoS
    {
        public List<NodoS> Edges{ get; private set; }//Basicamente los hijos
        public List<string> Keys { get; private set; }//Las llaves del arbol 
        public NodoS Parent { get; set; }

        public NodoS(string key)
        {
            Keys = new List<string>();
            Keys.Add(key);
            Edges = new List<NodoS>();
                
       }

        public int HasKey(string k)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Keys[i].CompareTo(k)==0)
                {
                    return 1;
                }
            }
            return -1;
        }
        public void InsertEdge(NodoS edge)
        {
            for (int x = 0; x < Edges.Count; x++)
            {
                if (Edges[x].Keys[0].CompareTo(edge.Keys[0])>0)
                {
                    Edges.Insert(x, edge);
                    return;
                }
            }

            Edges.Add(edge);
            edge.Parent = this;
        }
        public bool RemoveEdge(NodoS n)
        {
            return Edges.Remove(n);
        }
        public NodoS RemoveEdge(int position)
        {
            NodoS edge = null;
            if (Edges.Count > position)
            {
                edge = Edges[position];
                edge.Parent = null;
                Edges.RemoveAt(position);
            }

            return edge;
        }
        public NodoS GetEdge(int position)
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
        public int FindEdgePosition(string k)
        {
            if (Keys.Count != 0)
            {
                string left = " ";
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (left.CompareTo(k)<0 && k.CompareTo(Keys[x])<0)
                    {
                        return x;
                    }
                    else
                    {
                        left = Keys[x];
                    }
                }

                if (k.CompareTo(Keys[Keys.Count - 1])>0)
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
        public void Fuse(NodoS n1)
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
                string k = n1.Keys[x];
                this.Push(k);
            }

            for (int x = Edges.Count - 1; x >= 0; x--)
            {
                NodoS e = n1.RemoveEdge(x);
                this.InsertEdge(e);
            }
        }

        public void Fuse(NodoS n1, NodoS n2)
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
        public NodoS[] Split()
        {
            if (Keys.Count != 2)
            {
                throw new InvalidOperationException(string.Format("This node has {0} keys, can only split a 2 keys node", Keys.Count));
            }

            NodoS newRight = new NodoS(Keys[1]);

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

            return new NodoS[] { this, newRight };
        }

        public string Pop(int position)
        {
            if (Keys.Count == 1)
            {
                throw new InvalidOperationException("Cannot pop value from a 1 key node");
            }

            if (position < Keys.Count)
            {
                string k = Keys[position];
                Keys.RemoveAt(position);

                return k;
            }

            return null;
        }

        public void Push(string k)
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
                string left = " ";
                for (int x = 0; x < Keys.Count; x++)
                {
                    if (left.CompareTo(k)<0 && k.CompareTo(Keys[x])<0)
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
        public NodoS Traverse(string k)
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