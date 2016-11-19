using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace quikUnion
{
    // flatten as we find the root. 
    public class SocialNetworkConnectivity : unionCompressionWeighted
    {
        // N members
        // M timestamps
         
        public SocialNetworkConnectivity(int n) : base(n)
        {
        }

    }
}
