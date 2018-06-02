using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;



namespace Timer_HW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task taskFirst = new Task(Count);
            taskFirst.Start();
            taskFirst.Wait();
            MessageBox.Show("Process was completed");
        }

        public void Count()
        {
            double x = 50;
            double j;    
                for (double i = 1; i <= x; i++)
                {
                    j = i / x * 100;
                    this.label1.Text = $"This process is performed on {j}%";
                    
                    Task taskSecond = Task.Factory.StartNew(() =>
                    {
                        this.progressBar1.Value = (int)j;
                    }, TaskCreationOptions.AttachedToParent);
                    Thread.Sleep(100);
            }
        } 
    }
}
