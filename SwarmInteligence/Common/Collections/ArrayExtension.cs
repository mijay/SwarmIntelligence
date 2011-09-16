using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Common.Collections
{
	/// <summary>
	/// Some extensions for arrays and especially byte arrays.
	/// </summary>
	public static class ArrayExtension
	{
		/// <summary>
		/// This method splits the <paramref name="data"/> into sequence of pieces
		/// size of each is not more then <paramref name="maxLength"/>.
		/// (Size of each except the last is equal to <paramref name="maxLength"/>, size of the last can be less).
		/// </summary>
		public static IEnumerable<T[]> SplitIntoPieces<T>(this T[] data, int maxLength)
		{
			Contract.Requires(data != null);

			if(maxLength == -1 || maxLength >= data.Length) {
				yield return data;
				yield break;
			}

			int bufferOfsetInData = 0;
			do {
				int pieceLength = Math.Min(data.Length - bufferOfsetInData, maxLength);
				var buffer = new T[pieceLength];
				Array.Copy(data, bufferOfsetInData, buffer, 0, pieceLength);
				bufferOfsetInData += pieceLength;
				yield return buffer;
			} while(data.Length > bufferOfsetInData);
		}

		/// <summary>
		/// This method tries to cut a string lines from the head of the <paramref name="data"/>.
		/// Each line is returned as a pair: line and enumerator over all data after this line (tail of the <paramref name="data"/>).
		/// </summary>
		public static IEnumerable<KeyValuePair<string, IEnumerable<byte>>> CutLines(this byte[] data)
		{
			Contract.Requires(data != null);

			int lineBegin = 0;
			for(int position = 0; position < data.Length; ++position)
				if(data[position] == '\n') {
					string line = Encoding.UTF8.GetString(data, lineBegin, position - lineBegin);
					lineBegin = position + 1;
					yield return new KeyValuePair<string, IEnumerable<byte>>(line, data.Skip(lineBegin));
				}
		}
	}
}