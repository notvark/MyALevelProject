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

        public bool IsUserFollowingSearchedUser(User followerObject, User searchedUser)
        {
             return followerObject.FollowingUsers.Any(following => following.FollowingUserId == searchedUser.Id);
        }

        public async Task Follow(Follower followerObject, User searchedUser)
        {
            if (!searchedUser.FollowedUsers.Contains(followerObject))
            {
                searchedUser.FollowedUsers.Add(followerObject);
                followerObject.FollowingUser.FollowingUsers.Remove(followerObject);
                await _context.SaveChangesAsync();
            }  
        }

        public async Task Unfollow(Follower followerObject, User searchedUser)
        {
            if (searchedUser.FollowedUsers.Contains(followerObject))
            {
                searchedUser.FollowedUsers.Remove(followerObject);
                followerObject.FollowingUser.FollowingUsers.Remove(followerObject);
                await _context.SaveChangesAsync();
            }
        }


    }
}
