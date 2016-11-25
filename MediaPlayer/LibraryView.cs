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
using Bungalow.Models;

namespace Bungalow
{
    public partial class LibraryView : View
    {
        public IMusicLibraryService MusicLibrary;
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
            MusicLibrary = new LocalLibraryProvider();
        }
        public LibraryView(MainForm mainForm) : base(mainForm)
        {
            InitializeComponent();
            MusicLibrary = new LocalLibraryProvider();
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

                var tracks = MusicLibrary.GetAllTracks();
                    listView1.ReloadListView(tracks);
                var artists = MusicLibrary.GetAllArtists();
                foreach (Artist artist in artists)
                {
                    TreeNode tn = treeView1.Nodes[0].Nodes[0].Nodes.Add(artist.Name);
                    tn.Tag = "urn:artist:" + artist.Name + ":track";
                    var albums = MusicLibrary.GetAlbumsByArtist(artist.Name);
                    foreach(Album album in albums)
                    {
                        TreeNode trAlbum = tn.Nodes.Add(album.Name);
                        trAlbum.Tag = "urn:artist:" + artist.Name + ":album:" + album.Name + ":track";
                        tn.Nodes.Add(trAlbum);
                    }
                }
                    
                    listView1.Colorize();
                
            }
            catch (Exception e)
            {

            }
        }
        public void LoadMusic(string query)
        {
            TreeNode artistsNode = treeView1.Nodes[0].Nodes[0];
            try
            {
            

                var tracks = MusicLibrary.GetTracksByUri(query);
                listView1.ReloadListView(tracks);
                listView1.Colorize();
                    
                
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
            foreach (IStreamingMusicService m in MainForm.MusicServices)
            {
                if (m.Play(track.Name, track.Artist, track.Album))
                {
                    listView1.SelectedItems.Clear();
                    Playlist playlist = new Models.Playlist();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        playlist.Tracks.Add((Track)item.Tag);
                    }
                    MainForm.PlayContext(track, playlist);

                    
                    MainForm.Colorize();
                    break;
                }
            }
        }
        public void Play(Track track, Playlist playlist)
        {
            foreach (IStreamingMusicService m in MainForm.MusicServices)
            {
                if (track == null)
                    break;
                if (m.Play(track.Name, track.Artist, track.Album))
                {
                    MainForm.PlayContext(track, playlist);


                    MainForm.Colorize();
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
            this.Colorize();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlaylistListView_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void PlaylistListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ImportMusicForm imf = new ImportMusicForm();
            imf.Progress += Imf_Progress;
            imf.Show();

        }
        public class PlaylistEventArgs
        {
            public Playlist Playlist { get; set; }
            public Track Track { get; set; }
        }
        public delegate void PlaylistEventHandler(object sender, PlaylistEventArgs e);
        public event PlaylistEventHandler PlaylistChanged;
        private void PlaylistListView_DoubleClick(object sender, EventArgs e)
        {
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            var item = treeView1.SelectedNode;
            try
            {
                string sql = (string)item.Tag;
                LoadMusic(sql);
            }
            catch (Exception ex)
            {

            }
        }
        public void Search(string q)
        {
            var query = "urn:search:" + q; //"SELECT * FROM Tracks WHERE Name LIKE '%" + q + "%' OR Artist LIKE '%" + q + "%' OR Album LIKE '%" + q + "%'";
            LoadMusic(query);
            var node = treeView1.Nodes[1].Nodes.Add(q);
            node.Tag = query;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
