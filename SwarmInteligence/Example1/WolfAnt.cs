using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example1
{
	internal class WolfAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		private const int Speed = 3;

		public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world)
			: base(world)
		{
		}

		public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			Coordinates2D[] cellsWithPreys = outlook.Cell
				.GetSuburbCells(Speed)
				.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty())
				.Select(cell => cell.Coordinate)
				.ToArray();

			if(cellsWithPreys.IsNotEmpty())
				this.MoveTo(cellsWithPreys.First());
			else {
				Coordinates2D[] allCells = outlook.Cell
					.GetSuburb(Speed)
					.ToArray();

				Coordinates2D cellToGo = allCells[new Random().Next(allCells.Length)];
				this.MoveTo(cellToGo);
			}
		}
	}
}