using Bungalow.Models;
using ColorDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bungalow
{
    public static class Utils
    {
        public static Track CurrentTrack { get; set; }
        // http://stackoverflow.com/questions/1079820/rotate-hue-using-imageattributes-in-c-sharp
        public static Bitmap SetHue(this Bitmap bmpElement, float value)
        {

            const float wedge = 120f / 360;

            var hueDegree = -value / 360 % 1;
            if (hueDegree < 0) hueDegree += 1;

            var matrix = new float[5][];

            if (hueDegree <= wedge)
            {
                //Red..Green
                var theta = hueDegree / wedge * (Math.PI / 2);
                var c = (float)Math.Cos(theta);
                var s = (float)Math.Sin(theta);

                matrix[0] = new float[] { c, 0, s, 0, 0 };
                matrix[1] = new float[] { s, c, 0, 0, 0 };
                matrix[2] = new float[] { 0, s, c, 0, 0 };
                matrix[3] = new float[] { 0, 0, 0, 1, 0 };
                matrix[4] = new float[] { 0, 0, 0, 0, 1 };

            }
            else if (hueDegree <= wedge * 2)
            {
                //Green..Blue
                var theta = (hueDegree - wedge) / wedge * (Math.PI / 2);
                var c = (float)Math.Cos(theta);
                var s = (float)Math.Sin(theta);

                matrix[0] = new float[] { 0, s, c, 0, 0 };
                matrix[1] = new float[] { c, 0, s, 0, 0 };
                matrix[2] = new float[] { s, c, 0, 0, 0 };
                matrix[3] = new float[] { 0, 0, 0, 1, 0 };
                matrix[4] = new float[] { 0, 0, 0, 0, 1 };

            }
            else
            {
                //Blue..Red
                var theta = (hueDegree - 2 * wedge) / wedge * (Math.PI / 2);
                var c = (float)Math.Cos(theta);
                var s = (float)Math.Sin(theta);

                matrix[0] = new float[] { s, c, 0, 0, 0 };
                matrix[1] = new float[] { 0, s, c, 0, 0 };
                matrix[2] = new float[] { c, 0, s, 0, 0 };
                matrix[3] = new float[] { 0, 0, 0, 1, 0 };
                matrix[4] = new float[] { 0, 0, 0, 0, 1 };
            }

            Bitmap originalImage = bmpElement;

            var imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(matrix), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap destImage = new Bitmap(originalImage);
            var grpElement = Graphics.FromImage(destImage);
            grpElement.DrawImage(
                bmpElement, new Rectangle(0, 0, destImage.Width, destImage.Height),
                0, 0, originalImage.Width, originalImage.Height,
                GraphicsUnit.Pixel, imageAttributes
                );
            return destImage;
        }
        public static Image ChangeHue(this Image image, double degrees)
        {
            double r = degrees * System.Math.PI / 360; // degrees to radians
            ImageAttributes imageAttributes = new ImageAttributes();

            float[][] colorMatrixElements = {
            new float[] {(float)System.Math.Cos(r),  (float)System.Math.Sin(r),  0,  0, 0},
            new float[] {(float)-System.Math.Sin(r),  (float)-System.Math.Cos(r),  0,  0, 0},
            new float[] {0,  0,  2,  0, 0},
            new float[] {0,  0,  0,  1, 0},
            new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(
             colorMatrix,
            ColorMatrixFlag.Default,
           ColorAdjustType.Bitmap);
            Image destImage = new Bitmap(image);
            var g = Graphics.FromImage(destImage);
            g.DrawImage(
               image,
               new Rectangle(0, 0, image.Width, image.Height),  // destination rectangle 
                0, 0,        // upper-left corner of source rectangle 
                image.Width,       // width of source rectangle
                image.Height,      // height of source rectangle
                GraphicsUnit.Pixel,
               imageAttributes);
            return destImage;
        }
        public static Color Darken(this Color color, double darkenAmount)
        {
            HSLColor hslColor = new HSLColor(color);
            hslColor.Luminosity *= darkenAmount; // 0 to 1
            return hslColor;
        }
        public static void AddObject<T>(this ListView listView, T obj) where T : Model
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
        public static void ReloadListView<T>(this ListView listView, IEnumerable<T> objs) where T : Model
        {
            listView.Items.Clear();
            foreach (T obj in objs)
            {
                listView.AddObject(obj);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }
        public static void Colorize(this Control c)
        {
            c.Colorize(Properties.Settings.Default.Hue);
        }
        public static void Colorize(this Control c, float hue)
        {
            var GlobalBackColor = Properties.Settings.Default.ForeColor.Adjust(hue, Properties.Settings.Default.Saturation);

            if (c.Name == "Header" || c.Name == "Footer")
            {
                c.BackgroundImage = Properties.Resources.header3.SetHue(hue - 140);
            }
            c.BackColor = GlobalBackColor;
            if (c.GetType() == typeof(ListView))
            {
                ListView listView = (ListView)c;

                if (Properties.Settings.Default.Light && listView.Name != "PlaylistListView")
                {

                    listView.BackColor = Properties.Settings.Default.BackColor.Adjust(hue, Properties.Settings.Default.Saturation);
                    listView.ForeColor = Properties.Settings.Default.ForeColor.Adjust(hue, Properties.Settings.Default.Saturation);
                }
                else
                {
                    listView.BackColor = Properties.Settings.Default.DarkBackColor.Adjust(hue, Properties.Settings.Default.Saturation).Darken(1.5);
                    listView.ForeColor = Properties.Settings.Default.DarkForeColor.Adjust(hue, Properties.Settings.Default.Saturation);

                }
                if (listView.Name == "PlaylistListView")
                {
                    listView.BackColor = Properties.Settings.Default.DarkBackColor.Adjust(hue, Properties.Settings.Default.Saturation).Darken(1.8);
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
                        if ((i % 2) != 0)
                        {
                            if (Properties.Settings.Default.AlternatingRows)
                                if (Properties.Settings.Default.Light)
                                {
                                    item.BackColor = Properties.Settings.Default.AlternateRowColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation); ;
                                    if (listView.Name == "PlaylistListView")
                                    {
                                        item.BackColor = listView.BackColor;
                                    }
                                }
                                else
                                {
                                    item.BackColor = Properties.Settings.Default.DarkBackColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation).Darken(1.4);
                                    if (listView.Name == "PlaylistListView")
                                    {
                                        item.BackColor = listView.BackColor;
                                    }
                                }
                            else
                            {
                                item.BackColor = listView.BackColor.Darken(1.2);
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
                    treeView.BackColor = Properties.Settings.Default.BackColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                    treeView.ForeColor = Properties.Settings.Default.ForeColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                }
                else
                {
                    treeView.BackColor = Properties.Settings.Default.DarkBackColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation).Darken(1.2);
                    treeView.ForeColor = Properties.Settings.Default.DarkForeColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);

                }
            }
            foreach (Control control in c.Controls)
            {

                try
                {
                    control.Colorize(hue);
                }
                catch (Exception e)
                {

                }
            }
        }
        public static Color Adjust(this Color color, float hue, float saturation)
        {

            HSLColor hslColor = new HSLColor(color);
            hslColor.Hue += hue - 140;
            hslColor.Saturation += saturation;

            return hslColor;
        }

    }
}
