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
        string shapesInfo = string.Empty;
        string dataFileName = "Shapes.txt";
        string saveFileName = "ShapesInfo.txt";

        public MainWindow()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Returns shape from list by specific function
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

        private void BackToMainMenu()
        {
            menu.Visibility = Visibility.Visible;

            Height = 350;
            Width = 250;

            ResizeMode = ResizeMode.NoResize;
        }

        #region MainMenuEvents

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            mainProgram.Visibility = Visibility.Visible;

            ResizeMode = ResizeMode.CanResize;

            Height = 350;
            Width = 750;

            // Read data from file
            var shapes = DataReader.GetData(dataFileName);

            // Creating graphic variable in order to draw shapes
            canvas.Children.Clear();
            var graphic = new Graphic(canvas);

            // Drawing all triangles
            graphic.Draw(shapes);
            // Drawing all quadrangles
            graphic.Draw(shapes);

            // Getting max quadrangley by area
            var maxQuadrangleyByArea = GetByArea(shapes.OfType<Quadrangle>().ToList<Shape>(), (a, b) => a > b);

            // Getting a list of shapes with area smaller than some number
            var trianglesWithSmallArea = shapes.OfType<Triangle>().ToList().Where(t => t.Area < maxQuadrangleyByArea.Area / 2).ToList();

            // Getting min quadrangley by area
            var minQuadrangleyByArea = GetByArea(shapes.OfType<Quadrangle>().ToList<Shape>(), (a, b) => a < b);

            graphic.DrawFull(trianglesWithSmallArea.OfType<Triangle>().ToList());
            graphic.DrawFull(minQuadrangleyByArea as Quadrangle);


            // Combine all info about shapes into one string
            var sb = new StringBuilder();

            shapes.ForEach(t => sb.AppendLine(t.ToString()));

            shapesInfo = sb.ToString();            
        }
        private void ButtonTextEditor_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            textEditor.Visibility = Visibility.Visible;

            ResizeMode = ResizeMode.CanResize;

            Height = 550;
            Width = 450;

            richTextEditor.Document.Blocks.Clear();

            using (var sr = new StreamReader(dataFileName))
                richTextEditor.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));

        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region MainProgramEvents

        private void ButtonMainProgramBack_Click(object sender, RoutedEventArgs e)
        {
            mainProgram.Visibility = Visibility.Hidden;         
            BackToMainMenu();
        }        

        private void ButtonShapesInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(shapesInfo);
        }

        private void ButtonShapesSave_Click(object sender, RoutedEventArgs e)
        {
            // Write shapes info into a file
            using (var sw = new StreamWriter(saveFileName))
                sw.Write(shapesInfo);
        }


        #endregion

        #region TextEditorEvents

        private void ButtonTextEditorBack_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Visibility = Visibility.Hidden;
            BackToMainMenu();
        }
        private void ButtonTextEditorSave_Click(object sender, RoutedEventArgs e)
        {
            // Write shapes info into a file
            using (var sw = new StreamWriter(dataFileName))
                sw.Write(
                    new TextRange(
                        richTextEditor.Document.ContentStart,
                        richTextEditor.Document.ContentEnd)
                            .Text);
        }


        #endregion

        #region SourceCode
        private void ButtonSourceCode_Click(object sender, RoutedEventArgs e)
        {

        } 
        #endregion
    }
}
