using System.Diagnostics;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Genres
{
    public class Update
    {
        public class Command : IRequest<Result<GenreDto>>
        {
            public GenreCmdDto Genre { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GenreDto>>
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
                    RuleFor(x => x.Genre).SetValidator(new GenreCmdValidator());
                }
            }

            public async Task<Result<GenreDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var genre = await _ctx.Genres.FindAsync(new object[] { req.Genre.Gn_ID }, cancellationToken: cancellationToken);

                if (genre == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                _mpr.Map(req.Genre, genre);

                genre.Gn_Modifier = user.Id;
                genre.Gn_ModifyOn = DateTime.UtcNow;

                _mpr.Map(req.Genre, genre);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<GenreDto>.Failure("Failed to update Genre");
                return Result<GenreDto>.Success(_mpr.Map<GenreDto>(genre)); 
            }
        }
    }
}