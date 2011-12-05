using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwarmIntelligence.Core.Space;

namespace Example2
{
    public struct SimpleCoordinates: ICoordinate<SimpleCoordinates>
    {
        public readonly int vertex;

        public SimpleCoordinates(int vertex)
        {
            this.vertex = vertex;
        }

        public bool Equals(SimpleCoordinates other)
        {
            return this.vertex == other.vertex;
        }

        public SimpleCoordinates Clone()
        {
            return new SimpleCoordinates(vertex);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (vertex * 397);
            }
        }
    }
}
