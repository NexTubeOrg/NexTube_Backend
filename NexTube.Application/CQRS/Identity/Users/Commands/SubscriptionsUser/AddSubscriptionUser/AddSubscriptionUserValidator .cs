﻿// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Handlers
{
    public class AddSubscriptionUserValidator : AbstractValidator<AddSubscriptionUserCommand>
    {
        public AddSubscriptionUserValidator()
        {
            RuleFor(dto => dto.Subscriber)
                .NotEmpty().WithMessage("Ідентифікатор підписника не може бути порожнім.");

        }
    }
}
