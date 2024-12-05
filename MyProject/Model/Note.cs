using System.Threading.Tasks;

namespace MyProject.Model
{
    public class Note
    {
        public int Id { get; set; }  
        public string NoteContent { get; set; }
        public User User { get; set; }
    }
}
