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
using MediaPlayer.Models;

namespace MediaPlayer
{
    public partial class LibraryView : View
    {

        public override bool AcceptsUri(string uri)
        {
            return new Regex("urn:library").IsMatch(uri);
        }
        public override void Navigate(string uri)
        {

        }
        public LibraryView()
        {
            InitializeComponent();
        }
        public LibraryView(MainForm mainForm) : base(mainForm)
        {
            InitializeComponent();
            LoadMusic();
        }

        private void LibraryView_Load(object sender, EventArgs e)
        {

        }

        public void LoadMusic()
        {
            TreeNode artistsNode = treeView1.Nodes[0].Nodes[0];
            try
            {
                using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
                {
                    var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist);
                    listView1.ReloadListView(tracks);
                    MainForm.Colorize(listView1);
                    var artists = dbContext.Tracks.Distinct(new TrackArtistComparer());
                    artistsNode.Nodes.Clear();
                    foreach (Track t in artists)
                    {
                        artistsNode.Nodes.Add(t.Artist);

                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Play(Track track)
        {
            if (track == null)
                return;
            foreach (IMusicService m in MainForm.MusicServices)
            {
                if (m.Play(track.Name, track.Artist, track.Album))
                {
                    MainForm.CurrentTrack = track;
                    MainForm.Colorize(this);
                    listView1.SelectedItems.Clear();
                    Playlist playlist = new Models.Playlist();
                    foreach (ListViewItem i in this.listView1.Items)
                    {
                        playlist.Tracks.Add((Track)i.Tag);
                    }
                    PlaylistListView.ReloadListView(playlist.Tracks);
                    break;
                }
            }
        }
        public void Play(Track track, Playlist playlist)
        {
            foreach (IMusicService m in MainForm.MusicServices)
            {
                if (track == null)
                    break;
                if (m.Play(track.Name, track.Artist, track.Album))
                {
                    MainForm.CurrentTrack = track;
                    MainForm.Colorize(this);

                    PlaylistListView.ReloadListView(playlist.Tracks);
                    break;
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];
                Track t = (Track)item.Tag;
                Play(t);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ImportMusicForm imf = new ImportMusicForm();
            imf.Progress += Imf_Progress;
            imf.Show();
        }
        private void Imf_Progress(object sender, Track track)
        {
            this.listView1.AddObject(track);
            this.MainForm.Colorize(this);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlaylistListView_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
