using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Videos.Queries.GetCommentsList {
    public class GetCommentsListQueryValidation : AbstractValidator<GetCommentsListQuery> {
        public GetCommentsListQueryValidation() {
            RuleFor(c => c.VideoId)
                .NotEmpty();

            RuleFor(c => c.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
