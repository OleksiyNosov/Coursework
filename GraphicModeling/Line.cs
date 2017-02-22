using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GraphicModeling
{
    class SLine
    {
        private Coord start;
        private Coord end;

        public Coord Start
        {
            get { return start; }
            set { start = value; }
        }
        public Coord End
        {
            get { return end; }
            set { end = value; }
        }


        public SLine(Coord start, Coord end)
        {
            Start = start;
            End = end;
        }
        public SLine() : this(new Coord(), new Coord()) { }

        public static bool IsLinesIntersect(SLine line1, SLine line2)
        {
            return CrossProduct(line1.Start, line1.End, line2.Start) 
                != CrossProduct(line1.Start, line1.End, line2.End) 
                || CrossProduct(line2.Start, line2.End, line1.Start) 
                != CrossProduct(line2.Start, line2.End, line1.End);
        }

        private static double CrossProduct(Coord c1, Coord c2, Coord c3)
        {
            return (c2.X - c1.X) * (c3.Y - c1.Y) - (c3.X - c1.X) * (c2.Y - c1.Y);
        }
    }
}
