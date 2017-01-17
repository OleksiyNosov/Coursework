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

/*
 * У першому файлі задано координати вершин прямокутників.
 * У другому файлі задано координати вершин трикутників.
 *  
 * Вивести на екран трикутники та прямокутники. 
 * 
 * Окремим кольором виділити ті трикутники, що мають площу меншу за половину площі найбільшого прямокутника,
 * провести в них медіану, бісектрису і висоту також виділити прямокутник з найменшою площею і провести в ньому діагоналі.
 * 
 * Вивести повідомлення про характеристики виділених фігур і записати їх у файл.
 */

namespace GraphicModeling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var triangles = DataReader.GetData("triangles.txt");
            var quadrangles = DataReader.GetData("quadrangles.txt");

            var graphic = new Graphic(canvas);

            graphic.Draw(triangles);
            graphic.Draw(quadrangles);

            var maxQuadrangleyByArea = GetByArea(quadrangles, (a, b) => a > b);

            var trianglesWithSmallArea = triangles.Where(t => t.Area < maxQuadrangleyByArea.Area / 2).ToList();

            var minQuadrangleyByArea = GetByArea(quadrangles, (a, b) => a < b);

            graphic.DrawFull(trianglesWithSmallArea.OfType<Triangle>().ToList());

        }

        private Shape GetByArea(List<Shape> shapes, Func<double, double, bool> compare)
        {
            var maxShapeByArea = shapes.First();
            var maxShapeArea = maxShapeByArea.Area;

            foreach (var s in shapes)
            {
                var newShapeArea = s.Area;
                if (compare(newShapeArea, maxShapeArea))
                {
                    maxShapeArea = newShapeArea;
                    maxShapeByArea = s;
                }
            }

            return maxShapeByArea;
        }

    }


}
