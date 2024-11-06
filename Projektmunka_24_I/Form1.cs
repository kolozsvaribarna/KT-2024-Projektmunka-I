using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projektmunka_24_I
{
    public partial class Form1 : Form
    {
        private TabPage tabPage1,tabPage2, tabPage3;
        private TabControl tabControl1;

        Label label1, label2, label3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tabControl1 = new TabControl()
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, ClientSize.Height),
                Appearance = TabAppearance.FlatButtons,
                Dock = DockStyle.Fill,
                Padding = new Point(0, 0),

            };

            this.tabPage1 = new TabPage()
            {
                Text = "Fejben 21",
                BackColor = Color.Red,

            };
            this.tabPage2 = new TabPage()
            {
                Text = "Tic-Tac-Toe",
                BackColor = Color.Green,

            };
            this.tabPage3 = new TabPage()
            {
                Text = "Nim játék",
                BackColor = Color.Blue,

            };

            label1 = new Label()
            {
                Parent = tabPage1,
                Text = "label1",
            };
            label2 = new Label()
            {
                Parent = tabPage2,
                Text = "label2",
            };

            this.tabControl1.TabPages.Add(tabPage1);
            this.tabControl1.TabPages.Add(tabPage2);
            this.tabControl1.TabPages.Add(tabPage3);



            this.Controls.Add(tabControl1);

            this.ClientSize = new Size(500, 500);
        }
    }
}
