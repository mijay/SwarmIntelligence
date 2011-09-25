﻿//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: TransferStream.cs
//
//--------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace System.Threading
{
    /// <summary>Writeable stream for using a separate thread in a producer/consumer scenario.</summary>
    public sealed class TransferStream : AbstractStreamBase
    {
        /// <summary>The underlying stream to target.</summary>
        private Stream _writeableStream;
        /// <summary>The collection of chunks to be written.</summary>
        private BlockingCollection<byte[]> _chunks;
        /// <summary>The Task to use for background writing.</summary>
        private Task _processingTask;

        /// <summary>Initializes a new instance of the TransferStream.</summary>
        /// <param name="writeableStream">The underlying stream to which to write.</param>
        public TransferStream(Stream writeableStream)
        {
            // Validate arguments
            if (writeableStream == null) throw new ArgumentNullException("writeableStream");
            if (!writeableStream.CanWrite) throw new ArgumentException("Target stream is not writeable.");

            // Set up the producer/consumer relationship, including starting the consumer running
            _writeableStream = writeableStream;
            _chunks = new BlockingCollection<byte[]>();
            _processingTask = Task.Factory.StartNew(() =>
            {
                // Write out all chunks to the underlying stream
                foreach (var chunk in _chunks.GetConsumingEnumerable())
                    _writeableStream.Write(chunk, 0, chunk.Length);
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>Determines whether data can be written to the stream.</summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>Writes a sequence of bytes to the stream.</summary>
        /// <param name="buffer">An array of bytes. Write copies count bytes from buffer to the stream.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            // Validate all arguments
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || offset >= buffer.Length) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || offset + count > buffer.Length) throw new ArgumentOutOfRangeException("count");
            if (count == 0) return;

            // Store the data to the collection
            var chunk = new byte[count];
            Buffer.BlockCopy(buffer, offset, chunk, 0, count);
            _chunks.Add(chunk);
        }

        /// <summary>Closes the stream and releases all resources associated with it.</summary>
        public override void Close()
        {
            // Complete the collection and waits for the consumer to process all of the data
            _chunks.CompleteAdding();
            try { _processingTask.Wait(); }
            finally { base.Close(); }
        }
    }
}