using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;

namespace MyProject.Model
{
    public class Post
    {
        public int Id { get; set; }  // Primary Key
        public DateTime PostCreatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Navigation Property (User who created the post)
        public User User { get; set; }

        // Relationships
        public List<PostComment> PostComments { get; set; }
    }
}
