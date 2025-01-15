using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace MyProject.Context
{
    public class ChatProvider
    {
        private readonly DatabaseContext _context;

        public ChatProvider(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Chat>> RetrieveTextsOrdered(User currentUser, User chatToUser)
        {
            return await _context.Chats
                .Where(chat =>
                    (chat.FromUserId == currentUser.Id && chat.ToUserId == chatToUser.Id) || //Or 
                    (chat.FromUserId == chatToUser.Id && chat.ToUserId == currentUser.Id)) //Switching receiving to client and client to receiving
                .OrderBy(chat => chat.SentDateTime)
                .ToListAsync();
        }

        public async Task<List<User>> RetrieveMessageList(User currentUser)
        {
            var chats = await _context.Chats
                .Where(chat => chat.FromUserId == currentUser.Id || chat.ToUserId == currentUser.Id) //retrieving chats sent to or sent from current user
                .ToListAsync(); //transforming it into a list

            var userIds = chats
                .SelectMany(chat => new[] { chat.FromUserId, chat.ToUserId }) //extracts User Ids from both FromUserID and ToUserID and puts it into a flat list
                .Where(userId => userId != currentUser.Id) //excluding my ID
                .Distinct() //unique, removes duplicates
                .ToList(); //transforms into a list

            var usersMessaged = await _context.Users
                .Where(user => userIds.Contains(user.Id)) //user IDs from the userIds list are turned into a list of users
                .ToListAsync(); //transforms into a list

            return usersMessaged; //returns list of users
        }

        public async Task SendMessageAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Chat chat)
        {
            _context.Chats.Remove(chat); //deletes the chat from the database
            await _context.SaveChangesAsync(); //saves changes to the database
        }
    }
}