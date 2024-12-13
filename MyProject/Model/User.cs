using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;

namespace MyProject.Model
{
    public class User : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public string? Biography { get; set; }
        public string? BiologicalSex { get; set; }
        public string? Name { get; set; }
        public int? ForumAmount { get; set; }
        public DateTime? CreatedAt { get; set; }

        
        // Relationships
        public List<Post> Posts { get; set; }
        public List<PostComment> PostComments { get; set; }
        public List<Note> Notes { get; set; }
        public List<Chat> SentChats { get; set; }  // User sending the chat
        public List<Chat> ReceivedChats { get; set; }  // User receiving the chat
        public List<Follower> FollowedUsers { get; set; } = new List<Follower>();
        public List<Follower> FollowingUsers { get; set; } = new List<Follower>();

        /* Custom functions below */


    }
}
