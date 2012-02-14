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
	internal class PreyAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		private const int Speed = 2;

		public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world)
			: base(world)
		{
		}

		public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			Coordinates2D[] cellsWithWolfs = outlook.Cell
				.GetSuburbCells(Speed)
				.Where(cell => cell.OfType<WolfAnt>().IsNotEmpty())
				.Select(cell => cell.Coordinate)
				.ToArray();

			Coordinates2D[] cellsToGoInto = outlook.Cell
				.GetSuburb(Speed)
				.Except(cellsWithWolfs)
				.ToArray();

			Coordinates2D cellToGo = cellsToGoInto[new Random().Next(cellsToGoInto.Length)];
			this.MoveTo(cellToGo);
		}
	}
}