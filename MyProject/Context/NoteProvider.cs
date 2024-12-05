using Microsoft.EntityFrameworkCore;
using MyProject.Model;

namespace MyProject.Context
{
    public class NoteProvider
    {

        private readonly DatabaseContext _context;

        public NoteProvider(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllForumsAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetNoteByUserAsync(User user)
        {
            if (user == null)
            {
                return new List<Note>();
            }

            return await _context.Notes
                .Where(note => note.User.Id == user.Id)
                .ToListAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

    }
}
