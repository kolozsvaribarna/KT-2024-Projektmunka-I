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

        public void Fejben21(TabPage tabPage1)
        {
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
