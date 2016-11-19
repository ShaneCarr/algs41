using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// better makes an effort to walk up the tree for reas in an effecient manner by walking the relevant nodes.
// makes the tree ballanced because it always inserts the smaller tree below the larger tree. 
// note because the larger tree must be greater than or equal the number of nodes in the tree you are adding. 
// depth increases by 1 when its tree is merged into another tree we only do that if t2 is bigger than x's tree t1. so if the x increases it at least doubles.
// if it doubles and only has n elements lg N is the max it can double. 
// note max(depth) <= lg N for testing.
/*
 * when does x's depth increas
 * when x's tree is merged niot another. we oly do that when we are merging into a new tree, which is at least teh size of teh current. so ti is doubling each time, at least. Lg n is max height.
 * */
namespace quikUnion
{
    public class weightedUnion
    {
        public int[] ids;

        // hash table. i could have done this wtih a 2d array too. where the rowsvalue in zero is the 
        // root, and the values after are the items in the group. 
        public Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();


        public int FindLarget(int i)
        {
            var r = this.root(i);
            if (this.groups.ContainsKey(r.Item1))
            {
                return groups[r.Item1].Max();
            }

            return r.Item1;
        }

        public weightedUnion(int n)
        {
            this.ids = new int[n];
            for (int i = 0; i < ids.Length; i++)
            {
                this.ids[i] = i;
            }
        }

        public virtual Tuple<int, int> root(int i)
        {

            int debth = 0;
            if (ids[i] == i)
            {
                return new Tuple<int, int>(i, debth);
            }

            debth++;

            /*
             * if "8th item points to 99 pass 99 in to get 99ths root and so on"
             * */

            return rootHelper(ids[i] /* next level */, debth);
        }

        protected virtual Tuple<int, int> rootHelper(int i, int depth)
        {
            depth++;

            if (ids[i] == i)
            {
                return new Tuple<int, int>(i, depth);
            }

            /*
             * if "8th item points to 99 pass 99 in to get 99ths root and so on"
             * */
            return rootHelper(ids[i] /* next level */, depth);
        }

        public virtual bool connected(int p, int q)
        {
            return root(p).Item1 == root(q).Item1;
        }

        public virtual void union(int p, int q)
        {
            var rootP = root(p);
            var rootQ = root(q);


            // if p's depth is less than qs point p to q. This minimizes the size
            // if q's dept is less then use point to to P's root. 
            // this is becase your starting point the root it is alwasy better to add to teh smaller of the two. rather than the big one.
            // link root to the smaller tree to the larger.
            if (rootP.Item2 < rootQ.Item2)
            {
                // i could also create a data structure 
                // instead of just numbers and when i set p to q
                // set q to p. this would give a two direction node.
                //p poits to q
                ids[rootP.Item1] = rootQ.Item1;

                UpdateGroups(q, p, rootQ, rootP);

            }
            else
            {
                ids[rootQ.Item1] = rootP.Item1;

                UpdateGroups(p, q, rootP, rootQ);
            }

        }

        private void UpdateGroups(int parent, int child, Tuple<int, int> newRoot, Tuple<int, int> oldRoot)
        {
            // keep the dictionary of q to hold p's data
            // update group.
            if (!groups.ContainsKey(newRoot.Item1))
            {
                groups.Add(newRoot.Item1, new List<int>());
                groups[newRoot.Item1].Add(parent);
            }

            // keep the dictionary of p to hold q's data
            // update group.
            if (!groups.ContainsKey(newRoot.Item1))
            {
                groups.Add(newRoot.Item1, new List<int>());
            }

            if (groups.ContainsKey(oldRoot.Item1))
            {
                groups[newRoot.Item1].AddRange(groups[oldRoot.Item1]);
            }
            
            // don't need q anymore
            if (groups.ContainsKey(oldRoot.Item1) && oldRoot.Item1!= newRoot.Item1)
            {
                groups.Remove(oldRoot.Item1);
            }

            groups[newRoot.Item1].Add(parent);
            groups[newRoot.Item1].Add(child);
            groups[newRoot.Item1] = groups[newRoot.Item1].Distinct().ToList();
        }

        public void Print()
        {
            for (int i = 0; i < ids.Length; i++)
            {
                Console.WriteLine("id: " + i + " " + ids[i]);
            }
        }


        public void Metrics()
        {
            List<int> d = new List<int>();
            for (int i = 0; i < ids.Length; i++)
            {
                var ele = this.root(i);
                d.Add(ele.Item2);
            }

            Console.WriteLine("Average depth: " + d.Average());
        }
    }
}
