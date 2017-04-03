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

            SwitchToMainMenu();            
        }

        private void SwitchToMainMenu()
        {
            SwitchToWindow(menu, 350, 250, ResizeMode.NoResize);            
        }

        private void SwitchToWindow(Grid grid, int height, int width, ResizeMode resizeMode)
        {
            HideAllWindows();

            grid.Visibility = Visibility.Visible;
            Height = height;
            Width = width;
            ResizeMode = resizeMode;
        }

        private void HideAllWindows()
        {
            menu.Visibility = Visibility.Hidden;
            mainProgram.Visibility = Visibility.Hidden;
            textEditor.Visibility = Visibility.Hidden;
            aboutProgram.Visibility = Visibility.Hidden;
        }



        #region MainMenuEvents

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            SwitchToWindow(mainProgram, 450, 900, ResizeMode.CanResize);

            // Read data from file
            var shapeCtrls = ShapeControlFactory.Create(DataReader.GetData(dataFileName));

            if (shapeCtrls.Count <= 0)
                return;

            // Creating graphic variable in order to draw shapes
            canvas.Children.Clear();
            shapeCtrls.ForEach(s => canvas.Children.Add(s));

            // Getting max quadrangle by area
            var maxQuadrangleByArea =
                shapeCtrls.OfType<QuadrangleControl>().ToList()
                .OrderByDescending(s => s.Shape.Area).First();

            // Getting a list of shapes with area smaller than some number
            var trianglesWithSmallArea =
                shapeCtrls.OfType<TriangleControl>().ToList()
                .Where(t => t.Shape.Area < maxQuadrangleByArea.Shape.Area / 2).ToList();

            // Getting min quadrangle by area
            var minQuadrangleByArea =
                shapeCtrls.OfType<QuadrangleControl>().ToList()
                .OrderBy(s => s.Shape.Area).First();

            trianglesWithSmallArea.ForEach(t =>
            {
                t.Stroke = lineTrianglesSmall.Stroke;
                t.MediansColor = lineTrianglesSmallMedians.Stroke;
                t.BisectsColor = lineTrianglesSmallBisects.Stroke;
                t.HeightsColor = lineTrianglesSmallHeights.Stroke;
            });

            // Set Diagonals color in smallest quadrangle
            minQuadrangleByArea.Stroke = lineQuadranlesSmall.Stroke;
            minQuadrangleByArea.DiagonalsColor = lineQuadranlesSmallDiagonals.Stroke;

            // Combine all info about shapes into one string
            var sb = new StringBuilder();

            trianglesWithSmallArea.ForEach(s => sb.AppendLine(s.Shape.ToString()));
            sb.AppendLine(minQuadrangleByArea.Shape.ToString());

            shapesInfo = sb.ToString();
        }
        private void ButtonTextEditor_Click(object sender, RoutedEventArgs e)
        {
            SwitchToWindow(textEditor, 550, 450, ResizeMode.CanResize);

            richTextEditor.Document.Blocks.Clear();

            if (File.Exists(dataFileName))
                using (var sr = new StreamReader(dataFileName))
                    richTextEditor.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));

        }

        private void ButtonAboutProgram_Click(object sender, RoutedEventArgs e)
        {
            SwitchToWindow(aboutProgram, 350, 450, ResizeMode.CanResize);

            textBlockAboutProgram.Text =
                "Main tasks:\n\n" +
                "1 - Read triangles and quadrangles data from file.\n\n" +
                "2 - Select triangles with area smaller than hals of area of biggesst quadrangle.\n" +
                "    2.1 - Plot bisects.\n" +
                "    2.2 - Plot medians.\n" +
                "    2.3 - Plot altitudes.\n\n" +
                "3 - Select quadrangle with smallets area.\n" +
                "    3.1 - Plot diagonals.\n";
        }

        private void ButtonAboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Full Name: Oleksiy Nosov\nEmail: oleksiynosov@gmail.com\nAll rights reserved.®\nVinnitsia 2017", "Author");
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region MainProgramEvents

        private void ButtonBackToMain_Click(object sender, RoutedEventArgs e)
        {
            SwitchToMainMenu();
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


        
    }
}
