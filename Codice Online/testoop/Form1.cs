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
using System.Net.Http;

namespace testoop
{
    
    public partial class Form1 : Form
    {
        public static readonly HttpClient client = new HttpClient();

        public int MAX_HEALTH = 300;    //Da modificare anche in Class Character this.health !
        Character player1;
        Character player2;
        Opponent opponent;
        Move moveEnemy;
        Random random = new Random();
        SoundPlayer simpleSound;

        /* Online vars */
        string my_ip;
        string chosen_oppo = "";
        Move my_move;
        bool my_success;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitSql();
            timerPing.Start();
            Ping();
            LoadRoaster();  //Dentro chiama anche FindOppo();
        }

        /* Inizializza il record dell'utente sull'Sql e
         * Prende il proprio IP */
        private async Task InitSql()
        {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/init.php");
            my_ip = php_response;
            labelTag.Text = "Tag: " + my_ip;
        }

        private async Task Ping()
        {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/ping.php?oppo=" + chosen_oppo + "&&character=" + selectCharacterBox.Text);
        }

        private async Task CheckOppoOnline()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/check_oppo_online.php?oppo=" + chosen_oppo);
            if (Int32.Parse(php_response) <= (t.TotalSeconds - 20))
            {
                /* Se l'avversario non risponde nel tempo, vinci! */
                player2.Health = -666;
                updateGUI();
                CheckEnd();
            }
        }

        /* Prende gli ip degli avversari on-line e li mette in selectOppoBox */
        private async Task FindOppo()
        {
            /* Effettua la richiesta */
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/opponents_ips.php");
            string[] ips = php_response.Split(',');

            selectOppoBox.Items.Clear();
            foreach (string ip in ips){
                if ((ip != "") && (ip != my_ip))
                {
                    selectOppoBox.Items.Add(ip);
                }
            }
        }

        /* Attendi che l'avversario scelto scelga te come proprio avversario */
        private async Task WaitOppo()
        {
            buttonStart.Enabled = false;
            selectCharacterBox.Enabled = false;
            selectOppoBox.Enabled = false;
            refreshButton.Enabled = false;
            Ping();
            timerWaitOppo.Start();
        }

        private async Task<string> GetOppoOppo()
        {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/get_oppo_oppo.php?oppo_ip=" + chosen_oppo);
            labelTag.Text = php_response;
            return php_response;
        }

        /* Richiede l'avversario scelto dal tuo avversario E imposta sull'sql move a 0 */
        private async Task<string> GetOppoCharacter() {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/get_oppo_character.php?oppo_ip=" + selectOppoBox.Text);
            return php_response;
        }

        /* Rincomincia */
        private void RestartGame()
        {
            /* Interrompe eventuali audio in esecuzione */
            simpleSound.Stop();
            timerEntry.Stop();
            timerEnding.Stop();

            /* Sparire selezionatori */
            selectCharacterBox.Visible = true;
            selectOppoBox.Visible = true;
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
                selectCharacterBox.Items.Add(character_text[0]);
            }
            FindOppo();
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

                timerPing.Stop();
                timerCheckOppoOnline.Stop();
                timerWaitMove.Stop();
                timerEnding.Start();
                //Thread.Sleep(10000);
            }
        }

        /* Vede se la mossa è andata a buon segno */
        private bool IsSuccess(Move move)
        {
            if (move.Chance >= (random.Next(0, 11) * 10))
                return true;
            return false;
        }

        /* Invia la mossa scelta al server ed */
        /* invia se la mossa è fallita o meno */
        private async Task SendMove(int n_move, bool success)
        {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/send_move.php?n_move=" + n_move + "&&success=" + (Convert.ToInt32(success)));
            /* Disabilita pulsanti di gioco fino al prossimo turnoh */
            buttonMove1.Enabled = false;
            buttonMove2.Enabled = false;
            buttonMove3.Enabled = false;
            buttonMove4.Enabled = false;

            labelSpeech.Text = "L'avversario sta scegliendo";
            timerWaitMove.Start();
        }
        /* Attacca P1 */
        private void AttackP1(Move move,bool my_success)
        {
            labelSpeech.Text += player1.GetName() + ": “" + move.Sentence + "”;\n\r" + player1.GetName() + " ha usato " + move.Name;
            if (my_success)
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
        private void AttackP2(Move moveEnemy, bool oppo_success)
        {
            labelSpeech.Text += player2.GetName() + ": “" + moveEnemy.Sentence + "”;\n\r" + player2.GetName() + " ha usato " + moveEnemy.Name;
            if (oppo_success)
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

        /* Chiede al server la mossa scelta dall'avversario e se è un successo */
        private async Task<string> GetOppoNMove()
        {
            string php_response = await client.GetStringAsync("http://www.pippotaxi.altervista.org/bottedaorbi/get_oppo_move.php?oppo_ip=" + opponent.Ip);
            string[] vars = php_response.Split(',');
            int n_move = Int32.Parse(vars[0]);
            int time_last_move = Int32.Parse(vars[1]);
            string success = vars[2];   //'0': Fallita; '1': Successo



            //TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            /* Se l'ultima mossa dell'avversario è 0 (default value) -\ o se è stata effettuata più di 50 secondi fa /-
             * o se è la mossa effettuata al turno precedente, Non continua */
            if (n_move != 0 && (opponent.Time_last_move < (time_last_move - 1) || opponent.Time_last_move > (time_last_move + 1)))
            {
                /* Aggiorna il tempo dell'ultima mossa effettuata dall'avversario, cosìcchè, al prossimo turno,
                 * si possa verificare se la mossa sull'sql è quella del turno prima o no*/
                opponent.Time_last_move = time_last_move; //Aggiorno il tempo dell'ultima mossa effettuata dall'avversario
                opponent.Move = n_move;

                return opponent.Move.ToString() + "," + success;
            }

            return "";
        }

        private void Turno(Move move, Move moveEnemy, bool my_success, bool oppo_success)
        {
            /* Aggiornamento testo Speech */
            labelSpeech.Text = "";
            /* Chi attacca per primo */
            if (move.Speed != moveEnemy.Speed)
                if(move.Speed > moveEnemy.Speed)
                {
                    AttackP1(move, my_success);
                    AttackP2(moveEnemy, oppo_success);
                }
                else
                {
                    AttackP2(moveEnemy, oppo_success);
                    AttackP1(move, my_success);
                }
            else
            {
                if(random.Next(0, 2) == 0)
                {
                    AttackP1(move, my_success);
                    AttackP2(moveEnemy, oppo_success);
                }
                else
                {
                    AttackP2(moveEnemy, oppo_success);
                    AttackP1(move, my_success);
                }
                
            }

            updateGUI();
            /* Abilita Bottoni Mosse per il prossimo turno */
            buttonMove1.Enabled = true;
            buttonMove2.Enabled = true;
            buttonMove3.Enabled = true;
            buttonMove4.Enabled = true;
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
            /* Fa partire il controllo dello stato dell'avversario */
            timerCheckOppoOnline.Start();

            /* Sparire selezionatori */
            selectCharacterBox.Visible = false;
            selectOppoBox.Visible = false;
            refreshButton.Visible = false;
            labelVS.Visible = false;
            labelWaitOppo.Visible = false;
            labelTag.Visible = false;
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
            player1 = SelectCharacter(selectCharacterBox.Text);
            player2 = SelectCharacter(opponent.Character);



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
            chosen_oppo = selectOppoBox.Text;
            labelWaitOppo.Visible = true;
            WaitOppo();
        }

        int index_entry = 0;
        private void timerEntry_Tick(object sender, EventArgs e)
        {
            if (index_entry == 0)
            {
                labelSpeech.Text = player2.Entry;
                simpleSound = new System.Media.SoundPlayer();
                simpleSound.SoundLocation = player2.Entry_sound;
                simpleSound.Load();
                simpleSound.Play();
                index_entry = 1;
            }
            else
            {

                /* Abilita Bottoni Mosse dopo Entry */
                buttonMove1.Enabled = true;
                buttonMove2.Enabled = true;
                buttonMove3.Enabled = true;
                buttonMove4.Enabled = true;
                timerEntry.Stop();
                index_entry = 0;
            }
        }

        private void buttonMove1_Click(object sender, EventArgs e){
            /* Verifica se la mossa è un successo ed invia mossa e bool success al server Sql*/

            my_move = player1.Move1;
            my_success = IsSuccess(my_move);
            SendMove(1, my_success);
        }

        private void buttonMove2_Click(object sender, EventArgs e)
        {
            my_move = player1.Move2;
            my_success = IsSuccess(my_move);
            SendMove(2, my_success);
        }

        private void buttonMove3_Click(object sender, EventArgs e)
        {
            my_move = player1.Move3;
            my_success = IsSuccess(my_move);
            SendMove(3, my_success);
        }

        private void buttonMove4_Click(object sender, EventArgs e)
        {
            my_move = player1.Move4;
            my_success = IsSuccess(my_move);
            SendMove(4, my_success);
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            FindOppo();
        }

        private void timerPing_Tick(object sender, EventArgs e)
        {
            Ping();
        }

        private async Task timerWaitOppo_TickAsync()
        {
            if (labelWaitOppo.Text == "Waiting...")
                labelWaitOppo.Text = "Waiting";
            else
                labelWaitOppo.Text += ".";

            /* Verifica se l'avversario scelto coincide con l'avversario scelto dall'avversario scelto */
            //labelTag.Text = GetOppoOppo().Result + "a";

            //labelTag.Text = GetOppoOppo().Result;
            string x = await GetOppoOppo();
            //labelTag.Text = x;
            if (x == my_ip)
            {
                //Crea oggetto con Personaggio ed Ip dell'avversario
                opponent = new Opponent((await GetOppoCharacter()), selectOppoBox.Text);
                timerWaitOppo.Stop();
                StartGame();
            }
        }

        private void timerWaitOppo_Tick(object sender, EventArgs e)
        {
            timerWaitOppo_TickAsync();
        }

        private async Task TimerWaitMove_TickAsync()
        {
            if (labelSpeech.Text != "L'avversario sta scegliendo...")
                labelSpeech.Text += ".";
            else
                labelSpeech.Text = "L'avversario sta scegliendo";

            /* Chiede la mossa dell'avversario */
            string result = await GetOppoNMove();
            int n_move = Int32.Parse(result.Split(',')[0]);
            bool oppo_success;
            if (Int32.Parse(result.Split(',')[1]) == 0)
                oppo_success = false;
            else
                oppo_success = true;

            if (n_move != 0)
            {
                switch (n_move)
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

                timerWaitMove.Stop();
                Turno(my_move, moveEnemy, my_success, oppo_success);
            }
        }

        private void timerWaitMove_Tick(object sender, EventArgs e)
        {
            TimerWaitMove_TickAsync();
        }

        private void timerCheckOppoOnline_Tick(object sender, EventArgs e)
        {
            CheckOppoOnline();
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

    class Opponent
    {
        private string character;
        private string ip;
        private int move;
        private int time_last_move;

        public Opponent(string character, string ip)
        {
            this.Character = character;
            this.Ip = ip;
            this.Move = 0;
            this.Time_last_move = 0;
        }

        public string Character { get => character; set => character = value; }
        public string Ip { get => ip; set => ip = value; }
        public int Move { get => move; set => move = value; }
        public int Time_last_move { get => time_last_move; set => time_last_move = value; }
    }
}
