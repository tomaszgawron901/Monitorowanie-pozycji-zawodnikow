using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs201_lab_Monitorowanie_pozycji_zawodnikow
{
    public struct Board
    {
        public int width;
        public int height;
        public Board(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Cannot create board with negative dimentions.");
            this.width = width;
            this.height = height;
        }
    }
}
