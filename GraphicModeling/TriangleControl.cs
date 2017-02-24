using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicModeling
{
    class TriangleControl : ShapeControl
    {
        private List<Line> medians;
        private List<Line> bisects;
        private List<Line> heights;

        private Brush mediansColor;
        private Brush bisectsColor;
        private Brush heightsColor;

        public Brush MediansColor
        {
            get { return mediansColor; }
            set
            {
                mediansColor = value;
                SetColor(medians, mediansColor);
            }
        }
        public Brush BisectsColor
        {
            get { return bisectsColor; }
            set
            {
                bisectsColor = value;
                SetColor(bisects, bisectsColor);
            }
        }
        public Brush HeightsColor
        {
            get { return heightsColor; }
            set
            {
                heightsColor = value;
                SetColor(heights, heightsColor);
            }
        }


        public TriangleControl(Triangle triangle) : base(triangle)
        {
            Initialize();

            Create(MedianToList);
            Create(BisectToList);
            Create(HeightToList);

            AddToCanvas(medians);
            AddToCanvas(bisects);
            AddToCanvas(heights);
        }

        private void Initialize()
        {
            medians = new List<Line>(Shape.Coords.Length);
            bisects = new List<Line>(Shape.Coords.Length);
            heights = new List<Line>(Shape.Coords.Length);
        }

        private void Create(Action<int> line)
        {
            for (int i = 0; i < Shape.Coords.Length; i++)
                line(i);
        }

        private void MedianToList(int i)
        {
            medians.Add(
                CreateLine(
                    Shape[i],
                    Shape[i + 1].Between(Shape[i + 2])));
        }
        private void BisectToList(int i)
        {
            bisects.Add(
                CreateLine(
                    Shape[i],
                    Shape[i + 1].BetweenWithRatio(
                        Shape[i + 2],
                        Shape[i].DistanceTo(Shape[i + 1]),
                        Shape[i + 2].DistanceTo(Shape[i]))));
        }
        private void HeightToList(int i)
        {
            // k = ((y2-y1) * (x3-x1) - (x2-x1) * (y3-y1)) / ((y2-y1)^2 + (x2-x1)^2)
            var k = ((Shape[i + 2].Y - Shape[i + 1].Y) 
                    *  (Shape[i + 0].X - Shape[i + 1].X)
                    -  (Shape[i + 2].X - Shape[i + 1].X)
                    *  (Shape[i + 0].Y - Shape[i + 1].Y))
                    / (Math.Pow(Shape[i + 2].Y - Shape[i + 1].Y, 2)
                    +  Math.Pow(Shape[i + 2].X - Shape[i + 1].X, 2));

            var secondCoord = new Coord(
                Shape[i].X - k * (Shape[i + 2].Y - Shape[i + 1].Y),
                Shape[i].Y + k * (Shape[i + 2].X - Shape[i + 1].X));

            heights.Add(
                CreateLine(Shape[i], secondCoord));


        }
    }
}
