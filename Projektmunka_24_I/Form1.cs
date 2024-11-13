﻿using System;
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
            Text = "Projektmunka";
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
            };
            TabPage tabPage2 = new TabPage()
            {
                Text = "Tic-Tac-Toe",

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
            TicTacToe(tabPage2);
        }

        static Random R = new Random();

        public void Fejben21(TabPage tabPage1)
        {
            Label KezdoFelirat, HolTartunk;
            NumericUpDown MaxSzam, JatekosLepese;
            Button InditoGomb, MehetGomb;
            TrackBar FolyamatJelzo;
            int Osszeg = 0;
            List<int> Hatarok = new List<int>();
            Panel panel1;

            tabPage1.BackColor = Color.FromArgb(86, 129, 163);
            panel1 = new Panel() {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(68, 255, 242),
                Height = 100,
                Parent = tabPage1,
            };

            KezdoFelirat = new Label() {
                Parent = panel1,
                Location = new Point(55, 0),
                Font = new Font("Playfair Display", 16f),
                Size = new Size(400, 100),
                Text = "Állítsd be a lépésmaximumot",
                TextAlign = ContentAlignment.TopCenter,
                Padding = new Padding(0, 30, 0, 0),
            };

            MaxSzam = new NumericUpDown() {
                Parent = tabPage1,
                Location = new Point(115, 225),
                Minimum = 2,
                Maximum = 7,
                Size = new Size(70, 100),
                BackColor = Color.FromArgb(2, 143, 232)

            };

            InditoGomb = new Button() {
                Parent = tabPage1,
                Location = new Point(260, 213),
                Size = new Size(130, 50),
                Text = "Indítás",
                Font = new Font("Playfair Display", 14f),
                BackColor = Color.FromArgb(0, 30, 142),
                ForeColor = Color.FromArgb(242, 242, 242),
            };
            InditoGomb.Click += InditoGomb_Click;


            #region Fejben21 fuggvenyek
            void InditoGomb_Click(object sender, EventArgs e) {

                KezdoFelirat.Visible = false;
                MaxSzam.Visible = false;
                InditoGomb.Visible = false;
                panel1.Visible = false;


                HolTartunk = new Label() {
                    Parent = tabPage1,
                    Location = new Point(20, 70),
                    Size = new Size(450, 75),
                    Text = "Játék állása:\n",
                    BackColor = Color.FromArgb(127, 239, 232),
                };
                FolyamatJelzo = new TrackBar() {
                    Parent = tabPage1,
                    Location = new Point(20, 145),
                    Size = new Size(450, 25),
                    Minimum = 0,
                    Maximum = 21,
                    Enabled = false,
                    BackColor = Color.Black,
                    ForeColor = Color.FromArgb(0, 30, 142),

                };

                JatekosLepese = new NumericUpDown() {
                    Parent = tabPage1,
                    Location = new Point(115, 265),
                    Size = new Size(50, 50),
                    BackColor = Color.FromArgb(2, 143, 232),
                    Minimum = 1,
                    Maximum = (int)MaxSzam.Value < 2 ? 2 : (int)MaxSzam.Value > 7 ? 7 : (int)MaxSzam.Value,
                };

                MehetGomb = new Button() {
                    Parent = tabPage1,
                    Location = new Point(260, 254),
                    Text = "Mehet!",
                    Font = new Font("Playfair Display", 14f),

                    Size = new Size(110, 45),
                    BackColor = Color.FromArgb(0, 30, 142),
                    ForeColor = Color.FromArgb(242, 242, 242),
                };

                MehetGomb.Click += MehetGomb_Click;

                int NagyLepes = (int)MaxSzam.Value > 7 ? 7 :
                (int)MaxSzam.Value < 2 ? 2 : (int)MaxSzam.Value;
                for(int i = 21 % (++NagyLepes); i <= 21; i += NagyLepes) {
                    Hatarok.Add(i);
                }
            }

            void MehetGomb_Click(object sender, EventArgs e) {
                JatekosKovetkezik();
                GepKovetkezik();
            }

            void JatekosKovetkezik() {
                if(!Kiertekeles()) {
                    /*int AktualisLepes = (int)JatekosLepese.Value < 1 ? 1 :
                        (int)JatekosLepese.Value > (int)JatekosLepese.Maximum ?
                        (int)JatekosLepese.Maximum : (int)JatekosLepese.Value;*/
                    int AktualisLepes = 1;
                    if((int)JatekosLepese.Value < 1) AktualisLepes = 1;
                    else if((int)JatekosLepese.Value > (int)JatekosLepese.Maximum)
                        AktualisLepes = (int)JatekosLepese.Maximum;
                    else AktualisLepes = (int)JatekosLepese.Value;
                    //besokallt-e?
                    if(AktualisLepes + Osszeg > 21) {
                        MessageBox.Show("Besokalltál! Túllépted a 21-et! (" + (AktualisLepes + Osszeg + ")"));
                        Application.Restart();
                    } else {
                        Osszeg += AktualisLepes;
                        HolTartunk.Text += " " + Osszeg;
                        FolyamatJelzo.Value = Osszeg;
                    }
                }
                /*else
                {
                    HolTartunk.Text += " " + Osszeg;
                    MessageBox.Show("Vesztettél!");
                    Application.Restart();
                }*/
            }

            void GepKovetkezik() {
                if(!Kiertekeles()) {
                    int i = 0;
                    for(i = 0; i < Hatarok.Count; i++) {
                        if(Osszeg < Hatarok[i]) break;
                    }
                    if(Hatarok[i] - Osszeg >= 1 && Hatarok[i] - Osszeg <= (int)MaxSzam.Value) {
                        Osszeg = Hatarok[i];
                    } else Osszeg += R.Next(1, (int)MaxSzam.Value + 1);
                    HolTartunk.Text += " " + Osszeg;
                    FolyamatJelzo.Value = Osszeg;
                    if(Osszeg == 21) {
                        MessageBox.Show("Megvertelek!");
                        Application.Restart();
                    }
                } else {
                    MessageBox.Show("Nyertél, gratulálunk!");
                    Application.Restart();
                }
            }

            bool Kiertekeles() {
                if(Osszeg < 21) return false;
                return true;
            }
            #endregion

        }



        private Button[,] buttons = new Button[3, 3];
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';
        private bool gameEnded = false;
        public void TicTacToe(TabPage tabPage2)
        {
            int buttonSize = (this.Width - 100) / 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Parent = tabPage2,
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(j * buttonSize + 10, i * buttonSize + 10),
                        Font = new Font("Arial", 16, FontStyle.Bold),
                        Tag = new Point(i, j), // A gomb pozíciója a mátrixban
                        BackColor = Color.LightBlue,
                    };

                    buttons[i, j].Click += ButtonClick;
                    //Controls.Add(buttons[i, j]);
                }
            }

            // Hozzáadunk egy státusz Labelt az ablak aljára
            Label labelStatus = new Label
            {
                Parent = tabPage2,
                Location = new Point(0, 400),
                Size = new Size(250, 50),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "labelStatus",
                Text = "Játék kezdődött! 'X' kezd.",
            };
            Controls.Add(labelStatus);

            Button resetButton = new Button
            {
                Parent = tabPage2,
                Location = new Point(260, 450),
                Size = new Size(75, 50),
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Reset",
            };
            resetButton.Click += ResetGame;

            void ResetGame(object sender, EventArgs e)
            {
                currentPlayer = 'X';
                gameEnded = false;
                Controls["labelStatus"].Text = "reset";

                // Inicializáljuk a táblát és a gombok szövegét
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = '\0';
                        buttons[i, j].Text = "";
                        buttons[i, j].Enabled = true;
                    }
                }
            }

            void ButtonClick(object sender, EventArgs e)
            {
                if (gameEnded) return;

                Button button = (Button)sender;
                Point pos = (Point)button.Tag;
                int row = pos.X;
                int col = pos.Y;

                if (board[row, col] == '\0')
                {
                    board[row, col] = currentPlayer;
                    button.Text = currentPlayer.ToString();

                    if (CheckWin(currentPlayer))
                    {
                        Controls["labelStatus"].Text = $"'{currentPlayer}' nyert!";
                        gameEnded = true;
                        return;
                    }
                    else if (IsBoardFull())
                    {
                        Controls["labelStatus"].Text = "Döntetlen!";
                        gameEnded = true;
                        return;
                    }

                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';

                    if (currentPlayer == 'O')
                    {
                        ComputerMove();
                    }
                }
            }

            bool CheckWin(char player)
            {
                for (int i = 0; i < 3; i++)
                {
                    if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                        (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                        return true;
                }

                return (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                       (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
            }

            bool IsBoardFull()
            {
                foreach (char c in board)
                    if (c == '\0') return false;
                return true;
            }

            void ComputerMove()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == '\0')
                        {
                            board[i, j] = 'O';
                            buttons[i, j].Text = "O";
                            buttons[i, j].Enabled = false;

                            if (CheckWin('O'))
                            {
                                Controls["labelStatus"].Text = "'O' nyert!";
                                gameEnded = true;
                            }
                            else if (IsBoardFull())
                            {
                                Controls["labelStatus"].Text = "Döntetlen!";
                                gameEnded = true;
                            }
                            currentPlayer = 'X';
                            return;
                        }
                    }
                }
            }
        }

        public void Nim(TabPage tabPage3)
        {

        }
    }
}
