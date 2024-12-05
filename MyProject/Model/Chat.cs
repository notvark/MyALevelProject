namespace MyProject.Model
{
    public class Chat
    {
        public int Id { get; set; }  // Primary Key
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }  
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        // Navigation Properties
        public User FromUser { get; set; }  // Sender
        public User ToUser { get; set; }  // Receiver
    }
}
