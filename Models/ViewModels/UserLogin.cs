using System.ComponentModel.DataAnnotations;

namespace tarefasbackend.Models.ViewModels{
    public class UserLogin
    {
        [Required]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}