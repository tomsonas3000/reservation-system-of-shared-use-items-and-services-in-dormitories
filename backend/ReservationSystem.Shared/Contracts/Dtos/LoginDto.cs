namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}