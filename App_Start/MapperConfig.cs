using System;
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
                m.IdService = p.id_service;
                m.TotalPrice = p.totalPrice;
                m.ClientsAmount = p.clientsAmount;
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
                m.IdService = p.id;
                m.Type = p.type;
                m.Name = p.name;
                m.Prise = p.price;
                m.Rating = p.rating;
                m.IsActive = p.isActive;
            });

            Mapper.CreateMap<Comments, CommentViewModel>().AfterMap((p, m) =>
            {
                m.Id = p.id;
                m.IdUser = p.id_user;
                m.IdService = p.id_service;
                m.UserName = p.user.firstName + " " + p.user.lastName;
                m.Description = p.commentText;
                m.PhotoUser = p.user.photo;
                m.Date = p.dateOfComment;
            });

            Mapper.CreateMap<ServicePageViewModel, Comments>().AfterMap((p, m) =>
            {
                m.id_service = p.Id;
                m.id_user = p.IdUserInSystem;
                m.commentText = p.NewComment;
                m.dateOfComment = DateTime.Now;
            });

            Mapper.CreateMap<ServicePageViewModel, Order>().AfterMap((p, m) =>
            {
                m.id_user = p.IdUserInSystem;
                m.id_service = p.Id;
                m.dateOrder = DateTime.Now;
                m.dateStart = Convert.ToDateTime(p.NewOrder.DateStart);
                m.dateEnd = Convert.ToDateTime(p.NewOrder.DateEnd);
                m.clientsAmount = Convert.ToInt64(p.NewOrder.ClientsAmount);
                m.totalPrice = p.NewOrder.ClientsAmount*p.Price;
                m.isActive = true;
            });

            Mapper.CreateMap<AddServiceViewModel, Services>().AfterMap((p, m) =>
            {
                m.type = p.Type;
                m.name = p.Name;
                m.rating = 4;
                m.clientMax = p.ClientMax;
                m.price = p.Price;
                m.description = p.Description;
                m.prepaymant = p.Prepaymant;
                m.isActive = true;
            });
        }
    }
}