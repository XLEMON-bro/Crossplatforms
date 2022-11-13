using CP_LAB_5.Data;
using CP_LAB_5.Models.ViewModels;

namespace CP_LAB_5.Mapper
{
    public class CustomMapper : ICustomMapper
    {
        public User MapToUser(RegistrationViewModel userRegistrationViewModel)
        {
            return new User
            {
                UserName = userRegistrationViewModel.UserName,
                FIO = userRegistrationViewModel.FIO,
                Email = userRegistrationViewModel.Email,
                Password = userRegistrationViewModel.Password,
                Phone = userRegistrationViewModel.Phone
            };
        }
    }
}
