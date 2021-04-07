using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using EmailSender.Models;
using System.Data;

namespace EmailSender.Repository
{
    public class MailingRepository : IRepository<Mailing>
    {
        private readonly string connectionString;

        public MailingRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("postgresqldb");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public async Task AddAsync(Mailing item)
        {
            using(var connection = Connection)
            {
                connection.Open();                
                await connection.ExecuteAsync($"INSERT INTO \"{connection.Database}\".MAILING " +
                    "(RECIPIENT, SUBJECT, MESSAGE_TEXT, CARBON_COPY_RECIPIENTS, DEPARTURE_DATE, STATUS) " +
                    "VALUES(@Recipient, @Subject, @Message_text, @CCR, NOW(), @status)", 
                    new { Recipient = item.Recipient, Subject = item.Subject, Message_text = item.Message_text, CCR = item.CarbonCopyRecipients, Status = item.Status });
            };
        }

        public async Task<IEnumerable<Mailing>> FindAllAsync()
        {
            using (var connection = Connection)
            {
                connection.Open();                
                return await connection.QueryAsync<Mailing>($"SELECT * FROM \"{connection.Database}\".MAILING");
            }
        }
    }
}
