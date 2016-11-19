using ConsoleApplication1.Quiz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quikUnion
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            //========================================================
            slowUnion su = new slowUnion(6000);
            quickunion qu = new quickunion(6000);
            weightedUnion wu = new weightedUnion(6000);
            unionCompressionWeighted uc = new unionCompressionWeighted(6000);

            Stopwatch w = new Stopwatch();
            w.Start();
            for (int i = 0; i < 6000; i++)
            {
                su.union(r.Next(6000), r.Next(6000));
            }
            for (int i = 0; i < 6000; i++)
            {
                su.connected(r.Next(6000), r.Next(6000));

            }


            Console.WriteLine(w.Elapsed);

            //========================================================
            w = new Stopwatch();
            w.Start();
            for (int i = 0; i < 6000; i++)
            {
                qu.union(r.Next(6000), r.Next(6000));
            }

            for (int i = 0; i < 6000; i++)
            {
                qu.connected(r.Next(6000), r.Next(6000));
            }

            // qu.Print();

            Console.WriteLine(w.Elapsed);

            //========================================================
            w = new Stopwatch();
            w.Start();

            // union items
            for (int i = 0; i < 6000; i++)
            {
                wu.union(r.Next(6000), r.Next(6000));
            }
            // qu.Print();

            for (int i = 0; i < 60000; i++)
            {
                wu.connected(r.Next(6000), r.Next(6000));
            }

            Console.WriteLine(w.Elapsed);

            // this dept can't be bigger tahn log (2) 6000 should be 8 max.


            //========================================================
            w = new Stopwatch();
            w.Start();

            // union items
            for (int i = 0; i < 6000; i++)
            {
                uc.union(r.Next(6000), r.Next(6000));
            }
            // qu.Print();

            for (int i = 0; i < 60000; i++)
            {
                uc.connected(r.Next(6000), r.Next(6000));
            }

            Console.WriteLine(w.Elapsed);

            // this dept can't be bigger tahn log (2) 6000 should be 8 max. but smaller because of teh compressions

            // keep these down here to avoid the per hit.
            qu.Metrics();
            wu.Metrics();
            uc.Metrics();

            // N members
            // M timestamps

            // 1 k members
            int n = 1000;


            // 100 relationships these are teh relationshops between members
            // var m = GetTimeStamps(100);

            // connect members 100 for the time stamps. find earliest common root.
            var memberCOunt = 10; // tecnically + 1 i don't care.


            var p = new PeopleGenerator();
            var people = p.Generate(memberCOunt);
            p.Print(people);

            var sortedMemberRelation = people.OrderBy(i => i.RelationShipTime).ToList();
            p.Print(sortedMemberRelation);
      
            // all of these members. Draw connections between them at various times
            quickunion u = new quickunion(memberCOunt);
            foreach(var memberRelation in sortedMemberRelation)
            {
                u.union(memberRelation.Member1, memberRelation.Member2);
            }

            // Get the earliest time. for a relationshop of the network of people.
            /*
             * design an algorithm to determine the earliest time at which all members are connected (i.e., every member is a friend of a friend of a friend ... of a friend). Assume that the log 
             * */
             for(int i=0;i<memberCOunt; i++)
            {
                Tuple<int, int> root = u.root(i);
                Console.WriteLine("Item: {0} Root: {1}", i, root.Item1);
            }
        }




    }
     
 
}


