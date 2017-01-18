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
        }
        public Quadrangle() : this(new Coord(), new Coord(), new Coord(), new Coord()) { }

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
