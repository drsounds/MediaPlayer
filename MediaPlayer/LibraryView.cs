﻿using System;
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
                    var artists = dbContext.Database.SqlQuery<string>("SELECT DISTINCT Artist from Tracks ORDER BY Artist ASC");
                    
                    artistsNode.Nodes.Clear();
                    foreach (string t in artists)
                    {
                        TreeNode n = artistsNode.Nodes.Add(t);
                        n.Tag = "SELECT * FROM Tracks WHERE artist = '" + t.Replace("'", "") + "'";
                        var albums = dbContext.Database.SqlQuery<string>("SELECT DISTINCT Album FROM tracks WHERE artist = '" + t.Replace("'", "") + "'");
                    //    TreeNode nAlbums = n.Nodes.Add("Albums");
                        foreach(string album in albums)
                        {
                            if (album == null)
                                continue;
                            TreeNode nAlbum = n.Nodes.Add(album);

                            nAlbum.Tag = "SELECT * FROM Tracks WHERE Artist = '" + t + "' AND Album = '" + album.Replace("'", "''") + "'";
                     
                        }
                    }
                    MainForm.Colorize(listView1);
                }
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
                using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
                {
                   
                    var tracks = dbContext.Tracks.SqlQuery(query);
                    listView1.ReloadListView(tracks);
                    MainForm.Colorize(listView1);
                    
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
                    listView1.SelectedItems.Clear();
                    Playlist playlist = new Models.Playlist();
                    MainForm.CurrentPlaylist = playlist;
                    foreach (ListViewItem i in this.listView1.Items)
                    {
                        playlist.Tracks.Add((Track)i.Tag);
                    }
                    PlaylistListView.ReloadListView(playlist.Tracks);
                    MainForm.Colorize(this);
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

                    MainForm.CurrentPlaylist = playlist;
                    PlaylistListView.ReloadListView(playlist.Tracks);
                    MainForm.Colorize(this);
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

        private void PlaylistListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ImportMusicForm imf = new ImportMusicForm();
            imf.Progress += Imf_Progress;
            imf.Show();

        }

        private void PlaylistListView_DoubleClick(object sender, EventArgs e)
        {

            if (PlaylistListView.SelectedItems.Count > 0)
            {
                var item = PlaylistListView.SelectedItems[0];
                Track t = (Track)item.Tag;
                Play(t, MainForm.CurrentPlaylist);
                PlaylistListView.EnsureVisible(0);

            }
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
            var query = "SELECT * FROM Tracks WHERE Name LIKE '%" + q + "%' OR Artist LIKE '%" + q + "%' OR Album LIKE '%" + q + "%'";
            LoadMusic(query);
            var node = treeView1.Nodes[1].Nodes.Add(q);
            node.Tag = query;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }
    }
}
