using Hardware_Accelaration_Club_of_KUET_HACK_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProfileApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProfile()
        {
            try
            {
                Users user = new Users();
                AcademicInfo academic = new AcademicInfo();
                ProfileDetails profile = new ProfileDetails();
                SocialLinks social = new SocialLinks();

                using (SqlConnection con =
                    new SqlConnection(_configuration.GetConnectionString("DBCon")))
                {
                    con.Open();

                    SqlCommand cmd1 =
                        new SqlCommand("SELECT TOP 1 * FROM Users", con);

                    SqlDataReader reader1 = cmd1.ExecuteReader();

                    if (reader1.Read())
                    {
                        user.UserID = Convert.ToInt32(reader1["UserID"]);
                        user.FullName = reader1["FullName"].ToString();
                        user.Username = reader1["Username"].ToString();
                        user.Email = reader1["Email"].ToString();
                        user.Department = reader1["Department"].ToString();
                        user.BatchSession = reader1["BatchSession"].ToString();
                    }

                    reader1.Close();

                    SqlCommand cmd2 = new SqlCommand(
                        "SELECT TOP 1 * FROM AcademicInfo WHERE UserId=@id",
                        con);

                    cmd2.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.Read())
                    {
                        academic.AcademicId =
                            Convert.ToInt32(reader2["AcademicId"]);

                        academic.UserId =
                            Convert.ToInt32(reader2["UserId"]);

                        academic.University =
                            reader2["University"]?.ToString();

                        academic.Department =
                            reader2["Department"]?.ToString();

                        academic.Batch =
                            reader2["Batch"]?.ToString();

                        academic.Semester =
                            reader2["Semester"]?.ToString();
                    }

                    reader2.Close();


                    SqlCommand cmd3 = new SqlCommand(
                        "SELECT TOP 1 * FROM ProfileDetails WHERE UserId=@id",
                        con);

                    cmd3.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    if (reader3.Read())
                    {
                        profile.ProfileId =
                            Convert.ToInt32(reader3["ProfileId"]);

                        profile.UserId =
                            Convert.ToInt32(reader3["UserId"]);

                        profile.Bio =
                            reader3["Bio"]?.ToString();

                        profile.About =
                            reader3["About"]?.ToString();

                        profile.Website =
                            reader3["Website"]?.ToString();

                        profile.Location =
                            reader3["Location"]?.ToString();

                        profile.ProfileImage =
                            reader3["ProfileImage"]?.ToString();

                        if (reader3["DateOfBirth"] != DBNull.Value)
                        {
                            profile.DateOfBirth =
                                Convert.ToDateTime(reader3["DateOfBirth"]);
                        }

                        if (reader3["JoinedDate"] != DBNull.Value)
                        {
                            profile.JoinedDate =
                                Convert.ToDateTime(reader3["JoinedDate"]);
                        }
                    }

                    reader3.Close();


                    SqlCommand cmd4 = new SqlCommand(
                        "SELECT TOP 1 * FROM SocialLinks WHERE UserId=@id",
                        con);

                    cmd4.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader4 = cmd4.ExecuteReader();

                    if (reader4.Read())
                    {
                        social.SocialId =
                            Convert.ToInt32(reader4["SocialId"]);

                        social.UserId =
                            Convert.ToInt32(reader4["UserId"]);

                        social.LinkedIn =
                            reader4["LinkedIn"]?.ToString();

                        social.Twitter =
                            reader4["Twitter"]?.ToString();

                        social.GitHub =
                            reader4["GitHub"]?.ToString();

                        social.Website =
                            reader4["Website"]?.ToString();
                    }

                    reader4.Close();
                }

                return Ok(new
                {
                    success = true,
                    user = user,
                    academicInfo = academic,
                    profileDetails = profile,
                    socialLinks = social
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


        [HttpPut]
        public IActionResult UpdateProfile(
            [FromBody] ProfileUpdateDto model)
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(_configuration.GetConnectionString("DBCon")))
                {
                    con.Open();

                    int userId = model.UserId;

                    // ===========================
                    // USERS TABLE
                    // ===========================
                    SqlCommand cmd1 = new SqlCommand(
                    @"UPDATE Users
                      SET FullName=@FullName,
                          Username=@Username,
                          Email=@Email,
                          Department=@Department,
                          BatchSession=@BatchSession
                      WHERE UserID=@UserID", con);

                    cmd1.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd1.Parameters.AddWithValue("@Username", model.Username);
                    cmd1.Parameters.AddWithValue("@Email", model.Email);
                    cmd1.Parameters.AddWithValue("@Department", model.Department);
                    cmd1.Parameters.AddWithValue("@BatchSession", model.BatchSession);
                    cmd1.Parameters.AddWithValue("@UserID", userId);

                    cmd1.ExecuteNonQuery();


                    SqlCommand cmd2 = new SqlCommand(
                    @"UPDATE AcademicInfo
                      SET University=@University,
                          Department=@Department,
                          Batch=@Batch,
                          Semester=@Semester
                      WHERE UserId=@UserID", con);

                    cmd2.Parameters.AddWithValue("@University", model.University);
                    cmd2.Parameters.AddWithValue("@Department", model.Department);
                    cmd2.Parameters.AddWithValue("@Batch", model.BatchSession);
                    cmd2.Parameters.AddWithValue("@Semester", model.Semester);
                    cmd2.Parameters.AddWithValue("@UserID", userId);

                    cmd2.ExecuteNonQuery();


                    SqlCommand cmd3 = new SqlCommand(
                    @"UPDATE ProfileDetails
                      SET Bio=@Bio,
                          About=@About,
                          Website=@Website,
                          ProfileImage = COALESCE(@ProfileImage, ProfileImage)
                      WHERE UserId=@UserID", con);

                    cmd3.Parameters.AddWithValue(
                        "@ProfileImage",
                        (object?)model.ProfileImage ?? DBNull.Value);

                    cmd3.Parameters.AddWithValue("@Bio", model.Bio);
                    cmd3.Parameters.AddWithValue("@About", model.About);
                    cmd3.Parameters.AddWithValue("@Website", model.Website);
                    cmd3.Parameters.AddWithValue("@UserID", userId);

                    cmd3.ExecuteNonQuery();

 
                    SqlCommand cmd4 = new SqlCommand(
                    @"UPDATE SocialLinks
                      SET LinkedIn=@LinkedIn,
                          Twitter=@Twitter,
                          GitHub=@GitHub,
                          Website=@Website
                      WHERE UserId=@UserID", con);

                    cmd4.Parameters.AddWithValue("@LinkedIn", model.LinkedIn);
                    cmd4.Parameters.AddWithValue("@Twitter", model.Twitter);
                    cmd4.Parameters.AddWithValue("@GitHub", model.GitHub);
                    cmd4.Parameters.AddWithValue("@Website", model.Website);
                    cmd4.Parameters.AddWithValue("@UserID", userId);

                    cmd4.ExecuteNonQuery();
                }

                return Ok(new
                {
                    success = true,
                    message = "Profile updated successfully"
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