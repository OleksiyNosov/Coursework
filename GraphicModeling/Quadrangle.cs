using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GraphicModeling
{
    public class Quadrangle : Shape
    {

        public Quadrangle(Coord topFirst, Coord topSecond, Coord topThird, Coord topFourth)
        {
            Coords = new Coord[] { topFirst, topSecond, topThird, topFourth };

            OrderCoordsWithoutIntersecting();
        }
        public Quadrangle() : this(new Coord(), new Coord(), new Coord(), new Coord()) { }

        private void OrderCoordsWithoutIntersecting()
        {
            for (int i = 0; i < Coords.Length; i++)
            {
                if (SLine.IsLinesIntersect(
                        new SLine(this[i], this[i + 1]),
                        new SLine(this[i + 2], this[i + 3])))
                {
                    // Swapping the coordinates
                    var temp = this[i + 1];
                    this[i + 1] = this[i + 2];
                    this[i + 2] = temp;
                }

            }
        }

        public static Quadrangle Parse(string[] inputs)
        {
            return
                new Quadrangle(
                    Coord.Parse(inputs[0],inputs[1]),
                    Coord.Parse(inputs[2],inputs[3]),
                    Coord.Parse(inputs[4],inputs[5]),
                    Coord.Parse(inputs[6],inputs[7]));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Quadrangle");
            sb.Append(base.ToString());

            return sb.ToString();
        }
    }
}
