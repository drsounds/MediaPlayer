using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class ColorChooser : Form
    {
        public ColorChooser()
        {
            InitializeComponent();
        }
        public ColorChooser(MainForm mainForm)
        {
            this.MainForm = mainForm;
            InitializeComponent();
        }

        public MainForm MainForm { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ColorChooser_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.Hue = trackBar1.Value ;
            Properties.Settings.Default.Save();
            MainForm.Colorize(MainForm);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            Properties.Settings.Default.Saturation = trackBar2.Value ;
            Properties.Settings.Default.Save();
            MainForm.Colorize(MainForm);
        }
    }
}
