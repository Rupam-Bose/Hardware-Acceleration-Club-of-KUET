namespace Hardware_Accelaration_Club_of_KUET_HACK_.Models
{
    public class ProfileDetails
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string? ProfileImage { get; set; }
        public string? Bio { get; set; }
        public string? About { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Website { get; set; }
        public string? Location { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}