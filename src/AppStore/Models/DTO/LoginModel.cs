
using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string? userName{get;set;}
        [Required]
        public string? Password{get;set;}

    }
}