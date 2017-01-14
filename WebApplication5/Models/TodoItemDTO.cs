using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TodoItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsCompleted { get; set; }

        public string OwnerName { get; set; }
    }
}