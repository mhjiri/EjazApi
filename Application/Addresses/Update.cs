using System.Diagnostics;
using Application.Core;
using Application.Addresses.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Addresses
{
    public class Update
    {
        public class Command : IRequest<Result<AddressDto>>
        {
            public Address Address { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AddressDto>>
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
                    RuleFor(x => x.Address).SetValidator(new AddressValidator());
                }
            }

            public async Task<Result<AddressDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var address = await _ctx.Addresses.FindAsync(new object[] { req.Address.Ad_ID }, cancellationToken: cancellationToken);

                if (address == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);

                req.Address.Ad_Modifier = user.Id;
                req.Address.Ad_ModifyOn = DateTime.UtcNow;

                if(address.Ad_IsDefault && (!req.Address.Ad_IsDefault || !req.Address.Ad_Active))
                {
                    var defaultAddress = await _ctx.Addresses.Where(s => s.Ad_Active).OrderByDescending(s => s.Ad_ModifyOn)
                        .ThenByDescending(s => s.Ad_CreatedOn).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    if (defaultAddress != null) defaultAddress.Ad_IsDefault = true;
                }

                _mpr.Map(req.Address, address);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<AddressDto>.Failure("Failed to update Address");
                return Result<AddressDto>.Success(_mpr.Map<AddressDto>(address)); 
            }
        }
    }
}