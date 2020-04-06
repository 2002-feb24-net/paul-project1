using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        public string Username {get; set;}

        [Required]
        public string Password {get; set;}
    }
}