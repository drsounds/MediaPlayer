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
    public partial class ViewStack : UserControl
    {
        public ViewStack()
        {
            InitializeComponent();
            this.Views = new List<View>();
        }
        public void Navigate(string uri)
        {
            foreach(View v in this.Views)
            {
                if (v.AcceptsUri(uri))
                {
                    v.Show();
                    v.Navigate(uri);
                } else
                {
                    v.Hide();
                }
            }
        }
        public List<View> Views;
        public void AddView(View view)
        {
            this.Controls.Add(view);
            this.Views.Add(view);
            view.Dock = DockStyle.Fill;
            view.Hide();
        }

        private void ViewStack_Load(object sender, EventArgs e)
        {

        }
    }
}
