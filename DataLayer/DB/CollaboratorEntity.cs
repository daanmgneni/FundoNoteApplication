using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DB
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorID { get; set; }
        public string CollaboratedEmail { get; set; }

        [ForeignKey("Notes")]
        public long NoteID { get; set; }
        public virtual NotesEntity note { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }
        public virtual UserEntity user { get; set; }
    }
}
