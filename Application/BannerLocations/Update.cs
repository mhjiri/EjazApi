using System.Diagnostics;
using Application.Core;
using Application.BannerLocations.Core;
using Application.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BannerLocations
{
    public class Update
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
                var bannerLocation = await _ctx.BannerLocations.FindAsync(new object[] { req.BannerLocation.Bl_ID }, cancellationToken: cancellationToken);

                if (bannerLocation == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);

                _mpr.Map(req.BannerLocation, bannerLocation);

                bannerLocation.Bl_Modifier = user.Id;
                bannerLocation.Bl_ModifyOn = DateTime.UtcNow;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BannerLocationDto>.Failure("Failed to update BannerLocation");
                return Result<BannerLocationDto>.Success(_mpr.Map<BannerLocationDto>(bannerLocation)); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}