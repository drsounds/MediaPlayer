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
    public class Header : Panel
    {
        public Header() :base()
        {
            MenuItems = new List<MenuItem>();
            this.MouseDown += Header_MouseDown;
            this.MouseUp += Header_MouseUp;
            this.Click += Header_Click;
            this.MouseMove += Header_MouseMove;
        }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            HoveredButton = null;
            int x = 20;
            foreach (MenuItem item in MenuItems)
            {
                HoveredButton = item;
                if (e.X > x && e.X < x + MENUITEM_WIDTH)
                {
                    SelectedButton = item;
                    Invalidate(new Region(new Rectangle(x, 0, MENUITEM_WIDTH, this.Height)));
                }
                x += MENUITEM_WIDTH + MENUITEM_PADDING;
            }
        }

        private void Header_Click(object sender, EventArgs e)
        {
            CurrentButton = PressedButton;
            MenuItemChanged?.Invoke(sender, new HeaderMenuEventArgs() { MenuItem = CurrentButton });
            Invalidate(new Region(new Rectangle(0, 0, this.Width, this.Height)));
        }

        private void Header_MouseUp(object sender, MouseEventArgs e)
        {
         
            Invalidate(new Region(new Rectangle(0, 0, this.Width, this.Height)));
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 20;

            foreach (MenuItem item in MenuItems)
            {
                HoveredButton = item;
                if (e.X > x && e.X < x + MENUITEM_WIDTH)
                {
                    PressedButton = item;
                }
                x += MENUITEM_WIDTH + MENUITEM_PADDING;
            }
            Draw(this.CreateGraphics());
        }

        public const int MENUITEM_PADDING = 10;
        public const int MENUITEM_WIDTH = 100;
        public const int MENUITEM_HEIGHT = 38;
        public List<MenuItem> MenuItems;
        public MenuItem CurrentButton { get; set; }
        public class HeaderMenuEventArgs
        {
            public MenuItem MenuItem { get; set; }
        }
        public delegate void MenuButtonChanged(object sender, HeaderMenuEventArgs e);
        public event MenuButtonChanged MenuItemChanged;
        private MenuItem SelectedButton;
        private MenuItem PressedButton;
        private MenuItem HoveredButton;

        public void Draw(Graphics g)
        {

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(this.BackgroundImage, new Rectangle(0, 0, this.Width * 2, this.Height));
            int x = 20;

            foreach (MenuItem item in MenuItems)
            {
                Color foreColor = Properties.Settings.Default.ForeColor.Adjust(Properties.Settings.Default.Hue, Properties.Settings.Default.Saturation);
                Color backColor = Color.Transparent;
                Rectangle btnBounds = new Rectangle(x, this.Height - MENUITEM_HEIGHT, MENUITEM_WIDTH, MENUITEM_HEIGHT);
                if (CurrentButton == item || item == PressedButton)
                {
                    backColor = foreColor;
                    g.FillRectangle(new SolidBrush(backColor), btnBounds);
                    foreColor = Color.White;
                }
                else
                {
                    g.DrawRectangle(new Pen(new SolidBrush(foreColor)), btnBounds);

                }
                g.DrawString(item.Name, this.Font, new SolidBrush(foreColor), new Point((int)((btnBounds.X) + (btnBounds.Width / 2 ) - ((g.MeasureString(item.Name, this.Font).Width) / 2)), (int)((btnBounds.Y + btnBounds.Height) / 2 - (g.MeasureString(item.Name, this.Font).Height) / 2)));
                x += MENUITEM_WIDTH + MENUITEM_PADDING * 2;

            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
    }


}
