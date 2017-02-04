using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicModeling
{
    class QuadrangleControl : ShapeControl
    {
        private List<Line> diagonals;

        private Brush diagonalsColor;
        public Brush DiagonalsColor
        {
            get { return diagonalsColor; }
            set
            {
                diagonalsColor = value;
                SetColor(diagonals, diagonalsColor);
            }
        }


        public QuadrangleControl(Quadrangle quadrangle) : base(quadrangle)
        {
            CreateDiagonals();

            AddToCanvas(diagonals);
        }

        private void CreateDiagonals()
        {
            diagonals = new List<Line>(2);

            CreateDiagonal(0);
            CreateDiagonal(1);
        }

        private void CreateDiagonal(int i)
        {
            diagonals.Add(
                CreateLine(
                    Shape[i],
                    Shape[i + 2]));
        }
    }
}
