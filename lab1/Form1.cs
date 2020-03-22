using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace lab1
{
    public partial class Form1 : Form
    {
        // инициализация формы
        public Form1()
        {
            InitializeComponent();
        }

        int[] mass = new int[10];
        int i = 0;

        // загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = @"E:\4 курс\операционные системы\lab1\123.txt";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    mass[i] = Convert.ToInt32(line);
                    i++;
                }
            }
            Scheduler.full_memory = mass[0];
            Scheduler.check = checkBox1.Checked;
        }

        // таймер1
        private void timer1_Tick(object sender, EventArgs e)
        {
            Scheduler.Simulate();
            textBox5.Text = "";
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            for (int i = 0; i < 3; i++)
            {
                foreach (Process proc in Scheduler.queue[i])
                {
                    switch (i)
                    {
                        case 0:
                            textBox2.Text += $"ID процесса: {proc.id} " + Environment.NewLine +
                            $"Номер очереди: {proc.queue}" + Environment.NewLine +
                            $"Кванты процесса: {proc.timequant}" + Environment.NewLine +
                            $"Статус процесса: {proc.status}" + Environment.NewLine +
                            $"Приоритет процесса: {proc.priority}" + Environment.NewLine +
                            $"Память процесса: {proc.memorysize}" + Environment.NewLine +
                            Environment.NewLine;
                            break;
                        case 1:
                            textBox3.Text += $"ID процесса: {proc.id} " + Environment.NewLine +
                            $"Номер очереди: {proc.queue}" + Environment.NewLine +
                            $"Кванты процесса: {proc.timequant}" + Environment.NewLine +
                            $"Статус процесса: {proc.status}" + Environment.NewLine +
                            $"Приоритет процесса: {proc.priority}" + Environment.NewLine +
                            $"Память процесса: {proc.memorysize}" + Environment.NewLine +
                            Environment.NewLine;
                            break;
                        case 2:
                            textBox4.Text += $"ID процесса: {proc.id} " + Environment.NewLine +
                            $"Номер очереди: {proc.queue}" + Environment.NewLine +
                            $"Кванты процесса: {proc.timequant}" + Environment.NewLine +
                            $"Статус процесса: {proc.status}" + Environment.NewLine +
                            $"Приоритет процесса: {proc.priority}" + Environment.NewLine +
                            $"Память процесса: {proc.memorysize}" + Environment.NewLine +
                            Environment.NewLine;
                            break;
                    }
                }
            }

            if (Scheduler.executeable_process != null)
                textBox5.Text +=
                        $"ID процесса: {Scheduler.executeable_process.id} " + Environment.NewLine +
                        $"Номер очереди: {Scheduler.executeable_process.queue}" + Environment.NewLine +
                        //$"Кванты процесса: {Scheduler.executeable_process.timequant}" + Environment.NewLine +
                        $"Статус процесса: {Scheduler.executeable_process.status}" + Environment.NewLine +
                        $"Приоритет процесса: {Scheduler.executeable_process.priority}" + Environment.NewLine +
                        $"Память процесса: {Scheduler.executeable_process.memorysize}" + Environment.NewLine +
                        $"Команды процесса: {Scheduler.executeable_process.strstr}" + Environment.NewLine +
                        Environment.NewLine;

            textBox1.Text = $"{Scheduler.occupied_memory} / {Scheduler.full_memory}";

            if (Scheduler.executeable_process != null)
            {
                textBox7.Text = $"{Scheduler.str}; k = {Scheduler.executeable_process.com1}" ;
            }
            else
            {
                textBox7.Clear();
            }

        }

        // кнопка добавить процесс
        private void button1_Click(object sender, EventArgs e)
        {
            Scheduler.AddProcess();
        }

        // кнопка вкл/выкл
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        // кнопка увеличить скорость
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = Math.Max(50, timer1.Interval - 50);
        }

        // кнопка уменьшить скорость
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Interval = Math.Min(1000, timer1.Interval + 50);
        }

        // таймер2
        private void timer2_Tick(object sender, EventArgs e)
        {
            Scheduler.AddProcess();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        // чек вкл/выкл мультипрограммный режим
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Scheduler.check = checkBox1.Checked;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Scheduler.BlockExProcess();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
