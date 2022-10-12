using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        public DateTime Edited { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }

        [ForeignKey("NoteTable")]
        public long NoteId { get; set; }
        //public virtual UserEntity User { get; set; }
        //public virtual NoteEntity Note { get; set; }
    }
}
