namespace SoundSoulCore.Response.Common
{
    public class BadResponse
    {
        public bool Result { get; set; }

        public string? Message { get; set; }

        public TypeOperation TypeOperation { get; set; }

        public string? DescriptionError { get; set; }

        public object? Data { get; set; }
    }
}
