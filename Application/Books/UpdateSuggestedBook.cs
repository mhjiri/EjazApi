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
    public class UpdateSuggestedBook
    {
        public class Command : IRequest<Result<SuggestBookQryDto>>
        {
            public Guid BookId { get; set; }
            public UpdateSuggestBookCmd Book { get; set; }
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
                    RuleFor(x => x.Book).SetValidator(new UpdateSuggestBookCmdValidator());
                }
            }

            public async Task<Result<SuggestBookQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var bookToUpdate = await _ctx.SugggestBook.FindAsync(req.BookId);
                if (bookToUpdate == null)
                    return Result<SuggestBookQryDto>.Failure("Book not found");


                // Update non-null properties with the new values
                if (req.Book.Bk_Code != null)
                    bookToUpdate.Bk_Code = req.Book.Bk_Code;
                if (req.Book.Bk_Title != null)
                    bookToUpdate.Bk_Title = req.Book.Bk_Title;
                if (req.Book.Bk_Language != null)
                    bookToUpdate.Bk_Language = req.Book.Bk_Language;
                if (req.Book.Bk_Author != null)
                    bookToUpdate.Bk_Author = req.Book.Bk_Author;
                if (req.Book.Bk_Editor != null)
                    bookToUpdate.Bk_Editor = req.Book.Bk_Editor;
                if (req.Book.Bk_Comments != null)
                    bookToUpdate.Bk_Comments = req.Book.Bk_Comments;
                bookToUpdate.Bk_Creator = user.Us_Creator;

                // Save changes to the database
                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                    return Result<SuggestBookQryDto>.Failure("Failed to update the book");

                return Result<SuggestBookQryDto>.Success(_mpr.Map<SuggestBookQryDto>(bookToUpdate));
            }
        }
    }
}
