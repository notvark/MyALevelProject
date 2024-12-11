using Microsoft.EntityFrameworkCore;
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

        private List<Follower> ReturnFollowers(User user)
        {
            return user.FollowingUsers;
        }

        private List<Follower> ReturnFollowing(User user)
        {
            return user.FollowedUsers;
        }

    }
}
