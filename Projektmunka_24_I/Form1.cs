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
        private void InitializeTabcontrol()
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
                BackColor = Color.FromArgb(86, 129, 163),
            };
            TabPage tabPage2 = new TabPage()
            {
                Text = "Tic-Tac-Toe",

            };
            TabPage tabPage3 = new TabPage()
            {
                Text = "Nim játék",
            };

            tabControl1.TabPages.Add(tabPage1); //fejben21
            tabControl1.TabPages.Add(tabPage2); //tic-tac-toe
            tabControl1.TabPages.Add(tabPage3); //nim
            Controls.Add(tabControl1);

            Fejben21 f = new Fejben21();
            f.Fejben21_init(tabPage1);
            TicTacToe t = new TicTacToe();
            t.TicTacToe_init(tabPage2);
            NimGame n = new NimGame();
            n.NimGame_init(tabPage3);
        }
    }
    public class Fejben21
    {
        Label KezdoFelirat, HolTartunk, allas;
        NumericUpDown_style MaxSzam, JatekosLepese;
        Button InditoGomb, MehetGomb;
        TrackBar FolyamatJelzo;
        int Osszeg = 0;
        List<int> Hatarok = new List<int>();
        Panel panel1;
        Random R = new Random();

        public void Fejben21_init(TabPage tabPage1)
        {
            panel1 = new Panel()
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(68, 255, 242),
                Height = 100,
                Parent = tabPage1,
            };

            KezdoFelirat = new Label()
            {
                Parent = panel1,
                Location = new Point(55, 0),
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                Size = new Size(400, 100),
                Text = "Állítsd be a lépésmaximumot",
                TextAlign = ContentAlignment.TopCenter,
                Padding = new Padding(0, 30, 0, 0),
            };

            MaxSzam = new NumericUpDown_style()
            {
                Parent = tabPage1,
                Location = new Point(115, 225),
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Minimum = 2,
                Maximum = 7,
                Size = new Size(50, 50),
            };

            InditoGomb = new Button()
            {
                Parent = tabPage1,
                Location = new Point(260, 213),
                Size = new Size(130, 50),
                Text = "Indítás",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 154, 255),
                FlatStyle = FlatStyle.Flat,
            };
            InditoGomb.Click += InditoGomb_Click;

            allas = new Label()
            {
                Parent = tabPage1,
                Location = new Point(20, 30),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Text = "Játék állása:",
                Visible = false,
            };

            HolTartunk = new Label()
            {
                Parent = tabPage1,
                Location = new Point(20, 70),
                Size = new Size(450, 75),
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Text = "",
                BackColor = Color.FromArgb(127, 239, 232),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
            };
            FolyamatJelzo = new TrackBar()
            {
                Parent = tabPage1,
                Location = new Point(20, 145),
                Size = new Size(450, 25),
                Minimum = 0,
                Maximum = 21,
                Enabled = false,
                BackColor = Color.Black,
                ForeColor = Color.FromArgb(0, 30, 142),
                Visible = false,
            };

            JatekosLepese = new NumericUpDown_style()
            {
                Parent = tabPage1,
                Location = new Point(115, 265),
                Size = new Size(50, 50),
                Minimum = 1,
                Maximum = 5,
                Visible = false,
            };

            MehetGomb = new Button()
            {
                Parent = tabPage1,
                Location = new Point(260, 254),
                Text = "Mehet!",
                Font = new Font("Playfair Display", 14f),
                Size = new Size(110, 45),
                BackColor = Color.FromArgb(0, 154, 255),
                FlatStyle = FlatStyle.Flat,
                Visible = false,
            };
            MehetGomb.Click += MehetGomb_Click;

        }
        void ResetGame()
        {
            KezdoFelirat.Visible = true;
            MaxSzam.Visible = true;
            InditoGomb.Visible = true;
            panel1.Visible = true;

            Osszeg = 0;
            HolTartunk.Visible = false;
            HolTartunk.Text = string.Empty;
            FolyamatJelzo.Visible = false;
            FolyamatJelzo.Value = 0;
            JatekosLepese.Visible = false;
            MehetGomb.Visible = false;
        }

        void InditoGomb_Click(object sender, EventArgs e)
        {
            KezdoFelirat.Visible = false;
            MaxSzam.Visible = false;
            InditoGomb.Visible = false;
            panel1.Visible = false;

            allas.Visible = true;
            HolTartunk.Visible = true;
            FolyamatJelzo.Visible = true;
            JatekosLepese.Visible = true;
            JatekosLepese.Maximum = (int)MaxSzam.Value < 2 ? 2 : (int)MaxSzam.Value > 7 ? 7 : (int)MaxSzam.Value;
            MehetGomb.Visible = true;

            int NagyLepes = (int)MaxSzam.Value > 7 ? 7 :
            (int)MaxSzam.Value < 2 ? 2 : (int)MaxSzam.Value;
            for (int i = 21 % (++NagyLepes); i <= 21; i += NagyLepes)
            {
                Hatarok.Add(i);
            }
        }

        void MehetGomb_Click(object sender, EventArgs e)
        {
            JatekosKovetkezik();
            GepKovetkezik();
        }

        void JatekosKovetkezik()
        {
            if (!Kiertekeles())
            {
                int AktualisLepes = 1;
                if ((int)JatekosLepese.Value < 1)
                {
                    AktualisLepes = 1;
                }
                else if ((int)JatekosLepese.Value > (int)JatekosLepese.Maximum)
                {
                    AktualisLepes = (int)JatekosLepese.Maximum;
                }
                else
                {
                    AktualisLepes = (int)JatekosLepese.Value;
                }

                if (AktualisLepes + Osszeg > 21)
                {
                    MessageBox.Show("Besokalltál! Túllépted a 21-et! (" + (AktualisLepes + Osszeg + ")"));
                    //Application.Restart();
                    ResetGame();
                }
                else
                {
                    Osszeg += AktualisLepes;
                    HolTartunk.Text += " " + Osszeg;
                    FolyamatJelzo.Value = Osszeg;
                }
            }
        }

        void GepKovetkezik()
        {
            if (!Kiertekeles())
            {
                int i = 0;
                for (i = 0; i < Hatarok.Count; i++)
                {
                    if (Osszeg < Hatarok[i]) break;
                }
                if (Hatarok[i] - Osszeg >= 1 && Hatarok[i] - Osszeg <= (int)MaxSzam.Value)
                {
                    Osszeg = Hatarok[i];
                }
                else Osszeg += R.Next(1, (int)MaxSzam.Value + 1);
                HolTartunk.Text += " " + Osszeg;
                FolyamatJelzo.Value = Osszeg;
                if (Osszeg == 21)
                {
                    MessageBox.Show("Vesztettél!");
                    //Application.Restart();
                    ResetGame();
                }
            }
            else
            {
                MessageBox.Show("Győztél!");
                //Application.Restart();
                ResetGame();
            }
        }
        bool Kiertekeles()
        {
            if (Osszeg < 21) return false;
            return true;
        }
    }
    public class TicTacToe
    {
        Button[,] buttons = new Button[3, 3];
        char[,] board = new char[3, 3];
        char currentPlayer = 'X';
        bool gameEnded = false;
        Label labelStatus = new Label();

        public void TicTacToe_init(TabPage tabPage2)
        {
            int buttonSize = (tabPage2.Width) / 3;
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
                }
            }
            Label labelStatus = new Label
            {
                Parent = tabPage2,
                Location = new Point(0, 250),
                Size = new Size(250, 50),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "labelStatus",
                Text = "Játék kezdődött! Játékos kezd.",
            };
            this.labelStatus = labelStatus;

            Button resetButton = new Button
            {
                Parent = tabPage2,
                Location = new Point(260, 370),
                Size = new Size(75, 50),
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Reset",
            };
            resetButton.Click += ResetGame;
        }

        void ResetGame(object sender, EventArgs e)
        {
            currentPlayer = 'X';
            gameEnded = false;
            labelStatus.Text = "Új játék! Játékos kezd";

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
                labelStatus.Text = "";

                if (CheckWin(currentPlayer))
                {
                    labelStatus.Text = $"{currentPlayer} nyert!";
                    gameEnded = true;
                    return;
                }
                else if (IsBoardFull())
                {
                    labelStatus.Text = "Döntetlen!";
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
                            labelStatus.Text = "O nyert!";
                            gameEnded = true;
                        }
                        else if (IsBoardFull())
                        {
                            labelStatus.Text = "Döntetlen!";
                            gameEnded = true;
                        }
                        currentPlayer = 'X';
                        return;
                    }
                }
            }
        }
    }
    public class NimGame
    {
        Label[] pileLabels;
        Label statusLabel;
        Button[] pileButtons;
        int[] piles;
        bool isPlayerTurn = true;
        Random R = new Random();

        public void NimGame_init(TabPage tabPage3)
        {   
            piles = new int[] { R.Next(3, 10), R.Next(3, 10), R.Next(3, 10)};
            pileLabels = new Label[3];
            pileButtons = new Button[3];

            for (int i = 0; i < piles.Length; i++)
            {
                pileLabels[i] = new Label
                {
                    Parent = tabPage3,
                    Text = $"{i + 1}. kupac: {piles[i]}",
                    Location = new Point(50, 50 + i * 40),
                    Size = new Size(200, 30)
                };

                pileButtons[i] = new Button
                {
                    Parent = tabPage3,
                    Text = $"Lépés {i + 1}",
                    Location = new Point(250, 50 + i * 40),
                    Size = new Size(100, 30),
                    Tag = i
                };
                pileButtons[i].Click += RemoveStones;
            }

            statusLabel = new Label
            {
                Parent= tabPage3,
                Text = "Te következel!",
                Location = new Point(50, 200),
                Size = new Size(300, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
        }

        private void RemoveStones(object sender, EventArgs e)
        {
            if (!isPlayerTurn || IsGameOver())
                return;

            Button button = (Button)sender;
            int pileIndex = (int)button.Tag;

            if (piles[pileIndex] > 0)
            {
                piles[pileIndex]--;
                isPlayerTurn = false;
                UpdateLabelText();

                if (IsGameOver())
                {
                    statusLabel.Text = "Nyertél!";
                    DisableButtons();
                    return;
                }

                ComputerMove();
                UpdateLabelText();

                if (IsGameOver())
                {
                    statusLabel.Text = "Vesztettél!";
                    DisableButtons();
                }
                else
                {
                    statusLabel.Text = "Te következel!";
                }
            }
            else statusLabel.Text = "Érvénytelen lépés!";
        }

        private void ComputerMove()
        {
            for (int i = 0; i < piles.Length; i++)
            {
                if (piles[i] > 0)
                {
                    piles[i]--;
                    isPlayerTurn = true;
                    break;
                }
            }
        }

        private void UpdateLabelText()
        {
            for (int i = 0; i < pileLabels.Length; i++)
            {
                pileLabels[i].Text = $"{i + 1}. kupac: {piles[i]}";
            }
        }

        private void DisableButtons()
        {
            foreach (var button in pileButtons)
            {
                button.Enabled = false;
            }
        }

        private bool IsGameOver()
        {
            foreach (var pile in piles)
            {
                if (pile > 0) return false;
            }
            return true;
        }
    }
}