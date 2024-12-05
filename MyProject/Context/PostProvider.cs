using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using SQLitePCL;


namespace MyProject.Context
{
    public class PostProvider
    {

        private readonly DatabaseContext _context;

        public PostProvider(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<Post>> GetPostByUserAsync(User user)
        {
            if (user == null)
            {
                return new List<Post>();
            }

            return await _context.Posts
                .Where(post => post.User.Id == user.Id)
                .ToListAsync();
        }

        public string GetUserByPost(Post post)
        {
            var users = _context.Users
            .ToDictionary(user => user.Id, user => user.UserName); 
           
            return users[post.User.Id]; //returns Username
        }

        public async Task AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

    }
}
