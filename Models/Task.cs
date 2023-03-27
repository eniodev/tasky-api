using System;
using System.ComponentModel.DataAnnotations;

namespace tarefasbackend.Models
{
    public class Tasks
    {
        public Guid Id {get; set;}
        public Guid UserId {get; set;}
        [Required]
        public string Name {get; set;}
        public bool Done {get; set;} = false;

    }
}
