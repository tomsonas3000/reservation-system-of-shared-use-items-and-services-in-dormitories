using System;
using FluentAssertions;
using ReservationSystem.DataAccess.Entities;
using Xunit;

namespace ReservationSystem.Tests.UnitTests
{
    public class ReservationTests
    {
        [Fact]
        public void Should_Create_Reservation()
        {
            var user = CreateUser();
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);
            var savedBeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginDate.Hour, beginDate.Minute, 0, beginDate.Kind);
            var savedEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0, endDate.Kind);
            var createResult = Reservation.Create(service, user, beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeTrue();
            createResult.Value.Should().BeEquivalentTo(new
            {
                Service = service,
                BeginTime = savedBeginDate,
                EndTime = savedEndDate,
            });
        }
        
        [Fact]
        public void Should_Update_Reservation()
        {
            var reservation = CreateReservation();
            var beginDate = DateTime.Now.AddMinutes(4);
            var endDate = DateTime.Now.AddMinutes(14);
            var savedBeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginDate.Hour, beginDate.Minute, 0, beginDate.Kind);
            var savedEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0, endDate.Kind);
            var reservationUpdateResult = reservation.Update(beginDate.ToString(), endDate.ToString());
            reservationUpdateResult.IsSuccess.Should().BeTrue();
            reservation.BeginTime.Should().Be(savedBeginDate);
            reservation.EndTime.Should().Be(savedEndDate);
        }

        [Fact]
        public void Should_Validate_BeginTime_EndTime_When_Time_Span_Too_Big()
        {
            var user = CreateUser();
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(30);
            var createResult = Reservation.Create(service, user, beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Validate_Begin_EndTime_When_EndTime_Before_Now()
        {
            var user = CreateUser();
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(-15);
            var endDate = DateTime.Now.AddMinutes(-5);
            var createResult = Reservation.Create(service, user, beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Validate_Time_Span_When_Updating()
        {
            var reservation = CreateReservation();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(25);
            var createResult = reservation.Update(beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Validate_If_Reservation_Is_Not_Created_For_Past_Time_When_Updating()
        {
            var reservation = CreateReservation();
            var beginDate = DateTime.Now.AddMinutes(-5);
            var endDate = DateTime.Now.AddMinutes(-1);
            var createResult = reservation.Update(beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Validate_If_Reservation_End_Time_Is_Not_Sooner_Than_Begin_Time_When_Updating()
        {
            var reservation = CreateReservation();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(4);
            var createResult = reservation.Update(beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Allow_To_Make_Reservation_When_User_Banned_From_Reserving()
        {
            var user = CreateUser();
            var service = CreateService();
            user.SetIsBannedFromReserving(true);
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);
            var createResult = Reservation.Create(service, user, beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Allow_To_Update_Reservation_When_User_Banned_From_Reserving()
        {
            var reservation = CreateReservation();
            reservation.User.SetIsBannedFromReserving(true);
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);
            var createResult = reservation.Update(beginDate.ToString(), endDate.ToString());
            createResult.IsSuccess.Should().BeFalse();
        }
        
        private static User CreateUser()
        {
            return User.Create("name", "surname", "test@email.com", "+37061111111").Value;
        }

        private static Service CreateService()
        {
            return Service.Create("service", "volleyball", 15, Guid.NewGuid(), Guid.NewGuid()).Value;
        }

        private static Reservation CreateReservation()
        {
            var user = CreateUser();
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);
            var savedBeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginDate.Hour, beginDate.Minute, 0, beginDate.Kind);
            var savedEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0, endDate.Kind);
            var createResult = Reservation.Create(service, user, beginDate.ToString(), endDate.ToString());

            return createResult.Value;
        }
    }
}