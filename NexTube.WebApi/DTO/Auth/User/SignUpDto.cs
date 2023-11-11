﻿using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;

namespace NexTube.WebApi.DTO.Auth.User
{
    public class SignUpDto : IMapWith<CreateUserCommand> {
        public string Password { get; set; } = null!;
        public string? PasswordConfirm { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string  Nickname { get; set; }=null!;
       public string Description { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<SignUpDto, CreateUserCommand>()
                .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password))
                .ForMember(command => command.PasswordConfirm, opt => opt.MapFrom(dto => dto.PasswordConfirm))
                .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(command => command.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(command => command.LastName, opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(command => command.Nickname, opt => opt.MapFrom(dto => dto.Nickname))
                .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));

        }
    }
}
