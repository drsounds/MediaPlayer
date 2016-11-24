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



        private void button2_Click(object sender, EventArgs e)
        {

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
                   
                    if (Properties.Settings.Default.Light && listView.Name != "PlaylistListView")
                    {
                        
                        listView.BackColor = AdjustColor(Properties.Settings.Default.BackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                        listView.ForeColor = AdjustColor(Properties.Settings.Default.ForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                    }
                    else
                    {
                        listView.BackColor = AdjustColor(Properties.Settings.Default.DarkBackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation).Darken(.7);
                        listView.ForeColor = AdjustColor(Properties.Settings.Default.DarkForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                        if (listView.Name == "PlaylistListView")
                        {
                            listView.BackColor = listView.BackColor.Darken(1.8);
                        }
                    }
                    int i = 0;
                    foreach (ListViewItem item in listView.Items)
                    {
                        if (item.Tag == CurrentTrack)
                        {
                            item.BackColor = Color.Black;
                            item.ForeColor = Color.LightGreen;

                        }
                        else
                        {
                            item.BackColor = listView.BackColor;
                            item.ForeColor = listView.ForeColor;
                            if ((i % 2) == 0)
                            {
                                if (Properties.Settings.Default.AlternatingRows)
                                    if (Properties.Settings.Default.Light)
                                    {
                                        item.BackColor = AdjustColor(Properties.Settings.Default.AlternateRowColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); ;
                                        if (listView.Name == "PlaylistListView")
                                        {
                                            item.BackColor = listView.BackColor;
                                        }
                                    }
                                    else
                                    {
                                        item.BackColor = AdjustColor(Properties.Settings.Default.DarkAlternateRowColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation).Darken(.75);
                                        if (listView.Name == "PlaylistListView")
                                        {
                                            item.BackColor = listView.BackColor;
                                        }
                                    }
                                else
                                {
                                    item.BackColor = listView.BackColor;
                                    if (listView.Name == "PlaylistListView")
                                    {
                                        item.BackColor = listView.BackColor;
                                    }
                                }
                            }
                        }
                        i++;
                    }
                }
                if (c.GetType() == typeof(TreeView))
                {
                    TreeView treeView = (TreeView)c;
                    if (Properties.Settings.Default.Light)
                    {
                        treeView.BackColor = AdjustColor(Properties.Settings.Default.BackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                        treeView.ForeColor = AdjustColor(Properties.Settings.Default.ForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                    }
                    else
                    {
                        treeView.BackColor = AdjustColor(Properties.Settings.Default.DarkBackColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation).Darken(.7);
                        treeView.ForeColor = AdjustColor(Properties.Settings.Default.DarkForeColor, Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);

                    }
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
            this.viewStack1.AddView(new ColorChooserView(this));
            this.viewStack1.AddView(new LibraryView(this));
            this.viewStack1.Navigate("urn:library");
            Colorize(this);
        }
        public Playlist CurrentPlaylist { get; set; }

        private void button6_Click(object sender, EventArgs e)
        {
            this.viewStack1.Navigate("urn:library");
        }
        public Track CurrentTrack { get; set; }
       
        public IMusicService MusicService { get; set; }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewStack1.Navigate("urn:color:chooser");
        }
    }
    public static class Utils
    {
        public static Color Darken(this Color color, double darkenAmount)
        {
            HSLColor hslColor = new HSLColor(color);
            hslColor.Luminosity *= darkenAmount; // 0 to 1
            return hslColor;
        }
        public static void AddObject(this ListView listView, Track obj)
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
        public static void ReloadListView(this ListView listView, IEnumerable<Track> objs) 
        {
            listView.Items.Clear();
            foreach (Track obj in objs)
            {
                listView.AddObject(obj);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }
    }
}
