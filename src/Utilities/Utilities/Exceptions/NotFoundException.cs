namespace Utilities.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" with ID: {key} was not found ")
        {
        }
    }
}
