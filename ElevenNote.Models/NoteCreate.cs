using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models
{
    public class NoteCreate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }

    }
}
