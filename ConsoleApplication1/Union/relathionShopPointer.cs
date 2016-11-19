using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace quikUnion
{
    // flatten as we find the root. 
    public class unionCompressionWeightedRelationshippointer : weightedUnion
    { 
        public unionCompressionWeightedRelationshippointer(int n) : base(n)
        {
        }

        public override Tuple<int, int> root(int i)
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

        protected override Tuple<int, int> rootHelper(int currentParent, int depth)
        {
            depth++;

            if (ids[currentParent] == currentParent)
            {
                return new Tuple<int, int>(currentParent, depth);
            }

            var parent = ids[currentParent];
            var grandParent = ids[parent];

            // may as well move the current node's parent to point to the grand parent.
            ids[currentParent] = grandParent;

            /*
             * if "8th item points to 99 pass 99 in to get 99ths root and so on"
             * */
            var nextParent = ids[currentParent];
            return rootHelper(nextParent /* next level */, depth);
        }

        public override void union(int p, int q)
        {
            base.union(p, q);

        }
    }
}
