using System;
using System.Collections.Generic;
using FluentAssertions;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Tests.Constants;
using Xunit;

namespace ReservationSystem.Tests.UnitTests
{
    public class UserTests
    {
        [Fact]
        public void Should_Create_User()
        {
            var name = "name";
            var surname = "surname";
            var email = "test@email.com";
            var phoneNumber = "+37061111111";

            var createResult = User.Create(name, surname, email, phoneNumber);
            createResult.IsSuccess.Should().BeTrue();
            createResult.Value.Should().BeEquivalentTo(new
            {
                Name = name,
                Surname = surname,
                Email = email,
                PhoneNumber = phoneNumber,
            });
        }

        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length201String, false)]
        public void Should_Validate_Name_When_Creating(string name, bool expected)
        {
            var createResult = User.Create(name, "surname", "test@email.com", "+37061111111");
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length201String, false)]
        public void Should_Validate_Surname_When_Creating(string surname, bool expected)
        {
            var createResult = User.Create("name", surname, "test@email.com", "+37061111111");
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("test@email.com", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length201String, false)]
        [InlineData("testEmail.com", false)]
        [InlineData("@email.com", false)]
        [InlineData("test@emailcom", false)]
        public void Should_Validate_Email_When_Creating(string email, bool expected)
        {
            var createResult = User.Create("name", "surname", email, "+37061111111");
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("+37061111111", true)]
        [InlineData("+370611111111", false)]
        [InlineData("+3706111111", false)]
        [InlineData("+3606111111", false)]
        [InlineData("3606111111", false)]
        [InlineData("86061111111", false)]
        [InlineData("8606111111", false)]
        [InlineData("+3706111111b", false)]
        public void Should_Validate_PhoneNumber_When_Creating(string phoneNumber, bool expected)
        {
            var createResult = User.Create("name", "surname", "test@email.com", phoneNumber);
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_SetIsBannedFromReserving(bool value)
        {
            var user = CreateUser();
            user.SetIsBannedFromReserving(value);
            user.IsBannedFromReserving.Should().Be(value);
        }

        [Fact]
        public void Should_Add_Reservation()
        {
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);

            var reservation = CreateReservation(beginDate, endDate);

            var addReservationResult = service.AddReservation(reservation);
            addReservationResult.IsSuccess.Should().BeTrue();
            service.ReservationsList.Should().BeEquivalentTo(new List<object> {reservation});

        }

        private static Service CreateService()
        {
            return Service.Create("service", "volleyball", 15, Guid.NewGuid(), Guid.NewGuid()).Value;
        }
        
        private static User CreateUser()
        {
            return User.Create("name", "surname", "test@email.com", "+37061111111").Value;
        }

        private static Reservation CreateReservation(DateTime beginDate, DateTime endDate)
        {
            var user = CreateUser();
            var service = CreateService();
            return Reservation.Create(service, user, beginDate.ToString(), endDate.ToString()).Value;
        }
    }
}