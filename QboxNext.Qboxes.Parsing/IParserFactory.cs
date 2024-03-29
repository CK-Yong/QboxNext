using QboxNext.Qboxes.Parsing.Protocols;

namespace QboxNext.Qboxes.Parsing
{
    public interface IParserFactory
    {
        /// <summary>
        /// Gets a parser by extracting the protocol version from the message.
        /// </summary>
        /// <param name="message">The message to extract the protocol version of.</param>
        /// <returns>Returns a message parser.</returns>
        IMessageParser GetParser(string message);
    }
}
