using ColorDemo;
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
            this.MusicServices.Add(new MediaPlayerMusicService(this.axWindowsMediaPlayer1));
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
            try
            {
                using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
                {
                    var tracks = dbContext.Tracks;
                    listView1.ReloadListView(tracks);
                    Colorize(listView1);
                }
            }
            catch (Exception e)
            {
               
            }
        }

        public void Colorize(Control c)
        {
            var GlobalBackColor = AdjustColor(Properties.Settings.Default.ForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
            this.BackColor = GlobalBackColor;
            try
            {
                if (c.GetType() == typeof(ListView))
                {
                    ListView listView = (ListView)c;
                    listView.BackColor = AdjustColor(Properties.Settings.Default.BackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); ;
                    listView.ForeColor = AdjustColor(Properties.Settings.Default.ForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); 
                    int i = 0;
                    foreach (ListViewItem item in listView.Items)
                    {
                        if ((i % 2) == 0)
                        {
                            item.BackColor = AdjustColor(Properties.Settings.Default.AlternateRowColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); ;
                        }
                        else
                        {

                            item.BackColor = AdjustColor(Properties.Settings.Default.RowColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); 
                        }
                        i++;
                    }
                }
                if (c.GetType() == typeof(TreeView))
                {
                    TreeView treeView = (TreeView)c;
                    treeView.BackColor = AdjustColor(Properties.Settings.Default.BackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                    treeView.ForeColor = AdjustColor(Properties.Settings.Default.ForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                }
                foreach (Control t in c.Controls)
                {

                    Colorize(t);
                }
            }
            catch (Exception e)
            {

            }
        }
        public Color AdjustColor(Color color, float hue, float saturation)
        {

            HSLColor hslColor = new HSLColor(color);
            hslColor.Hue += hue - 140;
            hslColor.Saturation += saturation;
           
            return hslColor;
        }
        public List<IMusicService> MusicServices = new List<IMusicService>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            Colorize(this);
            LoadMusic();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ColorChooser(this).Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];
                Track t = (Track)item.Tag;
                foreach(IMusicService m in this.MusicServices)
                {
                    if (m.Play(t.Name, t.Artist, t.Album))
                    {
                        this.MusicService = m;
                        break;
                    }
                }
            }
        }
        public IMusicService MusicService { get; set; }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

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
