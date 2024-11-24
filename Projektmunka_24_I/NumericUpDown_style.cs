using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projektmunka_24_I
{
    public class NumericUpDown_style : NumericUpDown
    {
        public NumericUpDown_style()
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.FromArgb(0, 154, 255);
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }
        

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (this.Controls.Count > 0)
            {
                var numeric = Controls[0];
                numeric.Paint += Buttons_Paint;
                numeric.Invalidate();
            }
        }

        private void Buttons_Paint(object sender, PaintEventArgs e)
        {
            var control = (Control)sender;
            var graphics = e.Graphics;

            // Teljes terület törlése
            graphics.Clear(this.BackColor);

            // A gombok teljes területe
            var totalRect = control.ClientRectangle;

            // Fel gomb területe (felső fele)
            var upButtonRect = new Rectangle(0, 0, totalRect.Width, totalRect.Height / 2);

            // Le gomb területe (alsó fele)
            var downButtonRect = new Rectangle(0, totalRect.Height / 2, totalRect.Width, totalRect.Height / 2);

            // Rajzolj egy modern fel nyilat
            DrawArrow(graphics, upButtonRect, true);

            // Rajzolj egy modern le nyilat
            DrawArrow(graphics, downButtonRect, false);


        }

        private void DrawArrow(Graphics g, Rectangle rect, bool isUp)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                // Nyíl pontjainak kiszámítása
                if (isUp)
                {
                    path.AddPolygon(new Point[]
                    {
                new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 4), // Felső csúcs
                new Point(rect.Left + rect.Width / 4, rect.Bottom - rect.Height / 4), // Bal alsó
                new Point(rect.Right - rect.Width / 4, rect.Bottom - rect.Height / 4) // Jobb alsó
                    });
                }
                else
                {
                    path.AddPolygon(new Point[]
                    {
                new Point(rect.Left + rect.Width / 2, rect.Bottom - rect.Height / 4), // Alsó csúcs
                new Point(rect.Left + rect.Width / 4, rect.Top + rect.Height / 4), // Bal felső
                new Point(rect.Right - rect.Width / 4, rect.Top + rect.Height / 4) // Jobb felső
                    });
                }

                // Rajzoljuk a nyilat
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillPath(Brushes.Black, path);
            }
        }
        
    }
}
