using System.ComponentModel.DataAnnotations;

namespace Hardware_Accelaration_Club_of_KUET_HACK_.Models
{
    public class Users
    {
            public int UserID { get; set; }

            [Required]
            public string FullName { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Department { get; set; }

            [Required]
            public string BatchSession { get; set; }

            [Required]
            public string PasswordHash { get; set; }

            public bool IsActuve { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    }
}