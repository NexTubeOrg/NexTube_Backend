// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.Queries
{
    public class GetSubscriptionListQueryValidation : AbstractValidator<GetSubscriptionListQuery>
    {
        public GetSubscriptionListQueryValidation()
        {
            RuleFor(dto => dto.SubscriptionUserTo)
                .NotEmpty().WithMessage("Ідентифікатор підписника не може бути порожнім.");

        }
    }
}