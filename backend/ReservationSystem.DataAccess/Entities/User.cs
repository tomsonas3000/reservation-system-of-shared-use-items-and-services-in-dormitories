using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public sealed class User : IdentityUser<Guid>
    {
        private List<Reservation> reservationsList = new();

        public User()
        {
        }

        public ICollection<Reservation> Reservations => reservationsList;

        public string? Name { get; internal set; }
        
        public string? Surname { get; internal set; }
        
        public Guid? DormitoryId { get; private set; }

        public Dormitory? Dormitory { get; private set; }
        
        public bool IsBannedFromReserving { get; private set; }

        public static Result<User> Create(string? name, string? surname, string? email, string? phoneNumber)
        {
            var result = new Result<User>();
            var nameResult = RequiredString.Create(result, name, nameof(Name), 200);
            var surnameResult = RequiredString.Create(result, surname, nameof(Surname), 200);
            var emailResult = RequiredEmail.Create(result, email, "emailAddress");
            var phoneNumberResult = RequiredPhoneNumber.Create(result, phoneNumber, "telephoneNumber");

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<User>(new User
            {
                Name = nameResult!.Value.Value,
                Surname = surnameResult!.Value.Value,
                Email = emailResult!.Value.Value,
                PhoneNumber = phoneNumberResult!.Value.Value,
                UserName = emailResult.Value.Value,
            });
        }

        public void SetIsBannedFromReserving(bool value)
        {
            IsBannedFromReserving = value;
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservationsList.Contains(reservation))
            {
                reservationsList.Remove(reservation);
            }
            
            reservationsList.Add(reservation);
        }

        public void SetDormitory(Dormitory dormitory)
        {
            Dormitory = dormitory;
        }

    }
}