using MyProject.Model;

namespace MyProject.Context
{
    public class FollowingProvider
    {
        private readonly DatabaseContext _context;

        public FollowingProvider(DatabaseContext context)
        {
            _context = context;
        }

        public List<User> ReturnFollowers(User user)
        {
            return _context.Users
                .Where(user => user.FollowedUsers.Any())
                .ToList();
        }

        public List<User> ReturnFollowing(User user)
        {
            return _context.Users
                .Where(user => user.FollowingUsers.Any())
                .ToList();
        }
    }
}
