using MediaPlayer;
using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MediaPlayer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ImportMusicForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public void LoadMusic()
        {
            using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
            {
                var tracks = dbContext.Tracks;
                listView1.ReloadListView(tracks);
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadMusic();
        }
    }
    public static class Utils
    {
        public static void ReloadListView(this ListView listView, DbSet<Track> objs) 
        {
            listView.Items.Clear();
            foreach (Track obj in objs)
            {
                ListViewItem lvi = new ListViewItem();
                int i = 0;
                foreach (ColumnHeader ch in listView.Columns)
                {
                    string tag = (string)ch.Tag;
                    try
                    {

                        var value = obj.GetType().GetProperty(tag).GetValue(obj, null);
                        var val = "";
                        if (value.GetType() == typeof(float))
                        {
                            val = ((float)value).ToString("0,0.00");

                        }
                        else
                        {
                            val = value.ToString();
                        }
                        if (i == 0)
                        {
                            lvi.Text = val;

                            i++;
                            continue;
                        }
                        var subitem = lvi.SubItems.Add(val);
                        if (val == "Available amount")
                        {
                            lvi.Font = new Font(lvi.Font, FontStyle.Bold);
                        }
                        if (value.GetType() == typeof(float))
                        {
                            listView.Columns[lvi.SubItems.IndexOf(subitem)].TextAlign = HorizontalAlignment.Right;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (i == 0)
                        {
                            lvi.Text = "";
                            i++;
                            continue;
                        }
                        lvi.SubItems.Add("");

                    }
                    i++;
                }
                lvi.Tag = obj;
                listView.Items.Add(lvi);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }
    }
}
