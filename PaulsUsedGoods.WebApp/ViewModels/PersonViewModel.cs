using System.ComponentModel.DataAnnotations;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class PersonViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int PersonId {get; set;}

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(128)]
        public string FirstName {get; set;}

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(128)]
        public string LastName {get; set;}

        [Display(Name = "Username")]
        [Required]
        [MaxLength(128)]
        public string Username {get; set;}

        [Display(Name = "Password")]
        [Required]
        [MaxLength(30)]
        public string Password {get; set;}

        [Display(Name = "Is Employee?")]
        public bool Employee {get; set;}

        [Display(Name = "Store Name")]
        [Required]
        public string StoreName {get; set;}
    }
}