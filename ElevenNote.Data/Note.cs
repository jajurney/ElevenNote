using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTimeOffset CreateUtc { get; set; }
       // public int CategoryId { get; set; }

       // [ForeignKey(nameof(CategoryId))]
       // public virtual Categories Categories { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        [DefaultValue(false)]
        public bool IsStarred { get; set; }
    }
}
