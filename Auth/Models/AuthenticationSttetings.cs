namespace Auth.Models
{
    public class AuthenticationSttetings
    {
        public string JwtKey { get; set; }
        public string JwtExpireDays { get; set;}
        public string JwtIssuer { get; set; }
    }
}
