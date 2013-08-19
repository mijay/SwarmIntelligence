using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Common.Collections;
using Common.Collections.Extensions;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Implementation.Logging;

namespace WolfsAndPreysWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow: Window
	{
		private static readonly MultiMap<Tuple<Coordinates2D, bool>, Ellipse> ellipses
			= new MultiMap<Tuple<Coordinates2D, bool>, Ellipse>();

		private static double stepX;
		private static double stepY;

		private readonly Model model;

		public MainWindow()
		{
			InitializeComponent();
			DrawGrid();

			model = new Model(new Coordinates2D(-10, -10), new Coordinates2D(10, 10), 2, 9);

			SynchronizationContext uiContext = SynchronizationContext.Current;
			TaskScheduler uiContextScheduler = TaskScheduler.FromCurrentSynchronizationContext();
			model.OnNewRecords += records => uiContext.Send(_ => ProcessLog(records), new object());

			nextStep.Click += (o, a) => {
			                  	nextStep.IsEnabled = false;
			                  	Task.Factory
			                  		.StartNew(model.Turn)
			                  		.ContinueWith(_ => { nextStep.IsEnabled = true; }, uiContextScheduler);
			                  };

			Task.Factory
				.StartNew(model.Initialize)
				.ContinueWith(_ => { nextStep.IsEnabled = true; }, uiContextScheduler);
		}

		private void ProcessLog(IEnumerable<LogRecord> newRecords)
		{
			foreach(LogRecord newRecord in newRecords) {
				textBox1.Text += string.Format("{0}: {1}\n",
				                               newRecord.type, newRecord.arguments
				                                               	.Select(x => x.ToString())
				                                               	.JoinStrings(", "));

				if(newRecord.type == CommonLogTypes.AntMoved) {
					var from = (Coordinates2D) newRecord.arguments[1];
					var to = (Coordinates2D) newRecord.arguments[2];
					bool isWolf = newRecord.arguments[0] is WolfAnt;

					Ellipse ellipse;
					var removed = ellipses.RemoveFirst(Tuple.Create(from, isWolf), out ellipse);
					Contract.Assert(removed);
					gridVisual.Children.Remove(ellipse);

					AddEllipse(isWolf, to);
				} else if(newRecord.type == CommonLogTypes.AntAdded) {
					var to = (Coordinates2D) newRecord.arguments[1];
					bool isWolf = newRecord.arguments[0] is WolfAnt;

					AddEllipse(isWolf, to);
				} else if(newRecord.type == CommonLogTypes.AntRemoved) {
					var from = (Coordinates2D) newRecord.arguments[1];
					bool isWolf = newRecord.arguments[0] is WolfAnt;

					Ellipse ellipse;
					var removed = ellipses.RemoveFirst(Tuple.Create(from, isWolf), out ellipse);
					Contract.Assert(removed);
					gridVisual.Children.Remove(ellipse);

					gridVisual.Children.Add(BuildCycle(isWolf, from, isWolf ? Brushes.BlueViolet : Brushes.Gray));
				}
			}

			textBox1.ScrollToEnd();
		}

		private void AddEllipse(bool isWolf, Coordinates2D point)
		{
			Ellipse circle = BuildCycle(isWolf, point, isWolf ? Brushes.DarkRed : Brushes.DarkGreen);
			gridVisual.Children.Add(circle);
			ellipses.Add(Tuple.Create(point, isWolf), circle);
		}

		private static Ellipse BuildCycle(bool isWolf, Coordinates2D point, SolidColorBrush solidColorBrush)
		{
			int modifier = isWolf ? 1 : -1;
			return new Ellipse
			       {
			       	Margin = new Thickness
			       	         {
			       	         	Top = 2 * point.y * stepY + 0.25 * stepY * modifier,
			       	         	Left = 2 * point.x * stepX + 0.25 * stepX * modifier
			       	         },
			       	Stroke = solidColorBrush,
			       	Width = stepX / 2,
			       	Height = stepY / 2,
			       	StrokeThickness = 2
			       };
		}

		private void DrawGrid()
		{
			var min1 = new Coordinates2D(-10, -10);
			var max1 = new Coordinates2D(10, 10);
			stepX = (gridVisual.Width - 2) / (max1.x - min1.x + 1);
			stepY = (gridVisual.Height - 2) / (max1.y - min1.y + 1);

			//draw grid

			for(int i = 0; i <= max1.x - min1.x + 1; ++i) {
				var line = new Line();
				line.X1 = line.X2 = i * stepX + 1;
				line.Y1 = 0;
				line.Y2 = (max1.y - min1.y + 1) * stepY;
				line.Stroke = Brushes.Black;
				line.StrokeThickness = 2;

				gridVisual.Children.Add(line);
			}

			for(int i = 0; i <= max1.y - min1.y + 1; ++i) {
				var line = new Line();
				line.Y1 = line.Y2 = i * stepY + 1;
				line.X1 = 0;
				line.X2 = (max1.x - min1.x + 1) * stepX;
				line.Stroke = Brushes.Black;
				line.StrokeThickness = 2;

				gridVisual.Children.Add(line);
			}
		}
	}
}