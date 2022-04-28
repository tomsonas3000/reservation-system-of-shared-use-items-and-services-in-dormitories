namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class EmailDto
    {
        public string Recipient { get; set; }
        
        public string Subject { get; set; }
        
        public string Body { get; set; }
    }
}