using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs201_lab_Monitorowanie_pozycji_zawodnikow
{
    public class PlayerEventArgs: EventArgs
    {
        public Location location;

        public PlayerEventArgs((int x, int y) position, DateTime time)
        {
            this.location = new Location(time, position);
        }
    }


    public class Player
    {
        static private int maxID = -1;

        public readonly int id;
        public (int x, int y) position;

        public EventHandler<PlayerEventArgs> Moved;

        public Player(int positionX, int positionY)
        {
            maxID += 1;
            id = maxID;
            position.x = positionX;
            position.y = positionY;
        }

        public void MoveTo((int x, int y) newPosition)
        {
            if (position.x == newPosition.x && position.y == newPosition.y)
                return;

            position = newPosition;
            OnMove();
        }

        protected virtual void OnMove()
        {
            Moved?.Invoke(this, new PlayerEventArgs(this.position, DateTime.Now));
        }
    }
}
