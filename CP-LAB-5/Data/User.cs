using System.ComponentModel.DataAnnotations;

namespace CP_LAB_5.Data
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(55)]
        public string UserName { get; set; }

        [MaxLength(505)]
        public string FIO { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
