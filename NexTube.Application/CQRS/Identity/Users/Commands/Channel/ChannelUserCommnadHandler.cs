//// SubscriptionUserCommandHandler.cs
//using System.Threading;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using NexTube.Application.Common.DbContexts;
//using NexTube.Application.Common.Interfaces;
//using NexTube.Application.CQRS.Files.Photos.Commands.UploadSquarePhoto;
//using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
//using NexTube.Application.Subscriptions.Commands;
//using NexTube.Domain.Entities;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//namespace NexTube.Application.Subscriptions.Handlers
//{
//    public class ChannelUserCommnadHandler : IRequestHandler<bool>
//    {
//        private readonly IApplicationDbContext _iApplicationDbContext;
//        private readonly IJwtService _jwtService;
//        private readonly IMediator _mediator;

//        public ChannelUserCommnadHandler(IApplicationDbContext _iApplicationDbContext, IJwtService jwtService)
//        {

//            _jwtService = jwtService;

//        }

//        public async Task<bool> Handle(bool request, CancellationToken cancellationToken)
//        {
             

//            // Create a new channel entity
//            var channel = new ChannelEntity
//            {
//                Name = "",
//                Nickname = "",
//                Description = "",
//                User = null
//            };

//            // Save the channel to the database
//            await _iApplicationDbContext.Channel.AddAsync(channel);
//            await _iApplicationDbContext.SaveChangesAsync(cancellationToken);


//            return true;
//        }

//    }
