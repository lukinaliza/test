using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace lab1
{
    enum Status { Ready, Done, Active, Blocked, Paused }    // возможные состояния процесса

    class Process
    {
       // public int id;              // id процесса
        public Status status;       // состояние процесса
        public int priority;        // приоритет процесса
        public int timequant = 0;   // кол-во квантов, за который выполняется процесс
        static int id_gen = 0;      // переменная для генерации последовательного id
        public int memorysize;      // кол-во операций процесса
        public int[] memory;        // массив операций процесса
        public int queue;           // номер очереди процесса
        public string strstr = "";
        public int kk;

        // конструктор
        public Process()
        {
            Random rand = new Random();
            id = id_gen;
            id_gen++;
            kk = 0;
            status = Status.Ready;
            memorysize = rand.Next(3, 10);
            priority = rand.Next(100, 1000);
            //timequant = rand.Next(2, 6);
            memory = new int[memorysize];
            timequant = 0;
            for (int i=0; i < memorysize; i++)
            {
                memory[i] = rand.Next(0, 2);
                if (memory[i] == 0)
                {
                    strstr += memory[i];
                    timequant++;
                }
                if (memory[i] == 1)
                {
                    strstr += memory[i];
                    timequant++;
                    timequant++;
                    timequant++;
                }
            }
            
            Thread.Sleep(20);
        }

        public int com1 = 0;
        public int executable_command = -1;

        // симуляция процесса
        public void Simulate()
        {
            if(status != Status.Active && status != Status.Blocked)
            {
                //throw new Exception("sadasd");
            }

            if (com1 != 0)
            {
                com1--;
                return;
            }

            if (kk == this.memorysize)
            {
                status = Status.Done;
                return;
            }

            executable_command = memory[kk];
            if (executable_command == 1)
            {
                com1 = 3;
            }
            kk++;
        }
    }
}
