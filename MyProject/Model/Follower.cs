namespace MyProject.Model
{
    public class Follower
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string FollowingUserId { get; set; }
        public User FollowingUser { get; set; }
    }
}
