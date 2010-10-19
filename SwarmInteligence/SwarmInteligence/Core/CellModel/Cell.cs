//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using System.Linq;

//namespace SwarmInteligence
//{
//    /// <summary>
//    /// Structure that represent the cell of the <see cref="Map{C,B}"/>.
//    /// It is atomic addressable point on it.
//    /// </summary>
//    public struct Cell<C, B>: ILocatable<C, B>, IEnumerable<Stone<C, B>>
//        where C: struct, ICoordinate<C>
//    {
//        internal Cell(District<C, B> district, C coordinate, bool initialize)
//        {
//            Contract.Requires<ArgumentNullException>(district != null);
//            Contract.Requires<IndexOutOfRangeException>(coordinate.IsInRange(district.Bounds.Item1, district.Bounds.Item2));

//            this.district = district;
//            this.coordinate = coordinate;
//            if(initialize) {
//                backgroundInit = true;
//                background = district.Background[coordinate];
//                objectsList = district.GetData(coordinate);
//            } else {
//                backgroundInit = false;
//                background = default(B);
//                objectsList = null;
//            }
//        }

//        [ContractInvariantMethodAttribute]
//        private void CellContract()
//        {
//            Contract.Invariant(district != null);
//        }

//        #region Work with Background

//        private B background;
//        private bool backgroundInit;

//        /// <summary>
//        /// Gets the background data accessor of the current <see cref="Cell{C,B}"/>.
//        /// </summary>
//        [Pure]
//        public B Background
//        {
//            get
//            {
//                if(!backgroundInit) {
//                    backgroundInit = true;
//                    // конфликт возникнуть не может, т.к. это структура
//                    background = district.Background[coordinate];
//                }
//                return background;
//            }
//        }

//        #endregion

//        #region Work with list of stored objects

//        private IList<Stone<C, B>> objectsList;

//        private IList<Stone<C, B>> ObjectsList
//        {
//            get
//            {
//                Contract.Ensures(Contract.Result<IList<Stone<C, B>>>() != null);
//                return objectsList ?? (objectsList = district.GetData(coordinate));
//            }
//        }

//        /// <summary>
//        /// Gets number of objects stored in the current <see cref="Cell{C,B}"/>.
//        /// </summary>
//        [Pure]
//        public int Count
//        {
//            get { return ObjectsList.Count; }
//        }

//        /// <summary>
//        /// Gets the first stored object from the <see cref="Cell{C,B}"/>.
//        /// </summary>
//        [Pure]
//        public Stone<C, B> First
//        {
//            get { return ObjectsList[0]; }
//        }

//        /// <inheritdoc/>
//        [Pure]
//        public IEnumerator<Stone<C, B>> GetEnumerator()
//        {
//            return ObjectsList.GetEnumerator();
//        }

//        /// <inheritdoc/>
//        [Pure]
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        /// <summary>
//        /// Add new object to the <see cref="Cell{C,B}"/>.
//        /// </summary>
//        /// <remarks>Uses delayed evaluation.</remarks>
//        public void Add(Stone<C, B> stone)
//        {
//            Contract.Requires<ArgumentNullException>(stone != null);
//            Contract.Requires<InvalidOperationException>(!stone.Cell.Coordinate.Equals(stone.Coordinate),
//                                                         "cannot add stone which was already used");
//            stone.MoveTo(coordinate, true);
//        }

//        /// <summary>
//        /// Add new object to the <see cref="Cell{C,B}"/> immediately.
//        /// </summary>
//        internal void AddNow(Stone<C, B> stone)
//        {
//            Contract.Requires<ArgumentNullException>(stone != null);
//            Contract.Requires<InvalidOperationException>(stone.Coordinate.Equals(Coordinate),
//                                                         "cannot add stone which is placed in other cell");
//            ObjectsList.Add(stone);
//        }

//        /// <summary>
//        /// Removes the <paramref name="stone"/> from the <see cref="Cell{C,B}"/> immediately.
//        /// </summary>
//        internal void RemoveNow(Stone<C, B> stone)
//        {
//            Contract.Requires<ArgumentNullException>(stone != null);
//            Contract.Requires(stone.Coordinate.Equals(Coordinate), "cannot remove stone which is stored in other cell");
//            Contract.Requires(this.Contains(stone), "stone coordinate is correct but there is no such stone in the list");
//            ObjectsList.Remove(stone);
//        }

//        #endregion

//        #region Implementation of ILocatable<C,B>

//        private readonly C coordinate;
//        private readonly District<C, B> district;

//        /// <inheritdoc/>
//        public District<C, B> District
//        {
//            get { return district; }
//        }

//        /// <inheritdoc/>
//        public C Coordinate
//        {
//            get { return coordinate; }
//        }

//        #endregion
//    }
//}