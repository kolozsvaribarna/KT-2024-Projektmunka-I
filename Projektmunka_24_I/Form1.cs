using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.IO.IsolatedStorage;

namespace Projektmunka_24_I
{
    public partial class Form1 : Form
    {
        TabPage tabPage1, tabPage2, tabPage3, tabPage4, tabPage5;
        TabControl tc;
        TextBox usernameTextBox;
        Label nameLabel;

        #region fejben21_mezok
        Stopwatch fejben21T = new Stopwatch();
        Random R = new Random();
        NumericUpDown_style MaxSzam, JatekosLepese;
        TrackBar FolyamatJelzo;
        GombRounded InditoGomb, MehetGomb, VisszaGomb;
        Label KezdoFelirat, HolTartunk, allas;
        Panel panel1;
        int Osszeg = 0;
        List<int> Hatarok = new List<int>();
        #endregion

        #region Tic-tac-toe_mezok
        Button[,] buttons = new Button[3, 3];
        char[,] board = new char[3, 3];
        char currentPlayer = 'X';
        bool gameEnded = false;
        Label labelStatus = new Label();
        GombRounded resetButton = new GombRounded();
        bool isPlayerFirstMove = true;
        Stopwatch TicTacToeT = new Stopwatch();
        #endregion

        #region NimGame_mezok
        Label[] pileLabels;
        Label NimStatusLabel;
        Button[] pileButtons;
        Button NimResetButton;
        int[] piles;
        bool isPlayerTurn = true;
        bool Nim_isPlayerFirstMove = true;
        NumericUpDown[] pileNumerics;
        Stopwatch NimgameT = new Stopwatch();
        #endregion

        #region eredmenyoldal_mezok
        Label jatekvalasztoLabel;
        ComboBox jatekvalaszto;
        ListBox eredmenyListbox;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            #region Form_alapbeallitasok
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(500, 500);
            Text = "Projektmunka";
            #endregion

