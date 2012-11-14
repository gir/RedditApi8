//------------------------------------------------------------------------------
// <copyright file="FakeHttpServer.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace RedditApi8.Tests
{
    using System;
    using System.Net;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Networking.Sockets;
    using Windows.Storage.Streams;

    /// <summary>
    /// FakeHttpServer class.
    /// This is used to mock out responses made to localhost.
    /// </summary>
    public class FakeHttpServer
    {
        /// <summary>
        /// The buffer length.
        /// </summary>
        private const int BufferLength = 512;

        /// <summary>
        /// The listener.
        /// </summary>
        private StreamSocketListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeHttpServer" /> class.
        /// </summary>
        public FakeHttpServer()
        {
            this.listener = new StreamSocketListener();
            this.listener.ConnectionReceived += this.OnConnection;
            this.HttpStatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Gets or sets the content of the response.
        /// </summary>
        /// <value>
        /// The content of the response.
        /// </value>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Returns a Task.</returns>
        public async Task Initialize()
        {
            await this.listener.BindServiceNameAsync("80");
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.listener.Dispose();
        }

        /// <summary>
        /// Called when [connection].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="StreamSocketListenerConnectionReceivedEventArgs" /> instance containing the event data.</param>
        private async void OnConnection(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            StringBuilder request = new StringBuilder();
            try
            {
                // Get the whole request.
                using (IInputStream inputStream = args.Socket.InputStream)
                {
                    Windows.Storage.Streams.Buffer buffer = new Windows.Storage.Streams.Buffer(BufferLength);
                    do
                    {
                        await inputStream.ReadAsync(buffer, BufferLength, InputStreamOptions.Partial);
                        request.Append(Encoding.UTF8.GetString(buffer.ToArray(), 0, (int)buffer.Length));
                    }
                    while (buffer.Length == BufferLength);
                }

                // Write the response.
                using (IOutputStream output = args.Socket.OutputStream)
                {
                    await output.WriteAsync(this.CreateResponse());
                }
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>Returns a buffer with the HTTP response.</returns>
        private IBuffer CreateResponse()
        {
            return Encoding.UTF8.GetBytes(
                string.Format(
                    "HTTP/1.1 {0} {1}\r\nDate: {2}\r\nContent-Length: {3}\r\nContent-Type: application/json; charset=UTF-8\r\nConnection: close\r\n\r\n{4}",
                    (int)this.HttpStatusCode,
                    this.HttpStatusCode,
                    DateTime.UtcNow.ToString("R"),
                    this.ResponseContent.Length,
                    this.ResponseContent)).AsBuffer();
        }
    }
}