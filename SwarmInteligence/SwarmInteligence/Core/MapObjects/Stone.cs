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
    public abstract class Stone<C, B>: ICommunicative<C, B>, IVisualizable
        where C: struct, ICoordinate<C>
    {
        protected readonly Command command = new Command();
        private bool initialized;

        #region Implementation of ILocatable<C,B>

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
            get
            {
                if(!initialized)
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
                Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.AfterTurn,
                                                             "Stone.Messages can be called only in AfterTurn stage");
                return messages;
            }
        }

        /// <inheritdoc/>
        public void SendMessage(IMessage message)
        {
            messages.Add(message);
        }

        #endregion

        #region Implementation of IVisualizable

        /// <inheritdoc/>
        public abstract void Draw(FastBitmap bitmap);

        #endregion

        protected Stone(District<C, B> district)
        {
            Contract.Requires<ArgumentNullException>(district != null);
            this.district = district;
        }

        [ContractInvariantMethod]
        private void StoneInvariant()
        {
            Contract.Invariant(!initialized || (Cell.Coordinate.Equals(coordinate) && Cell.Contains(this)));
            Contract.Invariant(district != null && command != null && messages != null);
        }

        #region Work with Cell

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

        /// <summary>
        /// Move this object to other position on the <see cref="Map{C,B}"/>
        /// but only inside the current <see cref="District{C,B}"/>.
        /// </summary>
        /// <remarks>
        /// This method should not be made protected. Protected method should use it.
        /// </remarks>
        /// <param name="newCoord"> Global coordinates of new position. </param>
        /// <param name="delayed"> True if this action should be added to the <see cref="Command"/> and executed laiter. </param>
        internal void MoveTo(C newCoord, bool delayed)
        {
            Contract.Requires<IndexOutOfRangeException>(newCoord.IsInRange(District.Bounds.Item1, District.Bounds.Item2));

            if(delayed) {
                command.Add(MoveTo, newCoord, false);
                return;
            }

            if(initialized)
                Cell.RemoveNow(this);
            initialized = true;

            coordinate = newCoord;
            cell = new Cell<C, B>(district, coordinate, true);
            cell.AddNow(this);
        }

        #endregion
    }
}