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
        public Form1()
        {
            InitializeComponent();
            InitializeTabcontrol();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            this.ClientSize = new Size(500, 500);
        }

        public void InitializeTabcontrol()
        {
            TabControl tabControl1 = new TabControl
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, ClientSize.Height),
                Appearance = TabAppearance.FlatButtons,
                Dock = DockStyle.Fill,
                Padding = new Point(0, 0),

            };

            TabPage tabPage1 = new TabPage()
            {
                Text = "Fejben 21",
                BackColor = Color.Red,
            };
            TabPage tabPage2 = new TabPage()
            {
                Text = "Tic-Tac-Toe",
                BackColor = Color.Green,

            };
            TabPage tabPage3 = new TabPage()
            {
                Text = "Nim játék",
                BackColor = Color.Blue,

            };

            tabControl1.TabPages.Add(tabPage1); //fejben21
            tabControl1.TabPages.Add(tabPage2); //tic-tac-toe
            tabControl1.TabPages.Add(tabPage3); //nim
            Controls.Add(tabControl1);

            Fejben21(tabPage1);
        }

        public void Fejben21(TabPage tabPage1)
        {
            tabPage1.BackColor = Color.FromArgb(128, 220, 204, 182);
            tabPage1.ForeColor = Color.Black;


            // Setup initial label and input controls
            Label KezdoFelirat = new Label
            {
                Location = new Point(20, 20),
                Text = "Állítsd be a max lépésmaximumot!",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(300, 25),
                Parent = tabPage1,
                BackColor = Color.Transparent
            };

            NumericUpDown MaxSzam = new NumericUpDown()
            {
                Location = new Point(320, 20),
                Size = new Size(40, 25),
                Minimum = 2,
                Maximum = 7,
                Parent = tabPage1
            };

            Button InditoGomb = new Button()
            {
                Location = new Point(380, 20),
                Size = new Size(75, 25),
                Text = "Indítás",
                BackColor = Color.FromArgb(167, 148, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Parent = tabPage1,
            };

            InditoGomb.FlatAppearance.BorderSize = 0;
            //InditoGomb.Click += InditoGomb_Click;

            // Setup game controls
            TrackBar FolyamatJelzo = new TrackBar()
            {
                Location = new Point(20, 95),
                Size = new Size(300, 25),
                Minimum = 0,
                Maximum = 21,
                Visible = false,
                Enabled = false,
                Parent = tabPage1
            };

            NumericUpDown JatekosLepese = new NumericUpDown()
            {
                Location = new Point(330, 95),
                Size = new Size(40, 25),
                Minimum = 1,
                Visible = false,
                Parent = tabPage1
            };

            Button MehetGomb = new Button()
            {
                Location = new Point(400, 95),
                Size = new Size(75, 25),
                Text = "Mehet!",
                BackColor = Color.FromArgb(167, 148, 125),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Visible = false,
                Parent = tabPage1
            };

            MehetGomb.FlatAppearance.BorderSize = 0;
            //MehetGomb.Click += MehetGomb_Click;

            Button VisszaGomb = new Button()
            {
                Location = new Point(400, 20),
                Size = new Size(75, 25),
                Text = "Menü",
                BackColor = Color.FromArgb(167, 148, 125),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Visible = false,
                Parent = tabPage1
            };

            VisszaGomb.FlatAppearance.BorderSize = 0;
            //VisszaGomb.Click += VisszaGomb_Click;

            Label HolTartunk = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(560, 75),
                Text = "A játék állása:\n",
                Parent = tabPage1,
                BackColor = Color.Transparent
            };
        }
    }
}
