using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsCompleted { get; set; }

        // foreign key
        public int OwnerId { get; set; }
        // navigation property
        public Owner Owner { get; set; }
    }
}