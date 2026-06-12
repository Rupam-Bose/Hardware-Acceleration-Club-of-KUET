using Hardware_Accelaration_Club_of_KUET_HACK_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Users user = new Users();
                AcademicInfo academic = new AcademicInfo();
                ProfileDetails profile = new ProfileDetails();
                SocialLinks social = new SocialLinks();

                using (SqlConnection con = new SqlConnection(
                    _configuration.GetConnectionString("DBCon")))
                {
                    con.Open();

                    // Users
                    SqlCommand cmd1 = new SqlCommand(
                        "SELECT TOP 1 * FROM Users", con);

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

                    // AcademicInfo
                    SqlCommand cmd2 = new SqlCommand(
                        "SELECT TOP 1 * FROM AcademicInfo WHERE UserId=@id", con);

                    cmd2.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.Read())
                    {
                        academic.University = reader2["University"].ToString();
                        academic.Department = reader2["Department"].ToString();
                        academic.Batch = reader2["Batch"].ToString();
                        academic.Semester = reader2["Semester"].ToString();
                    }

                    reader2.Close();

                    // ProfileDetails
                    SqlCommand cmd3 = new SqlCommand(
                        "SELECT TOP 1 * FROM ProfileDetails WHERE UserId=@id", con);

                    cmd3.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    if (reader3.Read())
                    {
                        profile.Bio = reader3["Bio"].ToString();
                        profile.About = reader3["About"].ToString();
                        profile.Website = reader3["Website"].ToString();
                        profile.Location = reader3["Location"].ToString();

                        if (reader3["DateOfBirth"] != DBNull.Value)
                            profile.DateOfBirth = Convert.ToDateTime(reader3["DateOfBirth"]);

                        profile.ProfileImage = reader3["ProfileImage"].ToString();
                    }

                    reader3.Close();

                    // SocialLinks
                    SqlCommand cmd4 = new SqlCommand(
                        "SELECT TOP 1 * FROM SocialLinks WHERE UserId=@id", con);

                    cmd4.Parameters.AddWithValue("@id", user.UserID);

                    SqlDataReader reader4 = cmd4.ExecuteReader();

                    if (reader4.Read())
                    {
                        social.LinkedIn = reader4["LinkedIn"].ToString();
                        social.Twitter = reader4["Twitter"].ToString();
                        social.GitHub = reader4["GitHub"].ToString();
                        social.Website = reader4["Website"].ToString();
                    }

                    reader4.Close();
                }

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        fullName = user.FullName,
                        username = user.Username,
                        email = user.Email,
                        department = user.Department,
                        batchSession = user.BatchSession,

                        university = academic.University,
                        semester = academic.Semester,

                        bio = profile.Bio,
                        about = profile.About,
                        website = profile.Website,
                        location = profile.Location,
                        dateOfBirth = profile.DateOfBirth,
                        profileImage = profile.ProfileImage,

                        linkedIn = social.LinkedIn,
                        twitter = social.Twitter,
                        gitHub = social.GitHub,
                        portfolio = social.Website
                    }
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