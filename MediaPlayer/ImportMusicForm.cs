using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Id3.Info;
using Id3;
using MediaPlayer.Models;

namespace MediaPlayer
{
    public partial class ImportMusicForm : Form
    {
        public ImportMusicForm()
        {
            InitializeComponent();
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;
        }
        public delegate void ReportProgressEventHandler(object sender, Track track);
        public event ReportProgressEventHandler Progress;
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
                button3.Enabled = true;
            } 
        }

        BackgroundWorker bw = new BackgroundWorker();
        private void button3_Click(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress?.Invoke(this, (Track)e.UserState);
            try
            {
                label2.Text = ((Track)e.UserState).Artist + ((Track)e.UserState).Name;
            }
            catch (Exception ex)
            {

            }
        }

        private MediaPlayerDatabaseContext DbContext;
        public void ScanDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            
            foreach(FileInfo fi in di.GetFiles())
            {
                try
                {
                    if (!fi.FullName.EndsWith(".mp3")) continue;
                    TagLib.Mpeg.AudioFile audioFile = new TagLib.Mpeg.AudioFile(fi.FullName);

                    Track track = new Track()
                    {
                        Artist = "",
                        Name = audioFile.Tag.Title,
                        Album = audioFile.Tag.Album,
                        Url = "file://" + fi.FullName
                    };
                    if (audioFile.Tag.Artists.Length > 0)
                    {
                        track.Artist = audioFile.Tag.Artists[0];
                    }
                    DbContext.Tracks.Add(track);

                    bw.ReportProgress(2, track);
                }
                catch (Exception e)
               {

                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                ScanDirectory(dir.FullName);
            }

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            using (DbContext = new MediaPlayerDatabaseContext())
            {
                ScanDirectory(textBox1.Text);
                DbContext.SaveChanges();
            }
            bw.ReportProgress(100);
        }

        private void ImportMusicForm_Load(object sender, EventArgs e)
        {

        }
    }
}
