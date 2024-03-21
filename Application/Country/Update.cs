using System.Diagnostics;
using Application.Core;
using Application.Countries.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Countries
{
    public class Update
    {
        public class Command : IRequest<Result<CountryDto>>
        {
            public Country Country { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<CountryDto>>
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
                    RuleFor(x => x.Country).SetValidator(new CountryValidator());
                }
            }

            public async Task<Result<CountryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var country = await _ctx.Countries.FindAsync(new object[] { req.Country.Cn_ID }, cancellationToken: cancellationToken);

                if (country == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);

                req.Country.Cn_Modifier = user.Id;
                req.Country.Cn_ModifyOn = DateTime.UtcNow;

                _mpr.Map(req.Country, country);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<CountryDto>.Failure("Failed to update Country");
                return Result<CountryDto>.Success(_mpr.Map<CountryDto>(country)); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}