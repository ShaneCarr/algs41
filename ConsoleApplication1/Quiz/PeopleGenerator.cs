using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Quiz
{
    public class PeopleGenerator
    {
        public static Random r = new Random((int)DateTime.Now.Ticks);
        public List<MemberTimeStamprelationShip> Generate(int memberCOunt )
        {
            List<MemberTimeStamprelationShip> members = new List<MemberTimeStamprelationShip>();
            // for half the people generate random relationships at random time.
            Random ra = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < memberCOunt / 2; i++)
            {
                
                int m1 = ra.Next(0, memberCOunt);
                int m2 = ra.Next(0, memberCOunt);

                while (m1 == m2)
                {
                    m2 = ra.Next(0, memberCOunt);
                }

                MemberTimeStamprelationShip member = new MemberTimeStamprelationShip()
                {
                    Member1 = m1,
                    Member2 = m2,
                    RelationShipTime = GetTimeStamp()
                };

                members.Add(member);
            }

            return members;
        }
         public void Print(List<MemberTimeStamprelationShip> people )
        {
            Console.WriteLine("-===========================");
            foreach(var p in people)
            {
                Console.WriteLine("A relationShip:: Member1: {0} Member2: {1} TimeStamp: {2}", p.Member1, p.Member2, p.RelationShipTime);
            }
        }
        public static DateTime GetTimeStamp()
        {
            var time = DateTime.UtcNow;
            

            // add some random time
            var x = time.AddMinutes(0).AddMilliseconds((int)r.Next()).AddSeconds((int)r.Next());
            return x;
        }
    }
}
