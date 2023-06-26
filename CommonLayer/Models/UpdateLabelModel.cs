using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class UpdateLabelModel
    {

        public string LabelName { get; set; }

        public long NoteID { get; set; }

        public long userId { get; set; }    
    }
}
