using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Shapes;
using Common.Collections.Extensions;
using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Specialized;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static World<Coordinates2D, EmptyData, EmptyData> _world;
        private static Runner<Coordinates2D, EmptyData, EmptyData> _runner;
        private static LogManager _logger = new LogManager();
        private static readonly Coordinates2D Min = new Coordinates2D(-10, -10);
        private static readonly Coordinates2D Max = new Coordinates2D(10, 10);
        private static List<Ellipse> ellipses = new List<Ellipse>();

        private static double stepX;
        private static double stepY;

        private static readonly Random Random = new Random();

        private SynchronizationContext uiContext;
        private Thread t;

        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();

            stepX = (gridVisual.Width - 2)/(Max.x - Min.x + 1);
            stepY = (gridVisual.Height - 2)/(Max.y - Min.y + 1);

            //draw grid

            for (var i = 0; i <= Max.x - Min.x + 1; ++i)
            {
                var line = new Line();
                line.X1 = line.X2 = i*stepX + 1;
                line.Y1 = 0;
                line.Y2 = (Max.y - Min.y + 1)*stepY;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.StrokeThickness = 2;

                gridVisual.Children.Add(line);
            }

            for (var i = 0; i <= Max.y - Min.y + 1; ++i)
            {
                var line = new Line();
                line.Y1 = line.Y2 = i*stepY + 1;
                line.X1 = 0;
                line.X2 = (Max.x - Min.x + 1)*stepX;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.StrokeThickness = 2;

                gridVisual.Children.Add(line);
            }

                uiContext = SynchronizationContext.Current;
            t = new Thread(Initialize);
            t.Start();
        }

        private static IAnt<Coordinates2D, EmptyData, EmptyData>[] SeedAnts(int count)
        {
            using (var mapModifier = _world.Map.GetModifier())
                return EnumerableExtension.Repeat(() => SeedAnt(mapModifier), count).ToArray();
        }

        private static Coordinates2D GenerateCoordinates()
        {
            var x = Random.Next(-10, 10);
            var y = Random.Next(-10, 10);
            return new Coordinates2D(x, y);
        }

        private static IAnt<Coordinates2D, EmptyData, EmptyData> SeedAnt(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier)
        {
            var initialCoordinates = GenerateCoordinates();
            var ant = Random.NextDouble() > 0.5 ? (IAnt<Coordinates2D, EmptyData, EmptyData>)new WolfAnt(_world) : new PreyAnt(_world);
            mapModifier.AddAt(ant, initialCoordinates);
            return ant;
        }

        private void Initialize()
        {
            var topology = new EightConnectedSurfaceTopology(Min, Max);
            var cellProvider = SetCell<Coordinates2D, EmptyData, EmptyData>.Provider();
            var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology, cellProvider, _logger.Log);
            var nodeDataLayer = new EmptyDataLayer<Coordinates2D>();
            var edgeDataLayer = new EmptyDataLayer<Edge<Coordinates2D>>();

            _world = new World<Coordinates2D, EmptyData, EmptyData>(nodeDataLayer, edgeDataLayer, map, _logger.Log);
            _runner = new Runner<Coordinates2D, EmptyData, EmptyData>(_world, new GarbageCollector<Coordinates2D, EmptyData, EmptyData>());

            var ants = SeedAnts(10);

        }

        private void closeWindow(object sender, EventArgs e)
        {
            t.Interrupt();
        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string text = textBox1.Text;
            var logs = new List<string>(text.Split('\n'));
            foreach (var ellipse in ellipses)
            {
                gridVisual.Children.Remove(ellipse);
            }

            var sublogs = logs.Skip(logs.Count - 10 - 1).Take(10);
            foreach (var log in sublogs)
            {
                Regex r = new Regex("x: (\\-?\\d+), y: (\\-?\\d+)\\)$");
                Match m = r.Match(log);
                var x = 0;
                var y = 0;
                if (m.Success)
                {
                    x = Convert.ToInt32(m.Groups[1].ToString());
                    y = Convert.ToInt32(m.Groups[2].ToString());
                }

                Ellipse circle = new Ellipse();
                if (log.IndexOf("WpfApplication1.WolfAnt") != -1)
                {
                    circle = new Ellipse { Margin = new Thickness { Top = 2 * y * stepY - 0.25 * stepY, Left = 2 * x * stepX - 0.25 * stepX } };
                    circle.Stroke = System.Windows.Media.Brushes.Red;
                } else if (log.IndexOf("WpfApplication1.PreyAnt") != -1)
                {
                    circle = new Ellipse { Margin = new Thickness { Top = 2 * y * stepY + 0.25 * stepY, Left = 2 * x * stepX + 0.25 * stepX} };
                    circle.Stroke = System.Windows.Media.Brushes.Green;
                }

                circle.Width = stepX / 2;
                circle.Height = stepY / 2;
                circle.StrokeThickness = 2;

                ellipses.Add(circle);

                gridVisual.Children.Add(circle);
            }
        }

        private void nextStep_Click(object sender, RoutedEventArgs e)
        {
            _runner.DoTurn();

            _logger.Journal.OnRecordsAdded +=
                (begin, end) =>
                    {
                        LogRecord[] newRecords = _logger.Journal
                            .Records
                            .ReadFrom(begin)
                            .Take((int) (end - begin + 1))
                            .Where(x => x.type == "AntMoved")
                            .ToArray();

                        foreach (var newRecord in newRecords)
                        {
                            uiContext.Send(
                                d =>
                                textBox1.Text +=
                                newRecord.type + ": " + newRecord.arguments[0] + "(" + newRecord.arguments[1] + ", " +
                                newRecord.arguments[2] + ")" + "\n", null);
                        }
                    };
        }
    }
}
