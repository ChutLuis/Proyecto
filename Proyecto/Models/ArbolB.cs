using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class ArbolB
    {
        public Nodo Raiz { get; set; }
        public ArbolB(){
            Raiz = null;
            }
        public void Insertar(int value)
        {
            if (Raiz==null)
            {
                Raiz = new Nodo(value);
                return;
            }
            Nodo actual = Raiz;
            Nodo padre = null;
            while (actual!= null)
            {
                if (actual.Keys.Count==3)
                {
                    if (padre==null)
                    {
                        int k = actual.Pop(1).Value;
                        Nodo nuevaRaiz = new Nodo(k);
                        Nodo[] newNodos = actual.Split();
                        nuevaRaiz.InsertEdge(newNodos[0]);
                        nuevaRaiz.InsertEdge(newNodos[1]);
                        Raiz = nuevaRaiz;
                        actual = nuevaRaiz;
                    }
                    else
                    {
                        int? k = actual.Pop(1);
                        if (k!= null)
                        {
                            padre.Push(k.Value);
                        }
                        Nodo[] nNodos = actual.Split();
                        int pos1 = padre.FindEdgePosition(nNodos[1].Keys[0]);
                        padre.InsertEdge(nNodos[1]);

                        int posActual = padre.FindEdgePosition(value);
                        actual = padre.GetEdge(posActual);

                    }
                }
                padre = actual;
                actual = actual.Traverse(value);
                if (actual==null)
                {
                    padre.Push(value);
                }
            }
        }
        public Nodo Find(int k)
        {
            Nodo curr = Raiz;

            while (curr != null)
            {
                if (curr.HasKey(k) >= 0)
                {
                    return curr;
                }
                else
                {
                    int p = curr.FindEdgePosition(k);
                    curr = curr.GetEdge(p);
                }
            }

            return null;
        }
        public void Remove(int k)
        {
            //1 if in the leaf node, simply remove it.
            //2 as we encounter 1 key nodes,
            // a) pull the key from the siblings if they have 2 or more keys, via rotation
            // b) if both siblings have only 1 key, the parent (except if it is root) will always have 2 or more keys, 
            //    so pull a key from parent and fuse with it's sibling.
            // c) if siblings have only 1 key and parent is a 1 key root node, fuse all 3 nodes into 1

            Nodo curr = Raiz;
            Nodo parent = null;
            while (curr != null)
            {
                //check for 1 key nodes
                if (curr.Keys.Count == 1)
                {
                    if (curr != Raiz)//skip root node
                    {
                        int cK = curr.Keys[0];
                        int edgePos = parent.FindEdgePosition(cK);

                        bool? takeRight = null;
                        Nodo sibling = null;

                        if (edgePos > -1)//edge is found
                        {
                            if (edgePos < 3)//use right sibling if it is not the right most node
                            {
                                sibling = parent.GetEdge(edgePos + 1);
                                if (sibling.Keys.Count > 1)
                                {
                                    takeRight = true;
                                }
                            }

                            if (takeRight == null)//if this is the right most node, or there wasn't any left sibling with >1 keys
                            {
                                if (edgePos > 0)//use left sibling if it is not the left most node
                                {
                                    sibling = parent.GetEdge(edgePos - 1);
                                    if (sibling.Keys.Count > 1)
                                    {
                                        takeRight = false;//use left
                                    }
                                }
                            }

                            if (takeRight != null)//case 2a) perform rotation with sibling
                            {
                                int? pK = 0;
                                int? sK = 0;

                                if (takeRight.Value)//take from right sibling
                                {
                                    pK = parent.Pop(edgePos).Value;//take parent's key (corresponding to this edge)
                                    sK = sibling.Pop(0).Value;//take sibling's left most key

                                    if (sibling.Edges.Count > 0)
                                    {
                                        Nodo edge = sibling.RemoveEdge(0);//move left most edge
                                        curr.InsertEdge(edge);
                                    }
                                }
                                else//take from left sibling
                                {
                                    pK = parent.Pop(edgePos).Value;//take parent's key (corresponding to this edge)
                                    sK = sibling.Pop(sibling.Keys.Count - 1).Value;//take sibling's right most key

                                    if (sibling.Edges.Count > 0)
                                    {
                                        Nodo edge = sibling.RemoveEdge(sibling.Edges.Count - 1);//move right most edge
                                        curr.InsertEdge(edge);
                                    }
                                }

                                parent.Push(sK.Value);
                                curr.Push(pK.Value);
                            }
                            else//case 2b) or 2c) no siblings with >1 keys available
                            {
                                int? pK = null;
                                if (parent.Edges.Count >= 2)//case 2b
                                {
                                    if (edgePos == 0)//if n is left most node, take parent's first key
                                    {
                                        pK = parent.Pop(0);
                                    }
                                    else if (edgePos == parent.Edges.Count)//if n is the right most node take parent's right most key
                                    {
                                        pK = parent.Pop(parent.Keys.Count - 1);
                                    }
                                    else//take parent's middle key
                                    {
                                        pK = parent.Pop(1);
                                    }

                                    if (pK != null)
                                    {
                                        curr.Push(pK.Value);
                                        Nodo sib = null;
                                        if (edgePos != parent.Edges.Count)//use right sibling if it is not the rightmost node
                                        {
                                            sib = parent.RemoveEdge(edgePos + 1);
                                        }
                                        else
                                        {
                                            sib = parent.RemoveEdge(parent.Edges.Count - 1);
                                        }

                                        curr.Fuse(sib);
                                    }
                                }
                                else//case 2c
                                {
                                    curr.Fuse(parent, sibling);
                                    Raiz = curr;
                                    parent = null;
                                }
                            }
                        }
                    }
                }

                int rmPos = -1;
                if ((rmPos = curr.HasKey(k)) >= 0)
                {
                    //if it is a leaf node, remove the key
                    if (curr.Edges.Count == 0)
                    {
                        if (curr.Keys.Count == 0)
                        {
                            parent.Edges.Remove(curr);
                        }
                        else
                        {
                            curr.Pop(rmPos);
                        }
                    }
                    else//otherwise, replace it with the next higher key
                    {
                        Nodo successor = Min(curr.Edges[rmPos]);
                        int sK = successor.Keys[0];
                        if (successor.Keys.Count > 1)
                        {
                            successor.Pop(0);
                        }
                        else
                        {
                            if (successor.Edges.Count == 0)//just remove it if it is leaf
                            {
                                Nodo p = successor.Parent;
                                p.RemoveEdge(successor);
                            }
                            else
                            {
                                //not leaf so we have to rotate
                            }
                        }
                    }

                    curr = null;
                }
                else
                {
                    //not found, so we move down the tree
                    int p = curr.FindEdgePosition(k);
                    parent = curr;
                    curr = curr.GetEdge(p);
                }
            }

        }
        public Nodo Min(Nodo n = null)
        {
            if (n == null)
            {
                n = Raiz;
            }

            Nodo curr = n;
            if (curr != null)
            {
                while (curr.Edges.Count > 0)
                {
                    curr = curr.Edges[0];
                }
            }

            return curr;
        }
        public int[] Inorder(Nodo n = null)
        {
            if (n == null)
            {
                n = Raiz;
            }

            List<int> items = new List<int>();
            Tuple<Nodo, int> curr = new Tuple<Nodo, int>(n, 0);
            Stack<Tuple<Nodo, int>> stack = new Stack<Tuple<Nodo, int>>();
            while (stack.Count > 0 || curr.Item1 != null)
            {
                if (curr.Item1 != null)//Case 1
                {
                    stack.Push(curr);
                    Nodo leftChild = curr.Item1.GetEdge(curr.Item2);//move to leftmost unvisited child
                    curr = new Tuple<Nodo, int>(leftChild, 0);
                }
                else//Case 2
                {
                    curr = stack.Pop();
                    Nodo currNode = curr.Item1;

                    //because for every node, it can possibly have more edges than key
                    //if the current index corresponds to a key, we want to add the key into the list.
                    //else we just want to traverse it's edges.
                    if (curr.Item2 < currNode.Keys.Count)
                    {
                        items.Add(currNode.Keys[curr.Item2]);
                        curr = new Tuple<Nodo, int>(currNode, curr.Item2 + 1);
                    }
                    else
                    {
                        Nodo rightChild = currNode.GetEdge(curr.Item2 + 1);//get the rightmost child, may be null

                        //if right most child is null, we will visit 'Case 2' again in the next loop,
                        //and the parent will be popped off the stack
                        curr = new Tuple<Nodo, int>(rightChild, curr.Item2 + 1);
                    }
                }
            }
            return items.ToArray();
        }
    }
}