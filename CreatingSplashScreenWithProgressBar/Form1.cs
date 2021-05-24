using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatingSplashScreenWithProgressBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread t=new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(11600);
            InitializeComponent();
            t.Abort();
        }

        public void SplashStart()
        {
            Application.Run(new SplashScreen());
        }
    }
}
