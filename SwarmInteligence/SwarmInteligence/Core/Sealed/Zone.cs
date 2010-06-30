using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public sealed class Zone<C, B>: IComponent<C, B>
        where C: struct, ICoordinate<C>
    {
        private int radius;
        private Animal<C, B> animal;

        public Zone(District<C, B> district, Animal<C, B> animal)
        {
            Contract.Requires<ArgumentNullException>(district != null && animal != null);
            Contract.Requires<ArgumentException>(district == animal.District);
            this.animal = animal;
            this.district = district;
        }

        //todo: напиши все остальное!

        /// <summary>
        /// Gets the radius of the current <see cref="Zone{C,B}"/>
        /// </summary>
        public int Radius
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return radius;
            }
            internal set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0);
                Contract.Requires<ArgumentException>(animal.Radius == value);
                radius = value;
            }
        }

        #region Implementation of IComponent<C,B>

        private readonly District<C, B> district;

        private C coordinate;

        /// <inheritdoc/>
        public District<C, B> District
        {
            get { return district; }
        }

        /// <inheritdoc/>
        public C Coordinate
        {
            get { return coordinate; }
            internal set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value.IsInRange(District.Bounds.Item1, District.Bounds.Item2));
                Contract.Requires<ArgumentException>(animal.Coordinate.Equals(value));
                coordinate = value;
            }
        }

        #endregion
    }
}