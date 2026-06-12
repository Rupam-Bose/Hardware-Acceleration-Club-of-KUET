namespace Hardware_Accelaration_Club_of_KUET_HACK_.Models
{
    public class ContactMessage
    {
        public int MessageId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }
    }
}