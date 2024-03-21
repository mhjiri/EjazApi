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
    public class Update
    {
        public class Command : IRequest<Result<PublisherDto>>
        {
            public PublisherCmdDto Publisher { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PublisherDto>>
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

            public async Task<Result<PublisherDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var publisher = await _ctx.Publishers.Include(i => i.Genres).ThenInclude(j => j.Genre).Where(s => s.Pb_ID == req.Publisher.Pb_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (publisher == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Publisher, publisher);
                publisher.Cn_ID = new Guid("86bfef9f-dcb6-4cae-fe51-08db6f7c019e");
                publisher.Pb_Modifier = user.Id;
                publisher.Pb_ModifyOn = dateTime;




                //Publisher Genre
                if (req.Publisher.Genres != null)
                {
                    foreach (var publisherGenre in publisher.Genres.Where(s => !req.Publisher.GenreItems.Select(z => z.It_ID).Contains(s.Gn_ID)))
                    {
                        publisherGenre.PbGn_Active = false;
                        publisherGenre.PbGn_Modifier = user.Id;
                        publisherGenre.PbGn_ModifiedOn = dateTime;
                    }

                    foreach (ItemDto item in req.Publisher.GenreItems)
                    {
                        var genre = publisher.Genres.Where(s => s.Gn_ID == item.It_ID).FirstOrDefault();
                        if (genre == null)
                        {
                            var publisherGenre = new PublisherGenre
                            {
                                Gn_ID = item.It_ID,
                                PbGn_Ordinal = item.It_Ordinal,
                                PbGn_Active = true,
                                PbGn_Creator = user.Id,
                                PbGn_CreatedOn = dateTime
                            };
                            publisher.Genres.Add(publisherGenre);
                        }
                        else
                        {
                            genre.PbGn_Ordinal = item.It_Ordinal;
                            if (!genre.PbGn_Active) genre.PbGn_Active = true;
                            genre.PbGn_Modifier = user.Id;
                            genre.PbGn_ModifiedOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var publisherGenre in publisher.Genres)
                    {
                        publisherGenre.PbGn_Active = false;
                        publisherGenre.PbGn_Modifier = user.Id;
                        publisherGenre.PbGn_ModifiedOn = dateTime;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PublisherDto>.Failure("Failed to update Publisher");
                return Result<PublisherDto>.Success(_mpr.Map<PublisherDto>(publisher));
            }
        }
    }
}