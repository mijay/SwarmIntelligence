using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Implementation.Playground;

namespace WolfsAndPreys
{
	internal class WolfAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		private const int Speed = 3;

		public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world)
			: base(world)
		{
		}

		protected override void DoProcessTurn()
		{
			Coordinates2D[] cellsWithPreys = Cell
				.GetSuburbCells(Speed)
				.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty())
				.Select(cell => cell.Coordinate)
				.ToArray();

			if(cellsWithPreys.IsNotEmpty())
				this.MoveTo(cellsWithPreys.First());
			else {
				Coordinates2D[] allCells = Cell
					.GetSuburb(Speed)
					.ToArray();

				Coordinates2D cellToGo = allCells[new Random().Next(allCells.Length)];
				this.MoveTo(cellToGo);
			}
		}
	}
}