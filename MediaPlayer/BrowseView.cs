using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Bungalow
{
    public partial class BrowseView : View
    {
        public override bool AcceptsUri(string uri)
        {
            return new Regex("urn:browse").IsMatch(uri);
        }
        public override void Navigate(string uri)
        {
            base.Navigate(uri);
        }
        public BrowseView()
        {
            InitializeComponent();
        }

        public BrowseView(MainForm mainForm) : base(mainForm)
        {
            InitializeComponent();
        }
        private void BrowseView_Load(object sender, EventArgs e)
        {

        }
    }
}
