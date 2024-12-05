namespace MyProject.Model
{
    public class PostComment
    {
        public int Id { get; set; }  // Primary Key
        public string Comment { get; set; }
        public DateTime CommentSentTime { get; set; }

        // Navigation Properties
        public User User { get; set; }  // User who commented
        public Post Post { get; set; }  // Post related to this comment
    }
}
