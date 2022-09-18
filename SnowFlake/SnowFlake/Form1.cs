using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowFlake
{
    public partial class Form1 : Form
    {
        private readonly IList<Snow> snowflakes;
        private readonly Timer timer;
        Bitmap background;
        Bitmap snow;
        int speed = 0;
        int n = 0;
        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snow>();
            background = new Bitmap(Properties.Resources.Background);
            snow = new Bitmap(Properties.Resources.Snow1);
            AddCreateSnow();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach(var snowflake in snowflakes)
            {
               
                snowflake.Y += snowflake.Severity  + speed;
                if (snowflake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflake.Y = -snowflake.Severity;
                }
                Draw();
            }
            timer.Start();
        } 
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void AddCreateSnow()
        {
            var rnd = new Random();
            for(int i = 0; i < 100; i++)
            {
                snowflakes.Add(new Snow
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Severity = rnd.Next(5, 30)

                });

            }
            
        }

        private void Draw()
        {
            var scene = new Bitmap(background,
                ClientRectangle.Width,
                ClientRectangle.Height);
            var gr = Graphics.FromImage(scene);
            foreach(var snowflake in snowflakes)
            {
            gr.DrawImage(snow, new Rectangle(
                snowflake.X,
                snowflake.Y,
                snowflake.Severity,
                snowflake.Severity));
            }
            

            var g = this.CreateGraphics();
            g.DrawImage(scene, 0, 0);

        }

      
        private void Form1_Click(object sender, EventArgs e)
        {
            if (n == 0)
            {
                timer.Start();
                n++;
                Console.WriteLine("Сейчас Start");
            }
            else if (n == 1)
            {
                speed = 25;
                n++;
                Console.WriteLine("Сейчас Start c доп скоростью");
            }
            else if (n == 2)
            {
                timer.Stop();
                n = 0;
                Console.WriteLine("Сейчас Stop");
            }
           
        }
    }
}
