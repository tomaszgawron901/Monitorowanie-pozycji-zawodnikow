using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace cs201_lab_Monitorowanie_pozycji_zawodnikow
{
    public class PlayerMoveThreadManager
    {
        private static int maxMove = 40;
        private static int moveInterval = 100;

        private static Random rand;
        private Board board;
        private Thread thread;
        private bool isMoving;

        private Player player;
        public Player Player
        {
            get { return player; }
        }

        public PlayerMoveThreadManager(Player player, Board board)
        {
            this.player = player;
            this.board = board;
            rand = new Random();
            this.isMoving = false;
        }

        public void StartMoving()
        {
            if (isMoving) return;
            isMoving = true;
            thread = new Thread(new ThreadStart(MovePlayerRandomly));
            thread.Start();
        }

        public void StopMoving()
        {
            isMoving = false;
        }

        private void MovePlayerRandomly()
        {
            while(isMoving)
            {
                player.MoveTo(getNewRandomPosition());
                Thread.Sleep(moveInterval);
            }
        }

        private (int x, int y) getNewRandomPosition()
        {
            int top = player.position.y + maxMove;
            int bottom = player.position.y - maxMove;
            int right = player.position.x + maxMove;
            int left = player.position.x - maxMove;

            if (top > board.height) top = board.height;
            if (bottom < 0) bottom = 0;
            if (right > board.width) right = board.width;
            if (left < 0) left = 0;

            return (x: rand.Next(left, right+1), y: rand.Next(bottom, top+1));
        }
    }
}
