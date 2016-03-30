using AutoMapper;
using Camping.Core;
using Camping.ViewModels;

namespace Camping.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.CreateMap<User, RegisterViewModel>();
            Mapper.CreateMap<RegisterViewModel, User>().AfterMap((p, m) =>
            {
                m.firstName = p.FirstName;
                m.lastName = p.LastName;
                m.phoneNumber = p.PhoneNomber;
                m.email = p.Email;
            });


        }
    }
}