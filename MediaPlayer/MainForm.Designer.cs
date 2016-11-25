namespace Bungalow
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Footer = new System.Windows.Forms.Panel();
            this.AxWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.Header = new Bungalow.Header();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.viewStack1 = new Bungalow.ViewStack();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PlaylistListView = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Artist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AxWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackgroundImage = global::Bungalow.Properties.Resources.header3;
            this.Footer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Footer.Controls.Add(this.AxWindowsMediaPlayer1);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Location = new System.Drawing.Point(0, 546);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(1150, 60);
            this.Footer.TabIndex = 3;
            // 
            // AxWindowsMediaPlayer1
            // 
            this.AxWindowsMediaPlayer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AxWindowsMediaPlayer1.Enabled = true;
            this.AxWindowsMediaPlayer1.Location = new System.Drawing.Point(392, 6);
            this.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1";
            this.AxWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxWindowsMediaPlayer1.OcxState")));
            this.AxWindowsMediaPlayer1.Size = new System.Drawing.Size(271, 51);
            this.AxWindowsMediaPlayer1.TabIndex = 1;
            // 
            // Header
            // 
            this.Header.BackgroundImage = global::Bungalow.Properties.Resources.header3;
            this.Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Header.CurrentButton = null;
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1150, 55);
            this.Header.TabIndex = 0;
            this.Header.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.viewStack1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PlaylistListView);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1150, 491);
            this.splitContainer1.SplitterDistance = 870;
            this.splitContainer1.TabIndex = 4;
            // 
            // viewStack1
            // 
            this.viewStack1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewStack1.Location = new System.Drawing.Point(0, 0);
            this.viewStack1.Name = "viewStack1";
            this.viewStack1.Size = new System.Drawing.Size(870, 491);
            this.viewStack1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 22);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 466);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 25);
            this.panel2.TabIndex = 1;
            // 
            // PlaylistListView
            // 
            this.PlaylistListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.Artist});
            this.PlaylistListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistListView.Location = new System.Drawing.Point(0, 22);
            this.PlaylistListView.Name = "PlaylistListView";
            this.PlaylistListView.Size = new System.Drawing.Size(276, 444);
            this.PlaylistListView.TabIndex = 2;
            this.PlaylistListView.UseCompatibleStateImageBehavior = false;
            this.PlaylistListView.View = System.Windows.Forms.View.Details;
            this.PlaylistListView.DoubleClick += new System.EventHandler(this.PlaylistListView_DoubleClick);
            // 
            // NameColumn
            // 
            this.NameColumn.Tag = "Name";
            this.NameColumn.Text = "Name";
            // 
            // Artist
            // 
            this.Artist.Tag = "Artist";
            this.Artist.Text = "Artist";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 606);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AxWindowsMediaPlayer1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private AxWMPLib.AxWindowsMediaPlayer AxWindowsMediaPlayer1;
        private System.Windows.Forms.Panel Footer;
        private Header Header;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ViewStack viewStack1;
        private System.Windows.Forms.ListView PlaylistListView;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader Artist;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}

