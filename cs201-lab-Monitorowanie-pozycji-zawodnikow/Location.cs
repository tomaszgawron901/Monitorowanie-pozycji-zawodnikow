using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs201_lab_Monitorowanie_pozycji_zawodnikow
{
    public class Location
    {
        public DateTime time;
        public (int x, int y) position;
        public Location(DateTime time, (int x, int y) position)
        {
            this.time = time;
            this.position = position;
        }
    }
}
