namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateUserDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }
        
        public string? EmailAddress { get; set; }
        
        public string? TelephoneNumber { get; set; }
        
        public string? Password { get; set; }
        
        public string? Role { get; set; }
    }
}