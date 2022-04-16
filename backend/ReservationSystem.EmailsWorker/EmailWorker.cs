using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace ReservationSystem.EmailsWorker
{
    public class EmailWorker : BackgroundService
    {
        private readonly ILogger<EmailWorker> logger;
        private readonly IConfiguration configuration;

        public EmailWorker(ILogger<EmailWorker> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await using (SqlConnection connection = new(configuration["ConnectionStrings:Database"]))
                {
                    await connection.OpenAsync(stoppingToken);
                    var reservationsThatExpireData = await GetReservationsWhichExpireData(connection, stoppingToken);
                    if (reservationsThatExpireData.Any())
                    {
                        using var smtp = new SmtpClient();
                        await smtp.ConnectAsync(configuration["EmailSettings:Host"],
                            Int32.Parse(configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls,
                            stoppingToken);
                        await smtp.AuthenticateAsync(configuration["EmailSettings:Email"],
                            configuration["EmailSettings:Password"], stoppingToken);

                        foreach (var reservation in reservationsThatExpireData)
                        {
                            var email = new MimeMessage();
                            email.Sender = MailboxAddress.Parse(configuration["EmailSettings:Email"]);
                            email.To.Add(MailboxAddress.Parse(reservation.RecipientEmail));
                            email.Subject = $"Reservation of {reservation.ServiceName} on room {reservation.RoomName}";
                            var builder = new BodyBuilder
                            {
                                HtmlBody =
                                    $"Your reservation of {reservation.ServiceName} on {reservation.RoomName} expires in 15 minutes."
                            };
                            email.Body = builder.ToMessageBody();
                            await smtp.SendAsync(email, stoppingToken);

                            await connection.OpenAsync(stoppingToken);
                            var setEmailSentCommand =
                                new SqlCommand(
                                    $"UPDATE [dbo].[ReservationDates] SET IsEmailSentOn = 1 WHERE Id = '{reservation.ReservationId}'",
                                    connection);
                            setEmailSentCommand.CommandType = CommandType.Text;
                            await setEmailSentCommand.ExecuteNonQueryAsync(stoppingToken);
                            await connection.CloseAsync();

                            logger.LogInformation("Email sent to {recipient} on {time}", reservation.RecipientEmail,
                                DateTimeOffset.Now);
                        }

                        await smtp.DisconnectAsync(true, stoppingToken);
                    }
                }
                logger.LogInformation("Worked checked for not sent emails on {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }

        private async Task<List<ReservationEmailData>> GetReservationsWhichExpireData(SqlConnection connection,
            CancellationToken stoppingToken)
        {
            var selectCommand = new SqlCommand(@"
                SELECT 
                rd.[Id] AS ReservationId, 
                anu.[Email] AS RecipientEmail, 
                s.[Name] AS ServiceName, 
                r.RoomName AS RoomName
                FROM [ReservationSystem].[dbo].[ReservationDates] rd
                    INNER JOIN [dbo].[AspNetUsers] anu ON rd.UserId = anu.Id
                INNER JOIN [dbo].[Services] s ON s.Id = rd.ServiceId
                INNER JOIN [dbo].[Rooms] r ON r.Id = s.RoomId
                WHERE DATEDIFF(minute, TODATETIMEOFFSET(GETDATE(), '-03:00'), EndTime) < 15 
                AND DATEDIFF(minute, TODATETIMEOFFSET(GETDATE(), '-03:00'), EndTime) > 0
                AND IsEmailSentOn = 0", connection);
            selectCommand.CommandType = CommandType.Text;

            var reader = await selectCommand.ExecuteReaderAsync(stoppingToken);

            List<ReservationEmailData> results = new();

            while (await reader.ReadAsync(stoppingToken))
            {
                results.Add(new ReservationEmailData
                {
                    RecipientEmail = reader["RecipientEmail"].ToString()!,
                    ReservationId = Guid.Parse(reader["ReservationId"].ToString()!),
                    RoomName = reader["RoomName"].ToString()!,
                    ServiceName = reader["ServiceName"].ToString()!,
                });
            }

            await connection.CloseAsync();

            return results;
        }
    }
}