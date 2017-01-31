﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicModeling
{
    public abstract class Shape
    {
        public double Perimeter
        {
            get
            {
                var perimeter = 0d;

                for (int i = 0; i < Coords.Length; i++)
                    perimeter += Coords[i].DistanceTo(Coords[(i + 1) % Coords.Length]);

                return perimeter;
            }
        }

        public double Area
        {
            get
            {
                var area = 0d;

                for (int i = 0; i < Coords.Length; i++)
                    area += Coords[i].X * Coords[(i + 1) % Coords.Length].Y
                          - Coords[i].Y * Coords[(i + 1) % Coords.Length].X;

                return Math.Abs(area / 2);
            }
        }

        public Coord[] Coords;

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
