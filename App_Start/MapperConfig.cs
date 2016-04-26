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
                m.Email = p.email;
            });

            Mapper.CreateMap<User, EditProfileViewModel>().AfterMap((p, m) =>
            {
                m.Id = p.id;
                m.FirstName = p.firstName;
                m.LastName = p.lastName;
                m.PhoneNomber = p.phoneNumber;
                m.Email = p.email;
            });

            Mapper.CreateMap<EditProfileViewModel, User>().AfterMap((p, m) =>
            {
                m.firstName = p.FirstName;
                m.lastName = p.LastName;
                m.email = p.Email;
                m.phoneNumber = p.PhoneNomber;
            });

            Mapper.CreateMap<Services, ServicePageViewModel>().AfterMap((p, m) =>
            {
                m.Id = p.id;
                m.ClientMax = p.clientMax;
                m.Description = p.description;
                m.Name = p.name;
                m.Price = p.price;
                m.Rating = p.rating;
                m.Type = p.type;
                m.IsActive = p.isActive;
                m.Prepaymant = p.prepaymant;
            });

            Mapper.CreateMap<Order, OrderViewModel>().AfterMap((p, m) =>
            {
                m.Id = p.id;
                m.Name = p.services.name;
                m.DateStart = p.dateStart;
                m.DateEnd = p.dateEnd;
                m.DateOrder = p.dateOrder;
                m.IsActive = p.isActive;
            });

            Mapper.CreateMap<ServicePhoto, PhotoViewModel>().AfterMap((p, m) =>
            {
                m.Name = p.photo.name;
                m.Date = p.photo.date;
            });

            Mapper.CreateMap<Services, ServicesViewModel>().AfterMap((p, m) =>
            {
                m.Type = p.type;
                m.Name = p.name;
                m.Prise = p.price;
                m.Rating = p.rating;
                m.IsActive = p.isActive;
            });
        }
    }
}