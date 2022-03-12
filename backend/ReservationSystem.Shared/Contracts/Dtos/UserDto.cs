using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string TelehponeNumber { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string Role { get; set; }
        
        public string Dormitory { get; set; }
    }
}