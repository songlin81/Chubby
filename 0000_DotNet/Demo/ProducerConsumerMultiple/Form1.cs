using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;  

namespace ProducerConsumerMultiple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Instance to encapsulate any specific task to use.
        public class TaskInfo
        {
            public int Index;

            public TaskInfo()
            {
            }

            public TaskInfo(int index)
            {
                this.Index = index;
            }
        }

        private static bool StopFlag = false;
        private int ProducerNumber = 1;
        private int ConsumerNumber = 3;

        private Queue<TaskInfo> TaskQueue = new Queue<TaskInfo>();
        private Semaphore TaskSemaphore = new Semaphore(0, 256);

        private object myLock = new object();

        private void StartDemo()
        {
            StartProducer(ProducerNumber);
            Thread.Sleep(2000);
            StartConsumer(ConsumerNumber);
        }

        // Establish a single producer in this demo
        private void StartProducer(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Thread t = new Thread(new ThreadStart(Producer));
                t.Name = "Producer_" + (i + 1);
                t.Start();
            }
        }

        // Establish three consumers.
        private void StartConsumer(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(this.Consumer));
                t.Name = "Consumer_" + (i + 1);
                t.IsBackground = true;
                t.Start(i + 1);
            }
        }

        // Use delegate to get access to UI thread controls.
        public delegate void ProducerTextDelegate(string AddStr);
        public delegate void ComsumerTextDelegate(int Index, string AddStr);

        private void Producer()
        {
            int i = 0;
            while (!StopFlag)
            {
                TaskInfo Task = new TaskInfo(++i);
                lock (myLock)
                {
                    TaskQueue.Enqueue(Task);
                }
                TaskSemaphore.Release(1);

                string AddText = String.Format("Task {0} was Produced.\r\n", Task.Index);
                ChangeProducerText(AddText);

                Thread.Sleep(2000);
            }
        }

        private void ChangeProducerText(string AddStr)
        {
            if (textBox1.InvokeRequired)
            {
                ProducerTextDelegate pd = new ProducerTextDelegate(ChangeProducerText);
                textBox1.Invoke(pd, new object[] { AddStr });
            }
            else
            {
                textBox1.AppendText(AddStr);
            }
        }

        private void Consumer(object data)
        {
            int Index = (int)data;
            TaskInfo GetTask = null;

            while (true)
            {
                TaskSemaphore.WaitOne();
                lock (myLock)
                {
                    GetTask = TaskQueue.Dequeue();
                }

                string AddStr = String.Format("Consumer {0} take Task {1}\r\n", Index, GetTask.Index);

                ChangeConsumerText(Index, AddStr);
                Random r = new Random();
                Thread.Sleep(r.Next(5) * 1000);     // Random to simulate time for processing each consumer's task.
                                                    // Add real biz logic here to consume the task.
            }
        }

        private void ChangeConsumerText(int Index, string AddStr)
        {
            TextBox currentTextBox = null;
            switch (Index)
            {
                case 1:
                    currentTextBox = this.textBox2;
                    break;
                case 2:
                    currentTextBox = this.textBox3;
                    break;
                case 3:
                    currentTextBox = this.textBox4;
                    break;
                default:
                    break;
            }

            if (currentTextBox.InvokeRequired)
            {
                ComsumerTextDelegate cd = new ComsumerTextDelegate(ChangeConsumerText);
                currentTextBox.Invoke(cd, new object[] { Index, AddStr });
            }
            else
            {
                currentTextBox.AppendText(AddStr);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StopFlag = false;
            StartDemo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopFlag = true;
        }  
    }
}
