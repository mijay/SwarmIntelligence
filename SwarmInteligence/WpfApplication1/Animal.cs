using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Specialized;

namespace WpfApplication1
{
    internal abstract class Animal : AntBase<Coordinates2D, EmptyData, EmptyData>
    {
        public double weight { get; set; }
        protected int reproductionRadius { get; set; }
        private readonly Random random = new Random();

        protected Animal(World<Coordinates2D, EmptyData, EmptyData> world, double weight) : base(world)
        {
            this.weight = weight;
            reproductionRadius = 1;
        }

        public abstract override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook);

        protected Coordinates2D[] GetEmptySuborbCells(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius)
        {
            Coordinates2D[] cellsWithWolfs = outlook.Cell
                .GetSuburbCells(radius)
                .Where(cell => cell.IsNotEmpty())
                .Select(cell => cell.Coordinate)
                .ToArray();

            Coordinates2D[] cellsToGoInto = outlook.Cell
                .GetSuburb(radius)
                .Except(cellsWithWolfs)
                .ToArray();

            return cellsToGoInto;
        }

        protected void Reproducing(World<Coordinates2D, EmptyData, EmptyData> world, double weight, bool isWolf, Coordinates2D startPos)
        {
            
            using(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier = world.GetModifier())
                mapModifier.AddAt(isWolf
                                            ? (IAnt<Coordinates2D, EmptyData, EmptyData>)new WolfAnt(world, weight / 2)
                                            : new PreyAnt(world, weight / 2),
                                          startPos);
        }
    }
}
