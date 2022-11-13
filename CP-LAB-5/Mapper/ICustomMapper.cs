using CP_LAB_5.Data;
using CP_LAB_5.Models.ViewModels;

namespace CP_LAB_5.Mapper
{
    public interface ICustomMapper
    {
        public User MapToUser(RegistrationViewModel userRegistrationViewModel);
    }
}
