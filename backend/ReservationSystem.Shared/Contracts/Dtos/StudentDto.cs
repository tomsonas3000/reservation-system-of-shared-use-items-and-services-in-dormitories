using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string TelephoneNumber { get; set; }
        
        public string EmailAddress { get; set; }

        public Guid? DormitoryId { get; set; }
    }
}