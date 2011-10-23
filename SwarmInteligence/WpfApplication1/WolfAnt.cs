using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace WpfApplication1
{
	internal class WolfAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		private static readonly Random random = new Random();
		private const int Speed = 3;

		public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world)
			: base(world)
		{
		}

		public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			ICell<Coordinates2D, EmptyData, EmptyData>[] cellsWithPreys = outlook.Cell
				.GetSuburbCells(Speed)
				.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty())
				.ToArray();

			if(cellsWithPreys.IsNotEmpty()) {
				ICell<Coordinates2D, EmptyData, EmptyData> target = cellsWithPreys.First();
				this.MoveTo(target.Coordinate);
				this.RemoveFrom(target.Coordinate, target.OfType<PreyAnt>().First());
			} else {
				Coordinates2D[] allCells = outlook.Cell
					.GetSuburb(Speed)
					.ToArray();

				Coordinates2D cellToGo = allCells[random.Next(allCells.Length)];
				this.MoveTo(cellToGo);
			}
		}
	}
}