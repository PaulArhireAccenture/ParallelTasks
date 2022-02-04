using System;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Paralel
{
    
    class Program
    {
        static ConcurrentQueue<TimestampDTO> queue = new ConcurrentQueue<TimestampDTO>();
        static bool canStartC = false;
        static void Main(string[] args)
        {
            Task taskA = Task.Run(() => {
                //while (!canAdd) { }
                //canAdd = false;
                for (int i = 0; i < 100; i++)
                {
                    queue.Enqueue(new TimestampDTO(1));
                    canStartC = true;
                }
                
                //canAdd = true;
            });
            
            Task taskB = Task.Run(() => {
                //while (!canAdd) { }
                //canAdd = false;
                for (int i = 0; i<100; i++)
                {
                    queue.Enqueue(new TimestampDTO(2));
                }
                
                //canAdd = true;
            });
            Task taskC = Task.Run(() => {
                ArrayList list = new ArrayList();
                while (!canStartC) { }
                while (queue.Count != 0)
                {
                    queue.TryDequeue(out TimestampDTO aux);
                    list = aux.list;
                    list.Sort();
                    double avg = (from int number in list select number).Average();
                    int count = (from int number in list where number > avg select number).Count();
                    
                    DateTime currTime = DateTime.Now;
                    
                    Console.WriteLine("The DTO with senderID: " + aux.senderID + " has " + count + " elements over " + avg);
                    Console.WriteLine("The process took: " + (currTime - aux.timestamp) + " seconds");
                }
            });
            taskA.Wait();
            taskB.Wait();
            taskC.Wait();

        }
    }
}
