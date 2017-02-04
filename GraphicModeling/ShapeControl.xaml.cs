using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicModeling
{
    /// <summary>
    /// Interaction logic for ShapeControl.xaml
    /// </summary>
    public partial class ShapeControl : UserControl
    {
        private Polyline polyline;
        private Shape shape;

        public Shape Shape
        {
            get { return shape; }
        }
        public Brush Fill
        {
            get { return polyline.Fill; }
            set { polyline.Fill = value; }
        }
        public Brush Stroke
        {
            get { return polyline.Stroke; }
            set { polyline.Stroke = value; }
        }
        public double StrokeThickness
        {
            get { return polyline.StrokeThickness; }
            set { polyline.StrokeThickness = value; }
        }


        protected Line CreateLine(Coord start, Coord end)
        {
            return new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Transparent
            };
        }
        protected void AddToCanvas(List<Line> lines)
        {
            lines.ForEach(l => canvas.Children.Add(l));
        }
        protected void SetColor(List<Line> lines, Brush color)
        {
            lines.ForEach(l => l.Stroke = color);
        }

        public ShapeControl(Shape shape)
        {
            this.shape = shape;

            InitializeComponent();
            InitializePolyline();
        }

        private void InitializePolyline()
        {
            polyline = new Polyline();

            polyline.Stroke = Brushes.Black;
            polyline.StrokeThickness = 2;

            foreach (var c in shape.Coords)
                polyline.Points.Add(c.AsPoint());

            polyline.Points.Add(shape.Coords.First().AsPoint());

            canvas.Children.Add(polyline);
        }

        public void Shift(Coord coord)
        {
            shape.Shift(coord);

            for (int i = 0; i < polyline.Points.Count; i++)
                polyline.Points[i] = (polyline.Points[i] + coord).AsPoint();
        }
    }
}
