namespace Auth.Models
{
    public class BadRequestException: Exception
    {
        public BadRequestException( string message) : base(message)
        {
            
        }
    }
}
