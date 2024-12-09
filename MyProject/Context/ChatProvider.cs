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
                .Where(chat => chat.FromUserId == currentUser.Id || chat.ToUserId == currentUser.Id)
                .ToListAsync();

            var usersMessaged = new List<User>();

            usersMessaged = chats.Select(chat => chat.ToUser).Distinct().ToList();

            return usersMessaged;
        }

        public async Task SendMessageAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Chat chat)
        {
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }
    }
}