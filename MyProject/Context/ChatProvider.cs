using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace MyProject.Context
{
    public class ChatProvider
    {
        private readonly DatabaseContext _context;

        public async Task<List<Chat>> RetrieveTextsOrdered(User currentUser, User chatToUser)
        {
            return await _context.Chats
                .Where(chat => chat.FromUserId == currentUser.Id && chat.ToUserId == chatToUser.Id)
                .OrderBy(chat => chat.SentDateTime)
                .ToListAsync();
        }

    }

}
