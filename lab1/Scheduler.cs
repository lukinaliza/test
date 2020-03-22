using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    static class Scheduler
    {
        static public Process executeable_process;      // выполняемый процесс
        static int current_queue = 2;                   // текущая очередь
        static int numofqueue = 0;                      // номер очереди
        static public int full_memory = 500;             // память ОС
        static public string str = "";
        static public int k = 0;
        static public int occupied_memory = 0;          // используемая память
        static public int delta = 0;                    // дельта (задержка выполнения процесса)
        static public bool check = false;               // переключатель мультипрограммного режима
        static public List<Process>[] queue = new List<Process>[3]      // список очередей
        {
            new List<Process>(),
            new List<Process>(),
            new List<Process>()
        };
        
        // симуляция
        static public void Simulate()
        {
            // для мультипрограммного режима
            if (delta > 0)
            {
                delta--;
                return;
            }

            if (executeable_process != null && delta == 0)
            {
                delta = -1;

                executeable_process.status = Status.Active;
                return;
            }

            if (executeable_process == null &&
                queue[0].Count == 0 &&
                queue[1].Count == 0 &&
                queue[2].Count == 0
                )
            {
                return;
            }

            if (executeable_process == null)
            {
                ChooseProcces();
                if (executeable_process == null)
                {
                    return;
                }

                if(check)
                {
                    delta = 10;
                    return;
                }
                else
                    executeable_process.status = Status.Active;
            }

            if (executeable_process.status == Status.Blocked)
            {
                numofqueue = (numofqueue + 1) % 3;
                queue[numofqueue].Add(executeable_process);
                ChooseProcces();
            }

            if (executeable_process.status == Status.Done)
            {
                occupied_memory -= executeable_process.memorysize;
                ChooseProcces();
                if (executeable_process == null)
                {
                    return;
                }

                if (check)
                {
                    delta = 10;
                    return;
                }
                else
                {
                    executeable_process.status = Status.Active;
                }
            }
             if (executeable_process.executable_command == 0)
             {
                    str = "Запись в память";
             }
             if (executeable_process.executable_command == 1)
             {
                 str = "Арифметические операции";
             }
            executeable_process.Simulate();
            k++;
        }

        // добавление процесса
        static public void AddProcess()
        {
            Process process = new Process();
            if (occupied_memory + process.memorysize >= full_memory)
            {
                return;
            }
            process.queue = numofqueue;
            // выбрать очередь
            queue[numofqueue].Add(process);
            numofqueue = (numofqueue + 1) % 3;
            occupied_memory += process.memorysize;
        }

        // выборка процесса
        static public void ChooseProcces()
        {
            current_queue = (current_queue + 1) % 3;

            if (queue[current_queue].Count == 0)
            {
                current_queue = (current_queue + 1) % 3;
            }
            if (queue[current_queue].Count == 0)
            {
                current_queue = (current_queue + 1) % 3;
            }
            if (queue[current_queue].Count == 0)
            {
                executeable_process = null;
                return;
            }


            queue[current_queue] = queue[current_queue].OrderByDescending(i => i.priority).ToList();
            executeable_process = queue[current_queue][0];
            queue[current_queue].RemoveAt(0);
        }

        // блокировка процесса
        static public void BlockExProcess()
        {
            executeable_process.status = Status.Blocked;
        }

    }
}
