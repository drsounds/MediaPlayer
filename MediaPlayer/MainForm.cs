using ColorDemo;
using Bungalow;
using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Bungalow
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.MusicServices.Add(new BungalowMusicService(this.AxWindowsMediaPlayer1));
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
        
        
        public List<IMusicService> MusicServices = new List<IMusicService>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.viewStack1.AddView(new BrowseView(this));
            this.viewStack1.AddView(new ColorChooserView(this));
            this.viewStack1.AddView(new LibraryView(this));
            this.viewStack1.Navigate("urn:library");
            this.Header.MenuItems.Add(new MenuItem() { Name = "Now Playing", Uri = "urn:now:playing" });
            this.Header.MenuItems.Add(new MenuItem() { Name = "Browse", Uri = "urn:browse" });
            this.Header.MenuItems.Add(new MenuItem() { Name = "Library", Uri = "urn:library" });
            this.Header.MenuItems.Add(new MenuItem() { Name = "Store", Uri = "urn:store" });
            this.Header.MenuItems.Add(new MenuItem() { Name = "Settings", Uri = "urn:color:chooser" });
            this.Header.MenuItemChanged += Header_MenuItemChanged;
            this.Colorize();
        }

        private void Header_MenuItemChanged(object sender, Header.HeaderMenuEventArgs e)
        {
            this.viewStack1.Navigate(e.MenuItem.Uri);
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

        private void button3_Click(object sender, EventArgs e)
        {
            viewStack1.Navigate("urn:browse");
        }

        public void PlayContext(Track track, Playlist playlist)
        {

            this.CurrentTrack = track;
            Utils.CurrentTrack = track;
            if (playlist != CurrentPlaylist)
            {

                this.CurrentPlaylist = playlist;
                


                PlaylistListView.ReloadListView(playlist.Tracks);
            }

            this.Colorize();
        }

        private void PlaylistListView_DoubleClick(object sender, EventArgs e)
        {


            if (PlaylistListView.SelectedItems.Count > 0)
            {
                var item = PlaylistListView.SelectedItems[0];
                Track t = (Track)item.Tag;
                PlayContext(t, this.CurrentPlaylist);

            }
        }
    }
    
}
