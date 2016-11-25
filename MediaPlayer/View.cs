using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bungalow
{
    [Serializable]
    public partial class View : UserControl
    {
        public MainForm MainForm { get; private set; }

        public virtual bool AcceptsUri(string uri)
        {
            return false;
        }
        public virtual void Navigate(string uri)
        {

        }
        public View()
        {
            InitializeComponent();
        }

        public View(MainForm mainForm)
        {
            InitializeComponent();
            this.MainForm = mainForm;
        }
        private void View_Load(object sender, EventArgs e)
        {

        }
    }
}