            #region tabcontroll_setup
            tc = new TabControl
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, ClientSize.Height),
                Appearance = TabAppearance.FlatButtons,
                Dock = DockStyle.Fill,
                Padding = new Point(0, 0),
            };
            tc.SelectedIndexChanged += Tc_SelectedIndexChanged;

            tabPage1 = new TabPage()
            {
                Text = "Név",
            };

            tabPage2 = new TabPage()
            {
                Text = "Fejben 21",
                BackColor = Color.FromArgb(86, 129, 163),
            };
            tabPage3 = new TabPage()
            {
                Text = "Tic-Tac-Toe",
                BackColor = Color.FromArgb(144, 238, 144),

            };
            tabPage4 = new TabPage()
            {
                Text = "Nim játék",
            };
            tabPage5 = new TabPage()
            {
                Text = "Rangsor",
            };
            TabPage[] pages = {tabPage1, tabPage2, tabPage3, tabPage4, tabPage5 };
            tc.TabPages.AddRange(pages);
            Controls.Add(tc);
            #endregion

            #region usernamePage
            nameLabel = new Label()
            {
                Parent = tabPage1,
                Text = "Játékosnév:",
                Size = new Size(100, 50),
                Location = new Point(100, 110),
                Font = new Font("Consolas", 10),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            usernameTextBox = new TextBox()
            {
                Parent = tabPage1,
                Size = new Size(100, 35),
                Location = new Point(200, 126),
                Text = $"Játékos{R.Next(1, 2000)}",
                MaxLength = 15,
            };
            usernameTextBox.TextChanged += UsernameTextBox_TextChanged;

            Button torlesGomb = new Button()
            {
                Parent = tabPage1,
                Text = "X",
                Size = new Size(20, 20),
                Location = new Point(305, 126),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            torlesGomb.Click += TorlesGomb_Click;
            #endregion

            #region fejben21
            panel1 = new Panel()
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(0, 0, 139),
                Height = 100,
                Parent = tabPage2,
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
                ForeColor = Color.FromArgb(255, 255, 255)
            };

            MaxSzam = new NumericUpDown_style()
            {
                Parent = tabPage2,
                Location = new Point(115, 225),
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Minimum = 2,
                Maximum = 7,
                Size = new Size(50, 50),
                ForeColor = Color.FromArgb(255, 255, 255)
            };

            InditoGomb = new GombRounded()
            {
                Parent = tabPage2,
                Location = new Point(260, 213),
                Size = new Size(130, 50),
                Text = "Indítás",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 154, 255),
                //FlatStyle = FlatStyle.Flat,
                BorderSize = 3,
                BorderRadius = 20,
                BorderColor = Color.FromArgb(255, 255, 255),
                TextColor = Color.White,
            };
            InditoGomb.Click += InditoGomb_Click;

            allas = new Label()
            {
                Parent = tabPage2,
                Location = new Point(20, 30),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Text = "Játék állása:",
                Visible = false,
                ForeColor = Color.FromArgb(255, 255, 255)
            };

            HolTartunk = new Label()
            {
                Parent = tabPage2,
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
                Parent = tabPage2,
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
                Parent = tabPage2,
                Location = new Point(115, 265),
                Size = new Size(50, 50),
                Minimum = 1,
                Maximum = 5,
                Visible = false,
                ForeColor = Color.FromArgb(255, 255, 255)
            };

            MehetGomb = new GombRounded()
            {
                Parent = tabPage2,
                Location = new Point(260, 254),
                Text = "Mehet!",
                Font = new Font("Playfair Display", 14f),
                Size = new Size(110, 45),
                BackColor = Color.FromArgb(0, 154, 255),
                //FlatStyle = FlatStyle.Flat,
                Visible = false,
                BorderSize = 3,
                BorderRadius = 20,
                BorderColor = Color.FromArgb(255, 255, 255),
                TextColor = Color.White,
            };
            MehetGomb.Click += MehetGomb_Click;

            VisszaGomb = new GombRounded()
            {
                Parent = tabPage2,
                Location = new Point(280, 330),
                Text = "Vissza",
                Font = new Font("Playfair Display", 11f, FontStyle.Bold),
                Size = new Size(70, 30),
                BackColor = Color.FromArgb(255, 0, 10),
                FlatStyle = FlatStyle.Flat,
                Visible = false,
                BorderSize = 3,
                BorderRadius = 7,
                BorderColor = Color.FromArgb(255, 255, 255),
                TextColor = Color.White,
            };
            VisszaGomb.Click += VisszaGomb_Click;
            #endregion

            #region tic-tac-toe
            int buttonSize = 275 / 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Parent = tabPage3,
                        Size = new Size(buttonSize + 15, buttonSize + 15),
                        Location = new Point(j * (buttonSize + 15) + 88, i * (buttonSize + 15) + 15),
                        Font = new Font("Arial", 16, FontStyle.Bold),
                        Tag = new Point(i, j), // A gomb pozíciója a mátrixban
                        BackColor = Color.FromArgb(44, 95, 45),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                    };
                    buttons[i, j].Click += ButtonClick;
                }
            }
            Label labelStatus = new Label
            {
                Parent = tabPage3,
                Location = new Point(50, 350),
                Size = new Size(250, 50),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "labelStatus",
                Text = "Játék kezdődött! Játékos kezd.",
                ForeColor = Color.FromArgb(0, 0, 0),
            };
            this.labelStatus = labelStatus;

            GombRounded resetButton = new GombRounded
            {
                Parent = tabPage3,
                Location = new Point(350, 350),
                Size = new Size(75, 50),
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Reset",
                BackColor = Color.FromArgb(151, 188, 98),
                BorderSize = 3,
                BorderRadius = 20,
                BorderColor = Color.FromArgb(126, 140, 84),
                TextColor = Color.Black,
            };
            resetButton.Click += ResetGame;
            #endregion

            #region NimGame
            piles = new int[] { R.Next(3, 10), R.Next(3, 10), R.Next(3, 10) };
            pileLabels = new Label[3];
            pileButtons = new Button[3];
            pileNumerics = new NumericUpDown[3];

            NimStatusLabel = new Label()
            {
                Parent = tabPage4,
                Location = new Point(0, 250),
                Size = new Size(250, 50),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(0, 0, 0),
            };

            NimResetButton = new Button()
            {
                Parent= tabPage4,
                Location = new Point(340, 260),
                Size = new Size(55, 30),
                Text = "Kilépés",
                Visible = false,
            };
            NimResetButton.Click += nimResetGame;

            for (int i = 0; i < piles.Length; i++)
            {
                pileLabels[i] = new Label
                {
                    Parent = tabPage4,
                    Text = $"{i + 1}. kupac: {piles[i]}",
                    Location = new Point(50, 65 + i * 40),
                    Size = new Size(200, 30),
                    Font = new Font("Consolas", 10)
                };

                pileButtons[i] = new Button
                {
                    Parent = tabPage4,
                    Text = "Elveszek",
                    Location = new Point(250, 60 + i * 40),
                    Size = new Size(100, 30),
                    Tag = i
                };
                pileButtons[i].Click += RemoveStones;

                pileNumerics[i] = new NumericUpDown
                {
                    Parent = tabPage4,
                    Text = $"{i}",
                    Location = new Point(390, 63 + i * 42),
                    Size = new Size(50, 50),
                    Value = 1,
                    Minimum = 1,
                    Maximum = 3,
                };
            }
            #endregion

            #region Rangsorolas
            jatekvalasztoLabel = new Label()
            {
                Parent = tabPage5,
                Size = new Size(120, 16),
                Location = new Point(40, 13),
                Text = "Kiválaszott játék:",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Consolas", 8),
            };
            jatekvalaszto = new ComboBox()
            {
                Parent = tabPage5,
                Size = new Size(200, 50),
                Location = new Point(160, 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Consolas", 10),
            };
            string[] jatekok = { "Fejben 21", "Tic-Tac-Toe", "Nim játék" };
            jatekvalaszto.Items.AddRange(jatekok);

            jatekvalaszto.SelectedIndexChanged += Jatekvalaszto_update;

            eredmenyListbox = new ListBox()
            {
                Parent = tabPage5,
                Size = new Size(300, 340),
                Location = new Point(80, 50),
                SelectionMode = SelectionMode.None,
                Font = new Font("Consolas", 10), // barmilyen monospace font -> oszlopok szelessege
            };
            #endregion
        }
        void saveToFile(string gameName, int score)
        {
            // meglevo pontszamok beolvasasa
            List<string> osszSor = File.Exists($"{gameName}.txt") ? File.ReadAllLines($"{gameName}.txt").ToList() : new List<string>();

            // a meghivott jatek pontszamanak mentese
            osszSor.Add($"{usernameTextBox.Text};{Convert.ToString(score)};{DateTime.Now.ToString("yyyy-MM-dd_HH:ss")}");
            // sorok mezokre bontasa
            var bontottSorok = osszSor.Select(sor =>
            {
                var mezok = sor.Split(';');
                return new
                {
                    nev = mezok[0],
                    pont = int.Parse(mezok[1]),
                    datum = mezok[2]
                };
            });
            // sorok rendezese pontszam alapjan -> mezok rendezese -> lista konv.
            var sorok = bontottSorok.OrderByDescending(x => x.pont)
                    .Select(x => $"{x.nev};{x.pont};{x.datum}")
                    .ToList();
            // sorok vissairasa
            File.WriteAllLines($"{gameName}.txt", sorok);
        }
        int getScore(Stopwatch T)
        {
            T.Stop();
            // jatek kezdese ota eltelt ido, masopercekre konvertalva
            int time = Convert.ToInt32(T.ElapsedMilliseconds / 1000);
            if (time <= 3)
            {
                return 100;
            }
            else if (time <= 6)
            {
                return 80;
            }
            else if (time <= 9)
            {
                return 60;
            }
            else if (time <= 12)
            {
                return 40;
            }
            else if (time <= 20)
            {
                return 20;
            }
            return 10;
        }

        #region felhasznalonev_fuggvenyek
        void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(usernameTextBox.Text) || usernameTextBox.Text.Any(a => a == ';'))
            {
                nameLabel.ForeColor = Color.Red;
                return;
            }
            nameLabel.ForeColor = Color.Green;
        }

        void Tc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usernameTextBox.Text.Any(a => a == ';')) tc.SelectedTab = tabPage1;
            else if (!String.IsNullOrEmpty(usernameTextBox.Text)) return;

            if (tc.SelectedTab != tabPage1) tc.SelectedTab = tabPage1;
        }

        void TorlesGomb_Click(object sender, EventArgs e)
        {
            usernameTextBox.Clear();
            usernameTextBox.Focus();
        }
        #endregion

        #region rangsorolas_fuggvenyek
        void Jatekvalaszto_update(object sender, EventArgs e)
        {
            // alapertelmezett ertek kivalasztva
            if (jatekvalaszto.SelectedIndex < 0) return;

            eredmenyListbox.Items.Clear();
            var data = GetTop20Scores(jatekvalaszto.Text);

            // ures eredmenyek kezelese
            if (!data.Any())
            {
                eredmenyListbox.Items.Add("Még nincsenek eredmények..");
                return;
            }

            // fejlec es valasztovonal hozzaadasa a tablazathoz
            eredmenyListbox.Items.Add(string.Format("{0,-4}{1,-13}{2,-5}{3,-16}", "","Név","Pont","Dátum" ));
            eredmenyListbox.Items.Add(new string(' ', 3) + new string('-', 38));

            // padding beallitasa -> oszlopok szelessegenek allitasa
            var formattedData = data.Select(sor =>
            {
                var fields = sor.Split(';');
            return string.Format(
                    // rang, nev, pont, datum
                    "{0,-4} {1,-13} {2,-5} {3,-16}",
                    fields[0], fields[1], fields[2], fields[3]);
            });
            // formazott sorok megjelnitese
            eredmenyListbox.Items.AddRange(formattedData.ToArray());
        }

        List<string> GetTop20Scores(string gameName)
        {
            List<string> gyujtottSorok = new List<string>();

            if (File.Exists($"{gameName}.txt"))
            {
                List<string> osszSor = File.ReadAllLines($"{gameName}.txt").ToList();
                int i = 0;
                while (i < osszSor.Count)
                {
                    if (i == 20) break;
                    var sor = osszSor[i].Insert(0, $"{i += 1}. ;");
                    gyujtottSorok.Add(sor);
                }
            }
            return gyujtottSorok;
        }
        #endregion

        #region fejben21_fuggvenyek
        void VisszaGomb_Click(object sender, EventArgs e)
        {
            ResetGame();
            VisszaGomb.Visible = false;
        }
        void ResetGame()
        {
            fejben21T.Stop();
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
            VisszaGomb.Visible = false;
        }
        void InditoGomb_Click(object sender, EventArgs e)
        {
            fejben21T.Restart();
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
            VisszaGomb.Visible = true;

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
                    JatekosKovetkezik();
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
                    ResetGame();
                }
            }
            else
            {
                int pont = getScore(fejben21T);
                MessageBox.Show($"Győztél! Pontszám: {pont}");
                saveToFile("Fejben 21", pont);
                ResetGame();
            }
        }
        bool Kiertekeles()
        {
            if (Osszeg < 21) return false;
            return true;
        }
        #endregion

        #region tic-tac-toe_fuggvenyek
        void ResetGame(object sender, EventArgs e)
        {
            currentPlayer = 'X'; // játékos
            gameEnded = false;
            labelStatus.Text = "Új játék! Játékos kezd";
            isPlayerFirstMove = true;

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
                if (isPlayerFirstMove) TicTacToeT.Restart();
                isPlayerFirstMove = false;

                if (CheckWin(currentPlayer))
                {
                    labelStatus.Text = $"{currentPlayer} nyert!";
                    gameEnded = true;
                    TicTacToeT.Stop();
                    if (currentPlayer == 'X') saveToFile("Tic-Tac-Toe", getScore(TicTacToeT));
                    return;
                }
                else if (IsBoardFull())
                {
                    labelStatus.Text = "Döntetlen!";
                    gameEnded = true;
                    TicTacToeT.Stop();
                    return;
                }
                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                if (currentPlayer == 'O') ComputerMove();
            }
        }
        bool CheckWin(char player)
        {
            // egyenesek
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                    return true;
            }
            // atlok
            return (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                   (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
        }
        bool IsBoardFull()
        {
            foreach (char c in board) if (c == '\0') return false;
            return true;
        }
        void ComputerMove()
        {
            // nyero lepes valasztasa (ha van)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = 'O';
                        if (CheckWin('O'))
                        {
                            buttons[i, j].Text = "O";
                            buttons[i, j].Enabled = false;
                            labelStatus.Text = "O nyert!";
                            gameEnded = true;
                            return;
                        }
                        board[i, j] = '\0';
                    }
                }
            }
            // jatekos blokkolasa
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = 'X';
                        if (CheckWin('X'))
                        {
                            board[i, j] = 'O';
                            buttons[i, j].Text = "O";
                            buttons[i, j].Enabled = false;
                            currentPlayer = 'X';
                            return;
                        }
                        board[i, j] = '\0';
                    }
                }
            }
            // kozep elfoglalasa
            if (board[1, 1] == '\0')
            {
                board[1, 1] = 'O';
                buttons[1, 1].Text = "O";
                buttons[1, 1].Enabled = false;
                currentPlayer = 'X';
                return;
            }
            // maradek mezok
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = 'O';
                        buttons[i, j].Text = "O";
                        buttons[i, j].Enabled = false;
                        currentPlayer = 'X';
                        return;
                    }
                }
            }
        }
        #endregion

        #region NimGame_fuggvenyek
        void RemoveStones(object sender, EventArgs e)
        {
            if (!isPlayerTurn || IsGameOver()) return;

            if (Nim_isPlayerFirstMove)
            {
                NimgameT.Restart();
                Nim_isPlayerFirstMove = false;
            }


            Button button = (Button)sender;
            int pileIndex = (int)button.Tag;
            int value = (int)pileNumerics[pileIndex].Value;

            if (piles[pileIndex] > 0)
            {
                piles[pileIndex] -= value;
                isPlayerTurn = false;
                UpdateLabelText();
                UpdateNumericMaximum();

                if (IsGameOver())
                {
                    int pont = getScore(NimgameT);
                    NimStatusLabel.Text = $"Nyertél!";
                    NimStatusLabel.BackColor = Color.Black;
                    MessageBox.Show($"Elért pontszám: {pont}");
                    saveToFile("Nim játék", pont);
                    RestartGame();
                    return;
                }

                NimComputerMove();
                UpdateLabelText();
                UpdateNumericMaximum();

                if (IsGameOver())
                {
                    MessageBox.Show("Vesztettél!");
                    RestartGame();
                    return;
                }
                else
                {
                    NimStatusLabel.ForeColor = Color.Black;
                    NimStatusLabel.Text = "Te következel!";
                    NimResetButton.Visible = true;
                }
            }
            else
            {
                NimStatusLabel.ForeColor = Color.Red;
                NimStatusLabel.Text = "Érvénytelen lépés!";
                NimResetButton.Visible = false;
            }
        }

        void nimResetGame(object sender, EventArgs e)
        {
            RestartGame();
            NimStatusLabel.Text = "Játék megszakítva";
        }

        void NimComputerMove()
        {
            for (int i = 0; i < piles.Length; i++)
            {
                if (piles[i] > 0)
                {
                    if (piles[i] <= 3)
                    {
                        piles[i] -= piles[i];
                    }
                    else
                    {
                        piles[i] -= R.Next(1, 3);
                    }
                    isPlayerTurn = true;
                    break;
                }
            }
        }

        void UpdateLabelText()
        {
            for (int i = 0; i < pileLabels.Length; i++)
            {
                pileLabels[i].Text = $"{i + 1}. kupac: {piles[i]}";
            }
        }

        void UpdateNumericMaximum()
        {
            for (int i = 0; i < piles.Length; i++)
            {
                if (piles[i] <= 3)
                {
                    pileNumerics[i].Maximum = piles[i];
                }
            }
        }
        bool IsGameOver()
        {
            foreach (var pile in piles)
            {
                if (pile > 0) return false;
            }
            return true;
        }
        void RestartGame()
        {
            NimgameT.Stop();
            Nim_isPlayerFirstMove = true;
            NimResetButton.Visible = false;
            for (int i = 0; i < piles.Length; i++)
            {
                pileNumerics[i].Maximum = 3;
            }

            for (int i = 0; i < piles.Length; i++)
            {
                piles[i] = R.Next(3, 10);
                pileLabels[i].Text = $"{i + 1}. kupac: {piles[i]}";
                pileNumerics[i].Value = 1;
                pileNumerics[i].Minimum = 1;
                pileNumerics[i].Maximum = 3;
            }

            foreach (var button in pileButtons)
            {
                button.Enabled = true;
            }

            NimStatusLabel.ForeColor = Color.Black;
            NimStatusLabel.Text = "Te következel!";
            isPlayerTurn = true;
        }
    
    #endregion
    }
}