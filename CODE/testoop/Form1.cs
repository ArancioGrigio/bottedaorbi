using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testoop
{
    public partial class Form1 : Form
    {
        public int MAX_HEALTH = 300;    //Da modificare anche in Class Character this.health !
        Character player1;
        Character player2;
        Move moveEnemy;
        Random random = new Random();
        SoundPlayer simpleSound;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRoaster();
        }

        /* Ricomincia */
        private void RestartGame()
        {
            /* Interrompe eventuali audio in esecuzione */
            simpleSound.Stop();
            timerEntry.Stop();
            timerEnding.Stop();

            /* Sparire selezionatori */
            selectCharacter1.Visible = true;
            selectCharacter2.Visible = true;
            labelVS.Visible = true;
            buttonStart.Visible = true;

            /* Imposta il ring */
            BackgroundImage = Properties.Resources.bottedaorbi;

            /* Apparire Pulsanti di Gioco */
            picturePlayer1.Visible = false;
            picturePlayer2.Visible = false;
            buttonMove1.Visible = false;
            buttonMove2.Visible = false;
            buttonMove3.Visible = false;
            buttonMove4.Visible = false;
            labelNome1.Visible = false;
            labelNome2.Visible = false;
            labelVita1.Visible = false;
            labelVita2.Visible = false;
            labelSpeech.Visible = false;

            /* Disabilita pulsanti di gioco */
            buttonMove1.Enabled = false;
            buttonMove2.Enabled = false;
            buttonMove3.Enabled = false;
            buttonMove4.Enabled = false;
        }


        /* Carica i personaggi del Roaster */
        private void LoadRoaster()
        {
            foreach (var file in new DirectoryInfo("characters").GetFiles("*.txt"))
            {
                string[] character_text = File.ReadAllLines("characters/" + file.Name);
                selectCharacter1.Items.Add(character_text[0]);
                selectCharacter2.Items.Add(character_text[0]);
            }
        }

        /* Aggiornamento della GUI */
        private void updateGUI()
        {
            labelVita1.Text = player1.Health.ToString();
            labelVita2.Text = player2.Health.ToString();
            picturePlayer1.BackgroundImage = player1.GetSprite();
            picturePlayer2.BackgroundImage = player2.GetSprite();
            labelNome1.Text = player1.GetName();
            labelNome2.Text = player2.GetName();
        }
        

        /* Verifica se il gioco è terminato */
        private void CheckEnd()
        {
            if (player1.isDead() || player2.isDead())
            {
                buttonMove1.Visible = false;
                buttonMove2.Visible = false;
                buttonMove3.Visible = false;
                buttonMove4.Visible = false;
                timerEnding.Start();
                //Thread.Sleep(10000);
            }
        }

        /* Attacca P1 */
        private void AttackP1(Move move, Move moveEnemy)
        {
            labelSpeech.Text += player1.GetName() + ": “" + move.Sentence + "”;\n\r" + player1.GetName() + " ha usato " + move.Name;
            if (move.Chance >= (random.Next(0, 11) * 10))
            {
                labelSpeech.Text += ".\n\r";
                player2.Health -= Convert.ToInt32((Convert.ToDouble(player1.Atk) * 0.01 + 1) * move.Dmg);
                player1.Health += move.Restore;
            }
            else labelSpeech.Text += " (FALLITA) ";

            if (player1.Health > MAX_HEALTH)                   //Evita che la vita superi 100 (LIMITE)
                player1.Health = MAX_HEALTH;

            updateGUI();
            CheckEnd();
        }

        /* Attacca P2 */
        private void AttackP2(Move move, Move moveEnemy)
        {
            labelSpeech.Text += player2.GetName() + ": “" + moveEnemy.Sentence + "”;\n\r" + player2.GetName() + " ha usato " + moveEnemy.Name;
            if (moveEnemy.Chance >= (random.Next(0, 11) * 10))
            {
                labelSpeech.Text += ".\n\r";
                player1.Health -= Convert.ToInt32((Convert.ToDouble(player2.Atk) * 0.01 + 1) * moveEnemy.Dmg);
                player2.Health += moveEnemy.Restore;
            }
            else labelSpeech.Text += " (FALLITA)\n\r";

            if (player2.Health > MAX_HEALTH)                   //Evita che la vita superi 100 (LIMITE)
                player2.Health = MAX_HEALTH;

            updateGUI();
            CheckEnd();
        }

        private void Turno(Move move)
        {
            /* Aggiornamento testo Speech */
            labelSpeech.Text = "";

            /* Mossa avversario */
            switch(random.Next(1, 5))
            {
                case 1:
                    moveEnemy = player2.Move1;
                    break;
                case 2:
                    moveEnemy = player2.Move2;
                    break;
                case 3:
                    moveEnemy = player2.Move3;
                    break;
                case 4:
                    moveEnemy = player2.Move4;
                    break;
            }

            /* Chi attacca per primo */
            if (move.Speed != moveEnemy.Speed)
                if(move.Speed > moveEnemy.Speed)
                {
                    AttackP1(move, moveEnemy);
                    AttackP2(move, moveEnemy);
                }
                else
                {
                    AttackP2(move, moveEnemy);
                    AttackP1(move, moveEnemy);
                }
            else
            {
                if(random.Next(0, 2) == 0)
                {
                    AttackP1(move, moveEnemy);
                    AttackP2(move, moveEnemy);
                }
                else
                {
                    AttackP2(move, moveEnemy);
                    AttackP1(move, moveEnemy);
                }
                
            }

            updateGUI();
        }

        private Character SelectCharacter(string character)
        {
            Character player;

            /* (Default) - Constantin */
            player = new Character("Constantin",
                                        "Devil Constantin",
                                        "Fratello Carabiniere: “Ma sei sempre? Sei sempre tu che rompi i coglioni?”.\n\rConstantin: “Apri la bocca COGLIONE!”",
                                        "sounds/constantin_entry.wav",
                                        "",
                                        20,
                                        20,
                                        new Bitmap("sprites/constantin1.png"),
                                        new Bitmap("sprites/constantin2.png"),
                                        new Move("Prendi a cazzotti", "Prendi questo, vai a fare in culo pezzo di merda!", 20, 70, 100, 0),
                                        new Move("Allak di Merda", "Allak di Merda! Aldo Mori ti stavo aspettando..", 30, 50, 100, 0),
                                        new Move("Telefonata persa", "Ho ricevuto una chiamata coglione, tua mamma me lo succhia?", 0, 20, 100, 40),
                                        new Move("Invito letale", "COGLIONE VIENI IN PIAZZA MARSALA, SONO LA TUA MORTE!", 60, 50, 20, 0)
                                        );

            /* Prende i personaggi dai .txt */
            foreach (var file in new DirectoryInfo("characters").GetFiles("*.txt"))
            {
                string[] character_text = File.ReadAllLines("characters/" + file.Name);
                if(character_text[0] == character)
                    player = new Character(character_text[0],
                                       character_text[1],
                                       character_text[2],
                                       "sounds/" + character_text[3],
                                       character_text[4],
                                       Int32.Parse(character_text[5]),
                                       Int32.Parse(character_text[6]),
                                       new Bitmap("sprites/" + character_text[7]),
                                       new Bitmap("sprites/" + character_text[8]),
                                       new Move(character_text[9], character_text[10], Int32.Parse(character_text[11]), Int32.Parse(character_text[12]), Int32.Parse(character_text[13]), Int32.Parse(character_text[14])),
                                       new Move(character_text[15], character_text[16], Int32.Parse(character_text[17]), Int32.Parse(character_text[18]), Int32.Parse(character_text[19]), Int32.Parse(character_text[20])),
                                       new Move(character_text[21], character_text[22], Int32.Parse(character_text[23]), Int32.Parse(character_text[24]), Int32.Parse(character_text[25]), Int32.Parse(character_text[26])),
                                       new Move(character_text[27], character_text[28], Int32.Parse(character_text[29]), Int32.Parse(character_text[30]), Int32.Parse(character_text[31]), Int32.Parse(character_text[32]))
                                       );
            }
            
            return player;
        }

        private void StartGame()
        {
            /* Sparire selezionatori */
            selectCharacter1.Visible = false;
            selectCharacter2.Visible = false;
            labelVS.Visible = false;
            buttonStart.Visible = false;

            /* Imposta il ring */
            BackgroundImage = null;

            /* Apparire Pulsanti di Gioco */
            picturePlayer1.Visible = true;
            picturePlayer2.Visible = true;
            buttonMove1.Visible = true;
            buttonMove2.Visible = true;
            buttonMove3.Visible = true;
            buttonMove4.Visible = true;
            labelNome1.Visible = true;
            labelNome2.Visible = true;
            labelVita1.Visible = true;
            labelVita2.Visible = true;
            labelSpeech.Visible = true;



            /* Selezione giocatori */
            player1 = SelectCharacter(selectCharacter1.Text);
            player2 = SelectCharacter(selectCharacter2.Text);



            /* Personalizzazione Layout Partita */

            /* Imposta Bottoni Mosse */
            buttonMove1.Text = player1.Move1.Name + "\n\r ⚔ " + player1.Move1.Dmg + "  🍃 " + player1.Move1.Speed + "  % " + player1.Move1.Chance + "  ♥ " + player1.Move1.Restore;
            buttonMove2.Text = player1.Move2.Name + "\n\r ⚔ " + player1.Move2.Dmg + "  🍃 " + player1.Move2.Speed + "  % " + player1.Move2.Chance + "  ♥ " + player1.Move2.Restore;
            buttonMove3.Text = player1.Move3.Name + "\n\r ⚔ " + player1.Move3.Dmg + "  🍃 " + player1.Move3.Speed + "  % " + player1.Move3.Chance + "  ♥ " + player1.Move3.Restore;
            buttonMove4.Text = player1.Move4.Name + "\n\r ⚔ " + player1.Move4.Dmg + "  🍃 " + player1.Move4.Speed + "  % " + player1.Move4.Chance + "  ♥ " + player1.Move4.Restore;

            /* Nome - Vita - Sprite */
            updateGUI();


            /* Frasi di ingresso: */
            labelSpeech.Text = player1.Entry;
            simpleSound = new SoundPlayer(player1.Entry_sound);
            simpleSound.Play();
            timerEntry.Start();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void timerEntry_Tick(object sender, EventArgs e)
        {
            labelSpeech.Text = player2.Entry;
            simpleSound = new System.Media.SoundPlayer();
            simpleSound.SoundLocation = player2.Entry_sound;
            simpleSound.Load();
            simpleSound.Play();

            /* Abilita Bottoni Mosse dopo Entry */
            buttonMove1.Enabled = true;
            buttonMove2.Enabled = true;
            buttonMove3.Enabled = true;
            buttonMove4.Enabled = true;
            timerEntry.Stop();
        }

        private void buttonMove1_Click(object sender, EventArgs e)
        {
            Turno(player1.Move1);
        }

        private void buttonMove2_Click(object sender, EventArgs e)
        {
            Turno(player1.Move2);
        }

        private void buttonMove3_Click(object sender, EventArgs e)
        {
            Turno(player1.Move3);
        }

        private void buttonMove4_Click(object sender, EventArgs e)
        {
            Turno(player1.Move4);
        }

        private void timerEnding_Tick(object sender, EventArgs e)
        {
            /* Frase del vincitore: ending */
            if (player2.isDead())
                labelSpeech.Text = player1.GetName() + ": “" + player1.Ending + "”.";
            else
                labelSpeech.Text = player2.GetName() + ": “" + player2.Ending + "”.";
        }

        private void bt_restart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }

    class Move {
        private string name;
        private string sentence;
        private int dmg;
        private int speed;
        private int chance;
        private int restore;

        public Move(string name,string sentence, int dmg, int speed, int chance, int restore)
        {
            this.name = name;
            this.sentence = sentence;
            this.dmg = dmg;
            this.speed = speed;
            this.chance = chance;
            this.restore = restore;
        }

        public string Name { get => name; set => name = value; }
        public string Sentence { get => sentence; set => sentence = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Chance { get => chance; set => chance = value; }
        public int Restore { get => restore; set => restore = value; }
    }

    class Character {
        private string name;
        private string name_low;
        private string entry;
        private string entry_sound;
        private string ending;
        private int health;
        private int atk;
        private int def;

        private Image sprite1;
        private Image sprite2;

        private Move move1;
        private Move move2;
        private Move move3;
        private Move move4;





        /* Metodi standard */
        public Character(string name, string name_low, string entry, string entry_sound, string ending, int atk, int def, Image sprite1, Image sprite2, Move move1, Move move2, Move move3, Move move4)
        {
            this.name = name;
            this.name_low = name_low;
            this.entry = entry;
            this.entry_sound = entry_sound;
            this.ending = ending;
            this.health = 300;
            this.atk = atk;
            this.def = def;
            this.sprite1 = sprite1;
            this.sprite2 = sprite2;
            this.move1 = move1;
            this.move2 = move2;
            this.move3 = move3;
            this.move4 = move4;
        }


        public string Name { get => name; set => name = value; }
        public string Name_low { get => name_low; set => name_low = value; }
        public string Entry { get => entry; set => entry = value; }
        public string Ending { get => ending; set => ending = value; }
        public int Health { get => health; set => health = value; }
        public int Atk { get => atk; set => atk = value; }
        public int Def { get => def; set => def = value; }
        public string Entry_sound { get => entry_sound; set => entry_sound = value; }
        internal Move Move1 { get => move1; set => move1 = value; }
        internal Move Move2 { get => move2; set => move2 = value; }
        internal Move Move3 { get => move3; set => move3 = value; }
        internal Move Move4 { get => move4; set => move4 = value; }


        /* Metodi custom */
        public Image GetSprite()
        {
            if (this.Health > 50)
                return this.sprite1;
            else
                return this.sprite2;
        }

        public string GetName()
        {
            if (this.Health > 50)
                return this.name;
            else
                return this.Name_low;
        }

        public bool isDead() {
            if (Health <= 0)
                return true;
            return false;
        }
    }
}
