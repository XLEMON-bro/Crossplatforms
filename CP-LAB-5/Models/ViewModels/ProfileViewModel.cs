using System.ComponentModel.DataAnnotations;

namespace CP_LAB_5.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string FIO { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
