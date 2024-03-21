using Application.Books.Core;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books
{
    public class SuggestBook
    {
        public class Command : IRequest<Result<SuggestBookQryDto>>
        {
            public SuggestBookCmd Book { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SuggestBookQryDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Book).SetValidator(new SuggestBookCmdValidator());
                }
            }

            public async Task<Result<SuggestBookQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Domain.SuggestBook book = _mpr.Map<Domain.SuggestBook>(req.Book);
                book.Bk_Creator = user?.Id;


                _ctx.SugggestBook.Add(book);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<SuggestBookQryDto>.Failure("Failed to suggest a Book");


                book = await _ctx.SugggestBook
                    .Where(s => s.Bk_ID == book.Bk_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<SuggestBookQryDto>.Success(_mpr.Map<SuggestBookQryDto>(book));
            }
        }
    }
}
