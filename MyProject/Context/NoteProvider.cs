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
            if (user == null) //if the user doesn't exist, then return a new List,
                              //else there will be a null reference error
            {
                return new List<Note>(); //returning a new list
            }

            return await _context.Notes
                .Where(note => note.User.Id == user.Id) //where the user id linked to the note is equal to
                                                        //the user in the session's id
                .ToListAsync(); //returns a list to iterate over
                                //when displaying the notes
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note); //adds the note object into the database
            await _context.SaveChangesAsync(); //saves changes to the database
        }

        public async Task DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

    }
}
