using Hardware_Accelaration_Club_of_KUET_HACK_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContactApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(
            [FromBody] ContactMessage model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(
                    _configuration.GetConnectionString("DBCon")))
                {
                    await con.OpenAsync();

                    string query =
                    @"INSERT INTO ContactMessages
                    (
                        FullName,
                        Email,
                        Subject,
                        Message,
                        SentAt,
                        IsRead
                    )
                    VALUES
                    (
                        @FullName,
                        @Email,
                        @Subject,
                        @Message,
                        GETDATE(),
                        0
                    )";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add(
                            "@FullName",
                            SqlDbType.NVarChar).Value =
                            model.FullName;

                        cmd.Parameters.Add(
                            "@Email",
                            SqlDbType.NVarChar).Value =
                            model.Email;

                        cmd.Parameters.Add(
                            "@Subject",
                            SqlDbType.NVarChar).Value =
                            model.Subject;

                        cmd.Parameters.Add(
                            "@Message",
                            SqlDbType.NVarChar).Value =
                            model.Message;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Ok(new
                {
                    success = true,
                    message = "Message sent successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}