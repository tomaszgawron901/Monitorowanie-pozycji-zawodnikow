using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs201_lab_Monitorowanie_pozycji_zawodnikow
{
    public class Tracker
    {
        public Dictionary<int, Queue<Location>> playersPath;
        public Tracker()
        {
            playersPath = new Dictionary<int, Queue<Location>>();
        }

        public void ObservePlayer(Player player)
        {
            player.Moved += OnPlayerMove;
            playersPath.Add(player.id, new Queue<Location>());
        }

        public void OnPlayerMove(object source, PlayerEventArgs args)
        {
            var player = source as Player;
            playersPath[player.id].Enqueue(args.location);
        }

    }
}
