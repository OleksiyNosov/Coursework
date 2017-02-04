using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicModeling
{
    public static class ShapeControlFactory
    {
        public static List<ShapeControl> Create(List<Shape> shapes)
        {
            var shapeCtrls = new List<ShapeControl>(shapes.Count);

            foreach (var s in shapes)
            {
                if (s is Triangle)
                    shapeCtrls.Add(new TriangleControl(s as Triangle));
                else if (s is Quadrangle)
                    shapeCtrls.Add(new QuadrangleControl(s as Quadrangle));
            }

            return shapeCtrls;
        }
    }
}
