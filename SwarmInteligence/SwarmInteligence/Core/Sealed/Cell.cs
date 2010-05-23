﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace SwarmInteligence
{
    /// <summary>
    /// Structure that represent the cell of the <see cref="Map{C,B}"/>.
    /// It is atomic addressable point on it.
    /// </summary>
    public struct Cell<C, B>: ILocatable<C, B>, IEnumerable<Stone<C, B>>
        where C: struct, ICoordinate<C>
    {
        internal Cell(District<C, B> district, C coordinate, Command command, bool initialize)
        {
            this.command = command;
            this.district = district;
            this.command = command;
            this.coordinate = coordinate;
            if(initialize) {
                backgroundInit = true;
                background = district.Background[coordinate];
                objectsList = district.GetData(coordinate);
            }
            else {
                backgroundInit = false;
                background = default(B);
                objectsList = null;    
            }
        }

        #region Work with Background

        private B background;
        private bool backgroundInit;

        /// <summary>
        /// Gets the background data accessor of the current <see cref="Cell{C,B}"/>.
        /// </summary>
        [Pure]
        public B Background
        {
            get
            {
                if(!backgroundInit) {
                    backgroundInit = true;
                    // конфликт возникнуть не может, т.к. это структура
                    background = district.Background[coordinate];
                }
                return background;
            }
        }

        #endregion

        private readonly Command command;

        #region Work with list of stored objects

        private List<Stone<C, B>> objectsList;

        private List<Stone<C,B>> ObjectsList
        {
            get { return objectsList ?? (objectsList = district.GetData(coordinate)); }
        }

        /// <summary>
        /// Add new object to the <see cref="Cell{C,B}"/>.
        /// </summary>
        /// <remarks>Uses command and delayed evaluation.</remarks>
        public void Add(Stone<C, B> stone)
        {
            Contract.Requires<ArgumentNullException>(stone != null);
            Contract.Requires<InvalidOperationException>(!stone.IsInitialized, "cannot add stone which was already used");
            command.Add(new KeyValuePair<Action<Stone<C, B>>, Stone<C, B>>(ObjectsList.Add, stone));
        }

        /// <summary>
        /// Removes the <paramref name="stone"/> from the list of stored objects immediately.
        /// </summary>
        /// <param name="stone"></param>
        internal void Remove(Stone<C, B> stone)
        {
            Contract.Requires<ArgumentNullException>(stone != null);
            Contract.Requires(stone.Coordinate.Equals(coordinate), "cannot remove stone which is stored in other cell");
            Contract.Requires(ObjectsList.Contains(stone),"stone coordinate is correct but there is no such stone in the list");
            ObjectsList.Remove(stone);
        }

        /// <summary>
        /// Gets number of objects stored in the current <see cref="Cell{C,B}"/>.
        /// </summary>
        [Pure]
        public int Count
        {
            get { return ObjectsList.Count; }
        }

        /// <summary>
        /// Gets the first stored object from the <see cref="Cell{C,B}"/>.
        /// </summary>
        [Pure]
        public Stone<C, B> First
        {
            get { return ObjectsList[0]; }
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<Stone<C, B>> GetEnumerator()
        {
            return ObjectsList.GetEnumerator();
        }

        /// <inheritdoc/>
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ILocatable<C,B>

        private readonly District<C, B> district;

        /// <inheritdoc/>
        public District<C, B> District
        {
            get { return district; }
        }

        private readonly C coordinate;

        /// <inheritdoc/>
        public C Coordinate
        {
            get { return coordinate; }
        }

        #endregion
    }
}