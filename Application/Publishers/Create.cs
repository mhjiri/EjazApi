using System.Diagnostics;
using Application.Core;
using Application.Publishers.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Publishers
{
    public class Create
    {
        public class Command : IRequest<Result<PublisherQryDto>>
        {
            public PublisherCmdDto Publisher { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PublisherQryDto>>
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
                    RuleFor(x => x.Publisher).SetValidator(new PublisherCmdValidator());
                }
            }

            public async Task<Result<PublisherQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Publisher publisher = _mpr.Map<Publisher>(req.Publisher);
                publisher.Pb_Creator = user.Id;

                publisher.Genres = new List<PublisherGenre>();
                if (req.Publisher.GenreItems != null)
                {
                    foreach (ItemDto genre in req.Publisher.GenreItems)
                    {
                        var publisherGenre = new PublisherGenre
                        {
                            Gn_ID = genre.It_ID,
                            PbGn_Ordinal = genre.It_Ordinal,
                            PbGn_Active = true,
                            PbGn_Creator = user.Id
                        };
                        publisher.Genres.Add(publisherGenre);
                    }
                }

                publisher.Cn_ID = new Guid("86bfef9f-dcb6-4cae-fe51-08db6f7c019e");
                _ctx.Publishers.Add(publisher);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PublisherQryDto>.Failure("Failed to create Publisher");

                publisher = await _ctx.Publishers
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Creator)
                    .Where(s => s.Pb_ID == publisher.Pb_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<PublisherQryDto>.Success(_mpr.Map<PublisherQryDto>(publisher)); 
            }
        }
    }
}