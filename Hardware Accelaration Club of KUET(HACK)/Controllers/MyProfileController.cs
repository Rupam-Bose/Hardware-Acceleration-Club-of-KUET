using Hardware_Accelaration_Club_of_KUET_HACK_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyProfileController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MyProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProfile()
        {
            try
            {
                Users user = new Users
                {
                    FullName = "",
                    Username = "",
                    Email = "",
                    Department = "",
                    BatchSession = "",
                    PasswordHash = ""
                };

                using (SqlConnection con = new SqlConnection(
                    _configuration.GetConnectionString("DBCon")))
                {
                    con.Open();

                    string query = "SELECT TOP 1 * FROM Users";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            user.FullName = reader["FullName"].ToString() ?? "";
                            user.Username = reader["Username"].ToString() ?? "";
                            user.Email = reader["Email"].ToString() ?? "";
                            user.Department = reader["Department"].ToString() ?? "";
                            user.BatchSession = reader["BatchSession"].ToString() ?? "";
                        }
                        else
                        {
                            return NotFound(new
                            {
                                success = false,
                                message = "No user found"
                            });
                        }
                    }
                }

                return Ok(new
                {
                    success = true,
                    data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}