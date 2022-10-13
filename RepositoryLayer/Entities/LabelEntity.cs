using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }

        [ForeignKey("NoteTable")]
        public long NoteId { get; set; }
        //public virtual UserEntity User { get; set; }
        //public virtual NoteEntity Note { get; set; }
    }
}
