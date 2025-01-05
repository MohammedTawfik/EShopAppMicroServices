namespace Utilities.Exceptions
{
    public class ServerException : Exception
    {
        public string? Details { get; }
        public ServerException(string message) : base(message)
        {
        }

        public ServerException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
