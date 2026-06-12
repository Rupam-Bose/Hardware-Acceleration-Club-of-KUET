using System.Data;
using Hardware_Accelaration_Club_of_KUET_HACK_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using BCrypt.Net;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] Users registration)
        {
            if (registration == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(
                    _configuration.GetConnectionString("DBCon")))
                {
                    await connection.OpenAsync();

                    string query = @"
                        INSERT INTO Users
                        (FullName, Username, Email, Department, BatchSession, PasswordHash, IsActive, CreatedAt)
                        VALUES
                        (@FullName, @Username, @Email, @Department, @BatchSession, @PasswordHash, @IsActive, @CreatedAt)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = registration.FullName;
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = registration.Username;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = registration.Email;
                        cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = registration.Department;
                        cmd.Parameters.Add("@BatchSession", SqlDbType.NVarChar).Value = registration.BatchSession;

                        // IMPORTANT: never store plain password
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value =
                            BCrypt.Net.BCrypt.HashPassword(registration.PasswordHash);

                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
                        cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = DateTime.UtcNow;

                        int rows = await cmd.ExecuteNonQueryAsync();

                        if (rows > 0)
                        {
                            return Ok(new
                            {
                                message = "User registered successfully",
                                success = true
                            });
                        }

                        return BadRequest(new
                        {
                            message = "Registration failed",
                            success = false
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server error",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] login login)
        {
            if (login == null)
            {
                return BadRequest("Invalid login data");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(
                    _configuration.GetConnectionString("DBCon")))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT UserID, FullName, Username, Email, PasswordHash
                        FROM Users
                        WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar)
                            .Value = login.Email;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                            {
                                return Unauthorized(new
                                {
                                    success = false,
                                    message = "Email not found"
                                });
                            }

                            string storedHash = reader["PasswordHash"].ToString();

                            bool isValidPassword = BCrypt.Net.BCrypt.Verify(
                                login.Password,
                                storedHash
                            );

                            if (!isValidPassword)
                            {
                                return Unauthorized(new
                                {
                                    success = false,
                                    message = "Invalid password"
                                });
                            }

                            return Ok(new
                            {
                                success = true,
                                message = "Login successful",
                                user = new
                                {
                                    UserID = reader["UserID"],
                                    FullName = reader["FullName"],
                                    Username = reader["Username"],
                                    Email = reader["Email"]
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Server error",
                    error = ex.Message
                });
            }
        }
    }
}