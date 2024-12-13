using MyProject.Components.Pages;
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

        public bool IsUserFollowingSearchedUser(User currentUser, User searchedUser)
        {
            bool test = currentUser.FollowingUsers.Any(following => following.FollowingUserId == searchedUser.Id);
            return test;
        }

        public async Task Follow(Follower currentUser, User searchedUser)
        {
            searchedUser.FollowedUsers.Add(currentUser);
            await _context.SaveChangesAsync();
        }

        public async Task Unfollow(Follower currentUser, User searchedUser)
        {
            searchedUser.FollowedUsers.Remove(currentUser);
            await _context.SaveChangesAsync();
        }


    }
}
