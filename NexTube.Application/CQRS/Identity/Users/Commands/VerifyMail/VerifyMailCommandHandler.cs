using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Constants;

namespace NexTube.Application.CQRS.Identity.Users.Commands.VerifyMail
{
    public class VerifyMailCommandHandler : IRequestHandler<VerifyMailCommand, VerifyMailCommandResult>
    {

        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyMailCommandHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IMailService mailService, UserManager<ApplicationUser> userManager)
        {
            _identityService = identityService;
            _jwtService = jwtService;
            _mediator = mediator;
            _mailService = mailService; 
            _userManager = userManager;

        }
        public async Task<VerifyMailCommandResult> Handle(VerifyMailCommand request, CancellationToken cancellationToken)
        {
            var arePhrasesEqual = _jwtService.VerifyTokenWithSecretPhrase(request.VerificationToken, request.SecretPhrase);
            ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new NotFoundException(request.UserId.ToString(), nameof(ApplicationUser));

            if (arePhrasesEqual) {
                await _userManager.RemoveFromRoleAsync(user, Roles.Unverified);
                await _userManager.AddToRoleAsync(user, Roles.User);
                return new VerifyMailCommandResult() { Result = Result.Success(), Token = _jwtService.GenerateToken(request.UserId, _identityService.GetUserLookupAsync(request.UserId).Result.userLookup) };
            }
            else return new VerifyMailCommandResult() { Result = Result.Failure(new string[] { "Wrong secret phrase!" }) };
        }
        }
}
