using System;
using System.Collections.Generic;
using System.IO;
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
            Show();

            // Read data from file
            var triangles = DataReader.GetData("triangles.txt");
            var quadrangles = DataReader.GetData("quadrangles.txt");

            // Creating graphic variable in order to draw shapes
            var graphic = new Graphic(canvas);

            // Drawing all triangles
            graphic.Draw(triangles);
            // Drawing all quadrangles
            graphic.Draw(quadrangles);

            // Getting max quadrangley by area
            var maxQuadrangleyByArea = GetByArea(quadrangles, (a, b) => a > b);

            // Getting a list of shapes with area smaller than some number
            var trianglesWithSmallArea = triangles.Where(t => t.Area < maxQuadrangleyByArea.Area / 2).ToList();

            // Getting min quadrangley by area
            var minQuadrangleyByArea = GetByArea(quadrangles, (a, b) => a < b);

            graphic.DrawFull(trianglesWithSmallArea.OfType<Triangle>().ToList());
            graphic.DrawFull(minQuadrangleyByArea as Quadrangle);


            // Combine all info about shapes into one string
            var sb = new StringBuilder();

            triangles.ForEach(t => sb.AppendLine(t.ToString()));
            quadrangles.ForEach(q => sb.AppendLine(q.ToString()));

            var shapesInfo = sb.ToString();

            // Show shapes info
            MessageBox.Show(shapesInfo);

            // Write shapes info into a file
            using (var sw = new StreamWriter("ShapesInfo.txt"))
                sw.Write(shapesInfo);
        }

        /// <summary>
        /// Returns shpe from list by specific function
        /// </summary>
        /// <param name="shapes">List of given shapes</param>
        /// <param name="compare">Logic function that compare two areas</param>
        /// <returns>Return shape with mostly satisfied given logic function</returns>
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
