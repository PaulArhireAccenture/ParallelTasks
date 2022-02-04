using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Paralel
{
    class TimestampDTO
    {
        public DateTime timestamp;
        public int elementID;
        public int senderID;
        public ArrayList list = new ArrayList();

        public TimestampDTO(int senderID)
        {
            this.senderID = senderID;
            this.timestamp = DateTime.Now;
            this.elementID = new Random().Next(1000, 9999);
            for (int i = 0; i < 10; i++)
            {
                this.list.Add(new Random().Next(1, 100));
            }
        }
    }
}
