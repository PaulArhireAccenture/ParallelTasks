using System;
using System.Linq;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Paralel
{
    class TimestampDTO
    {

        public DateTime timestamp;
        public int elementID;
        public int senderID;
        public int[] list = new int[10];
    
        public TimestampDTO(int senderID)
        {
            this.senderID = senderID;
            this.timestamp = DateTime.Now;
            this.elementID = new Random().Next(1000,9999);
            for(int i = 0; i < 10; i++)
            {
                this.list[i] = new Random().Next(1,100);
            }
        }
    }
    class Program
    {
        static Queue queue = new Queue();
        static bool canAdd = true;
        static void Main(string[] args)
        {
            Task taskA = Task.Run(() => {
                while (!canAdd) { }
                canAdd = false;
                queue.Enqueue(new TimestampDTO(1));
                canAdd = true;
            });
            Task taskB = Task.Run(() => {
                while (!canAdd) { }
                canAdd = false;
                queue.Enqueue(new TimestampDTO(2));
                canAdd = true;
            });
            Task taskC = Task.Run(() => {
                ArrayList list = new ArrayList();
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>");
                while (!canAdd) { }
                while (queue.Count != 0)
                {
                    TimestampDTO aux = (TimestampDTO)queue.Dequeue();
                    foreach(int i in aux.list)
                    {
                        list.Add(i);
                    }
                    list.Sort();
                    double avg = 0;
                    int count = 0;
                    foreach(int i in list)
                    {
                        avg += i;
                    }
                    avg = avg / list.Count;
                    foreach(int i in list)
                    {
                        if(i > avg)
                        {
                            count++;
                        }
                    }
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
