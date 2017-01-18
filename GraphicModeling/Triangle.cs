using System;
using System.Text;

namespace GraphicModeling
{
    public class Triangle : Shape
    {

        public Triangle(Coord topFirst, Coord topSecond, Coord topThird)
        {
            Coords = new Coord[]{ topFirst, topSecond, topThird };
        }
        public Triangle() : this(new Coord(), new Coord(), new Coord()) { }

        public static Triangle Parse(string[] inputs)
        {
            return
                new Triangle(
                    Coord.Parse(inputs[0],inputs[1]),
                    Coord.Parse(inputs[2],inputs[3]),
                    Coord.Parse(inputs[4],inputs[5]));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Triangle");
            sb.Append(base.ToString());

            return sb.ToString();
        }
    }
}