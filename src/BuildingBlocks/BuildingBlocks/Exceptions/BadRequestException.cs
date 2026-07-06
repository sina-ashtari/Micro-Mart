namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public string? Details;
        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, string detail) : base(message)
        {
            Details = detail;
        }
    }
}
