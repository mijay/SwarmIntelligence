using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Abstract class for representing animal-like object on the <see cref="Map{C,B}"/>.
    /// Such objects has no restrictions.
    /// </summary>
    public abstract class Animal<C, B>: Plant<C, B>
        where C: struct, ICoordinate<C>
    {
        private readonly Zone<C, B> zone;
        private int radius;

        protected Animal(District<C, B> district, int radius): base(district)
        {
            zone = new Zone<C, B>(district);
            Radius = radius;
        }

        /// <summary>
        /// Gets and sets the radius of the <see cref="Zone"/> which this <see cref="Animal{C,B}"/> overviews.
        /// </summary>
        public int Radius
        {
            [Pure]
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return radius;
            }
            protected set
            {
                Contract.Requires<ArgumentException>(value >= 0);
                radius = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Zone{C,B}"/> which this <see cref="Animal{C,B}"/> overviews.
        /// </summary>
        public Zone<C, B> Zone
        {
            [Pure]
            get
            {
                Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn,
                                                             "Animal.Zone can be used only in Turn stage.");
                Contract.Ensures(Contract.Result<Zone<C, B>>() != null);
                return zone;
            }
        }

        /// <summary>
        /// This method is invoked on the <see cref="TurnStage.Turn"/> stage.
        /// </summary>
        protected abstract void OnTurn();

        //todo: напиши рабочий код для обработки всяких разных сервистных событий
    }
}