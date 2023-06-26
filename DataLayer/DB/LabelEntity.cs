using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DB
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelID { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("Notes")]
        public long noteID { get; set; }
   
        public virtual NotesEntity note { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }
    
        public virtual UserEntity User { get; set; }
    }
}
