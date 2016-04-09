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

            Mapper.CreateMap<User, UserPageViewModel>().AfterMap((p, m) =>
            {
                m.IdUserPage = p.id;
                m.Name = p.firstName + " " + p.lastName;
                m.Phone = p.phoneNumber;
                m.Photo = p.photo;
            });

            Mapper.CreateMap<User, EditProfileViewModel>().AfterMap((p, m) =>
            {
                m.Id = p.id;
                m.FirstName = p.firstName;
                m.LastName = p.lastName;
                m.PhoneNomber = p.phoneNumber;
                m.Email = p.email;
            });
        }
    }
}