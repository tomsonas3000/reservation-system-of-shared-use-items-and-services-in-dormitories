namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }
    }
}