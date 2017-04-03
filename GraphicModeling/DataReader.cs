using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;

namespace GraphicModeling
{
    public class DataReader
    {
        public static List<Shape> GetData(string filePath)
        {
            var shapes = new List<Shape>();

            if ( ! File.Exists(filePath))
                return shapes;

            using (var sr = new StreamReader(filePath))
            {
                while ( ! sr.EndOfStream)
                {
                    var inputs = sr.ReadLine().Split();

                    if (inputs.Length == 6)
                        shapes.Add(Triangle.Parse(inputs));
                    else if (inputs.Length == 8)
                        shapes.Add(Quadrangle.Parse(inputs));
                }

                return shapes;
            }
        }
    }
}