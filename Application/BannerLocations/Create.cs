using System.Diagnostics;
using Application.Core;
using Application.BannerLocations.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BannerLocations
{
    public class Create
    {
        public class Command : IRequest<Result<BannerLocationDto>>
        {
            public BannerLocationCmdDto BannerLocation { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<BannerLocationDto>>
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
                    RuleFor(x => x.BannerLocation).SetValidator(new BannerLocationCmdValidator());
                }
            }

            public async Task<Result<BannerLocationDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                BannerLocation bannerLocation = _mpr.Map<BannerLocation>(req.BannerLocation);

                bannerLocation.Bl_Creator = user.Id;
                _ctx.BannerLocations.Add(bannerLocation);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BannerLocationDto>.Failure("Failed to create Banner Location");
                return Result<BannerLocationDto>.Success(_mpr.Map<BannerLocationDto>(bannerLocation)); 
            }
        }
    }
}