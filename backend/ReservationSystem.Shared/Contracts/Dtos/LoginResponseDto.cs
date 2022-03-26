﻿using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        
        public string Role { get; set; }
        
        public Guid? DormitoryId { get; set; }
    }
}