using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Domain.Entities;
using System.ComponentModel.Design;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment {
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit> {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCommentCommandHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken) {
            var comment = await _dbContext.VideoComments
                .Include(vc => vc.Creator)
                .Where(vc => vc.Id == request.CommentId)
                .FirstOrDefaultAsync();

            if (comment is null)
                throw new NotFoundException(request.CommentId.ToString(), nameof(VideoCommentEntity));

            if (comment?.Creator?.Id != request?.Requester?.Id)
                throw new ForbiddenAccessException();

            _dbContext.VideoComments.Remove(comment);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
