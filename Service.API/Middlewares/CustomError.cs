namespace Service.API.Middlewares
{
    public class CustomError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public int Error { get; set; }

        public override string ToString()
        {
            return $"StatusCode: {StatusCode}\n" +
                   $"Message:    {Message}\n" +
                   $"Details:    {Details}\n" +
                   $"Error Code: {Error}";
        }
    }
}
