namespace Cukiernia.MVC.Models
{
    public class emailDto
    {
        public string fromAddress { get; set; } = default!;
        public string toEmail { get; set; } = default!;
        public string subject { get; set; } = default!;
        public string body { get; set; } = default!;
    }
}
