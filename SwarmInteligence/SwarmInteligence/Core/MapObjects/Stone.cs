using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Utils.Drawing;

namespace SwarmInteligence
{
    /// <summary>
    /// Abstract class for representing objects of the <see cref="Map{C,B}"/>
    /// that are stored in <see cref="Cell{C,B}"/>
    /// </summary>
    public abstract class Stone<C, B>: ILocatable<C, B>, ICommunicative, IVisualizable
        where C: struct, ICoordinate<C>
    {
        protected Stone(District<C, B> district)
        {
            Contract.Requires<ArgumentNullException>(district != null);
            this.district = district;
        }

        [ContractInvariantMethod]
        private void StoneInvariant()
        {
            Contract.Invariant(!initialized || (Cell.Coordinate.Equals(coordinate) && Cell.Contains(this)));
        }

        #region Work with Cell

        /// <summary>
        /// Move this object to other position on the <see cref="Map{C,B}"/>
        /// but only inside the current <see cref="District{C,B}"/>.
        /// </summary>
        /// <remarks>
        /// This method should not be made protected. Protected method should use it.
        /// </remarks>
        /// <param name="newCoord"> Global coordinates of new position. </param>
        internal void MoveTo(C newCoord)
        {
            Contract.Requires<IndexOutOfRangeException>(newCoord.IsInRange(district.Bounds.Item1, district.Bounds.Item2));

            if(initialized)
                Cell.Remove(this);
            initialized = true;

            coordinate = newCoord;
            cell = new Cell<C, B>(district, coordinate, command, true);
            cell.Add(this);
        }

        private Cell<C, B> cell;

        /// <summary>
        /// Gets the <see cref="Cell{C,B}"/> in which the object is stored.
        /// </summary>
        [Pure]
        public Cell<C, B> Cell
        {
            get
            {
                if(!initialized)
                    throw new InvalidOperationException("Stone hasn't been initialized");
                return cell;
            }
        }

        #endregion

        private bool initialized;

        /// <summary>
        /// Checks if the <see cref="Stone{C,B}"/> has been already placed in some location.
        /// </summary>
        [Pure]
        internal bool IsInitialized
        {
            get { return initialized; }
        }

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
            get
            {
                if (!initialized)
                    throw new InvalidOperationException("Stone hasn't been initialized");
                return coordinate;
            }
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