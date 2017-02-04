using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicModeling
{
    public abstract class Shape
    {
        private bool isPerimeterUpdated = false;
        private bool isAreaUpdated = false;

        private Coord[] coords;
        private double perimeter;
        private double area;

        public Coord[] Coords
        {
            get { return coords; }
            set
            {
                coords = value;
                
                NotUpdated();
            }
        }              

        public Coord this[int index]
        {
            get { return Coords[index % Coords.Length]; }
            set
            {                
                Coords[index % Coords.Length] = value;

                NotUpdated();
            }
        }

        public double Perimeter
        {
            get
            {
                if ( ! isPerimeterUpdated)
                {
                    perimeter = CalcPerimeter();
                    isPerimeterUpdated = true;
                }

                return perimeter;
            }
        }

        public double Area
        {
            get
            {
                if ( ! isAreaUpdated)
                {
                    area = CalcArea();
                    isAreaUpdated = true;
                }

                return area;
            }
        }

        private void NotUpdated()
        {
            isAreaUpdated = isPerimeterUpdated = false;
        }

        private double CalcPerimeter()
        {
            var perimeter = 0d;

            for (int i = 0; i < Coords.Length; i++)
                perimeter += this[i].DistanceTo(this[i + 1]);

            return perimeter;
        }
        private double CalcArea()
        {
            var area = 0d;

            for (int i = 0; i < Coords.Length; i++)
                area += this[i].X * this[i + 1].Y
                      - this[i].Y * this[i + 1].X;

            return Math.Abs(area / 2);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Points: ");
            foreach (var c in Coords)
            {
                sb.Append(c.ToString());
                sb.Append(' ');
            }
            sb.Append("\n\r");

            sb.Append("Perimeter: ");
            sb.AppendLine(Perimeter.ToString());
            sb.Append("Area: ");
            sb.AppendLine(Area.ToString());

            return sb.ToString();
        }
    }
}
