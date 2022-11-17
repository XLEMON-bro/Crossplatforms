using System.ComponentModel.DataAnnotations;

namespace CP_LAB_5.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "FIO is required.")]
        public string FIO { get;set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%?=*&_~]).{8,16}", ErrorMessage = "At least 1 number, 1 char, 1 Upper Latter and 8-16 Length")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [RegularExpression(@"^(\+?38)(0\d{2}\d{3}\d{2}\d{2})$", ErrorMessage = "Not Valid! Ex: +380ХХХХХХХХХ")]
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
