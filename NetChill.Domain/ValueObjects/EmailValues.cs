namespace NetChill.Domain.ValueObjects
{
    public class EmailValues
    {
        public string EmailFrom { get; set; }
        public string DisplayName { get; set; }
        public string AuthPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
