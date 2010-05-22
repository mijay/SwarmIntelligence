using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Utils.Drawing;

namespace SwarmInteligence
{
    //todo: probably it is better to split this class into two and move some functions to abstract base
    /// <summary>
    /// Abstract class for representing objects of the <see cref="Map{C,B}"/>
    /// that are stored in <see cref="Cell{C,B}"/>
    /// </summary>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="B"></typeparam>
    public abstract class Stone<C, B>: ILocatable<C, B>, ICommunicative, IVisualizable
        where C: struct, ICoordinate<C>
    {
        protected Stone(District<C, B> district)
        {
            Contract.Invariant(!initialized || (Cell.Coordinate.Equals(coordinate) && Cell.Contains(this)));
            Contract.Requires<ArgumentNullException>(district != null);
            this.district = district;
        }

        /// <summary>
        /// Move this object to other position on the <see cref="Map{C,B}"/>
        /// but only inside the current <see cref="District{C,B}"/>.
        /// </summary>
        /// <remarks>
        /// This method is internal and should not be used outside of the class. And should not be made protected.
        /// Protected method should use it.
        /// </remarks>
        /// <param name="newCoord"> Global coordinates of new position. </param>
        internal void MoveTo(C newCoord)
        {
            //Contract.Requires<IndexOutOfRangeException>(district.Contains(newCoord));

            if(initialized)
                Cell.Remove(this);
            initialized = true;

            coordinate = newCoord;
            Cell = new Cell<C, B>(district, coordinate, command, true);
            Cell.Add(this);
        }

        /// <summary>
        /// Gets the <see cref="Cell{C,B}"/> in which the object is stored.
        /// </summary>
        public Cell<C, B> Cell { get; private set; }

        private bool initialized;

        //todo:?
        protected Command command;

        #region Implementation of ILocatable<C,B>

        private readonly District<C, B> district;

        /// <inheritdoc/>
        public District<C, B> District
        {
            get { return district; }
        }

        private C coordinate;

        /// <inheritdoc/>
        public C Coordinate
        {
            get { return coordinate; }
        }

        #endregion

        #region ICommunicative and other work with messages

        private readonly IList<IMessage> messages = new List<IMessage>();

        /// <summary>
        /// Gets the list of received <see cref="IMessage"/>s.
        /// </summary>
        protected IEnumerable<IMessage> Messages
        {
            get
            {
                Contract.Requires<InvalidOperationException>(district.Stage == TurnStage.AfterTurn,
                                                             "Stone.Messages can be called only in AfterTurn stage");
                return messages;
            }
        }

        /// <inheritdoc/>
        public void SendMessage(IMessage message)
        {
            Contract.Requires<InvalidOperationException>(district.Stage == TurnStage.Turn,
                                                         "Stone.SendMessage can be called only in Turn stage");
            Contract.Requires<ArgumentNullException>(message != null);
            messages.Add(message);
        }

        #endregion

        #region Implementation of IVisualizable

        /// <inheritdoc/>
        public abstract void Draw(FastBitmap bitmap);

        #endregion
    }
}