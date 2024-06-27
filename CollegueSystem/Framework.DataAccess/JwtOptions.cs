namespace Service.API
{
    public class JwtOptions
    {
          public string Issure { get; set; }
          public string Audience { get; set; }
          public int Lifetime { get; set; }
          public string SigningKey { get; set; }
    }
}
