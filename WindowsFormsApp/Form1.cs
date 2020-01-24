using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using cs201_lab_Monitorowanie_pozycji_zawodnikow;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<PlayerMoveThreadManager> players;
        private Tracker tracker;

        private bool isDrawing;
        private Thread drawingThread;
        private static int drawingInterval = 100;
        private Graphics canvas;

        private void start(Board board, int numberOfPlayers)
        {
            tracker = new Tracker();
            players = CreatePlayersList(numberOfPlayers, board);
            foreach (var playerManager in players)
            {
                tracker.ObservePlayer(playerManager.Player);
                playerManager.StartMoving();
            }
        }

        private void stop()
        {
            if (players == null) return;
            foreach(var playerManager in players)
            {
                playerManager.StopMoving();
            }
        }

        private void startDrawing()
        {
            drawingThread = new Thread(new ThreadStart(startDrawLoop));
            drawingThread.Start();
        }

        private void StopDrawing()
        {
            isDrawing = false;
        }

        private void startDrawLoop()
        {
            if (isDrawing) return;
            isDrawing = true;
            while(isDrawing)
            {
                draw();
                Thread.Sleep(drawingInterval);
            }
        }

        private void createCanvas()
        {
            canvas = this.CreateGraphics();
        }

        private void draw()
        {
            if (canvas is null)
                return;
            canvas.Clear(Color.White);
            foreach(var playerManager in players)
            {
                canvas.FillEllipse(Brushes.Red, playerManager.Player.position.x, playerManager.Player.position.y, 10, 10);
                canvas.DrawString(playerManager.Player.id.ToString(), new Font(new FontFamily("Arial"), 20), Brushes.Black , playerManager.Player.position.x, playerManager.Player.position.y-20);
            }
        }

        private void drawPath(int idPlayer)
        {
            if (tracker is null) return;
            if (canvas is null) return;
            canvas.Clear(Color.White);

            if (!tracker.playersPath.ContainsKey(idPlayer)) return;

            Queue<Location> locationQueue = tracker.playersPath[idPlayer];

            Location preloc = null;
            foreach(var location in locationQueue)
            {
                if(preloc is null)
                {
                    preloc = location;
                }
                else
                {
                    canvas.DrawLine(Pens.Blue, preloc.position.x, preloc.position.y, location.position.x, location.position.y);
                    preloc = location;
                }
            }

        }

        private List<PlayerMoveThreadManager> CreatePlayersList(int amount, Board board)
        {
            comboBox1.Items.Clear();
            var outputList = new List<PlayerMoveThreadManager>();
            for(int i = 0; i < amount; i++)
            {
                var playermanager = new PlayerMoveThreadManager(new Player(board.width / 2, board.height / 2), board);
                outputList.Add(playermanager);
                comboBox1.Items.Add(playermanager.Player.id);
            }
            return outputList;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            createCanvas();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (numericUpDown1.Value <= 0) return;
            if (isDrawing) stop();
            start(new Board(400, 400), decimal.ToInt32(numericUpDown1.Value));
            startDrawing();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isDrawing)
            {
                stop();
                StopDrawing();
            }
            if (comboBox1.Text == null || comboBox1.Text == "") return;
            drawPath(int.Parse(comboBox1.Text));
        }
    }
}
