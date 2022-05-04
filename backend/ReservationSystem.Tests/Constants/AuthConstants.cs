using System;

namespace ReservationSystem.Tests.Constants
{
    public static class AuthConstants
    {
        public const string DefaultAdminUserEmail = "admin@email.com";
        public const string DefaultManagerUserEmail = "manager@email.com";
        public const string DefaultStudentUserEmail = "student@email.com";
        public static readonly Guid DefaultAdminUserId = Guid.Parse("25FCF350-FB60-472B-8C3F-68E62654AE83");
        public static readonly Guid DefaultManagerUserId = Guid.Parse("F1CB0CBA-0DB2-45C0-B2AB-81BDA3042337");
        public static readonly Guid DefaultStudentUserId = Guid.Parse("D9A3EBA6-0F43-4736-A1E6-FCDBB8CCE89C");
        public static readonly Guid DefaultDormitoryId = Guid.Parse("24C9ED38-1D3B-46E5-BA46-D467649A619F");
        public static readonly Guid DefaultServiceId = Guid.Parse("86AB9198-B3A2-46CE-9E83-2E531E13A716");
        public static readonly Guid DefaultReservationId = Guid.Parse("30BD887B-0807-4461-B844-290E6AD39616");
        public static readonly Guid DefaultRoomId = Guid.Parse("450740E7-51DC-47C1-B70F-41A77548D109");
        public static readonly string DefaultRoomName = "Room";
    }
}