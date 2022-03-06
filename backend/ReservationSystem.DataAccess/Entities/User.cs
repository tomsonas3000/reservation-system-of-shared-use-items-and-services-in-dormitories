using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public sealed class User : IdentityUser<Guid>
    {
        private readonly List<Reservation> reservationsList = new();

        private User()
        {
        }

        public static Result<User> Create(string? name, string? surname, string? email, string? phoneNumber)
        {
            var result = new Result<User>();
            var nameResult = RequiredString.Create(result, name, nameof(Name), 200);
            var surnameResult = RequiredString.Create(result, surname, nameof(Surname), 200);
            var emailResult = RequiredEmail.Create(result, email, nameof(Email));
            var phoneNumberResult = RequiredPhoneNumber.Create(result, phoneNumber, nameof(PhoneNumber));

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<User>(new User
            {
                Name = nameResult.Value.Value,
                Surname = surnameResult.Value.Value,
                Email = emailResult.Value.Value,
                PhoneNumber = phoneNumberResult.Value.Value,
                UserName = emailResult.Value.Value,
            });
        }
        
        public override Guid Id { get; set; }

        public ICollection<Reservation> Reservations => reservationsList.AsReadOnly();

        public string? Name { get; private set; }
        
        public string? Surname { get; private set; }

        public Dormitory? Dormitory { get; private set; }
    }
}