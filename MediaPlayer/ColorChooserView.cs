using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bungalow
{
    public partial class ColorChooserView : View
    {
        public ColorChooserView()
        {
            InitializeComponent();
        }
        public ColorChooserView(MainForm mainForm)
        {
            this.MainForm = mainForm;
            InitializeComponent();
        }

        public MainForm MainForm { get; private set; }
        public override bool AcceptsUri(string uri)
        {
            return new Regex("urn:color:chooser").IsMatch(uri);
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void ColorChooser_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.Hue = trackBar1.Value ;
            Properties.Settings.Default.Save();
            MainForm.Colorize();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            Properties.Settings.Default.Saturation = trackBar2.Value ;
            Properties.Settings.Default.Save();
            MainForm.Colorize();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Light = checkBox1.Checked;
            Properties.Settings.Default.Save();
            MainForm.Colorize();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default.AlternatingRows = checkBox2.Checked;
            Properties.Settings.Default.Save();
            MainForm.Colorize();
        }
    }
}
