using System.Diagnostics;   
using Application.Core;
using Application.Banners.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Banners
{
    public class Update
    {
        public class Command : IRequest<Result<BannerDto>>
        {
            public BannerCmdDto Banner { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<BannerDto>>
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
                    RuleFor(x => x.Banner).SetValidator(new BannerCmdValidator());
                }
            }

            public async Task<Result<BannerDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var banner = await _ctx.Banners.FindAsync(new object[] { req.Banner.Bn_ID }, cancellationToken: cancellationToken);

                if (banner == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Banner, banner);

                banner.Bn_Modifier = user.Id;
                banner.Bn_ModifyOn = dateTime;


                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BannerDto>.Failure("Failed to update Banner");

                banner = await _ctx.Banners.Include(i => i.BannerLocation).Include(i => i.Group).Where(s => s.Bn_ID == banner.Bn_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);


                return Result<BannerDto>.Success(_mpr.Map<BannerDto>(banner));
            }
        }
    }
}