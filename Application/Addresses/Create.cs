using System.Diagnostics;
using Application.Core;
using Application.Addresses.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Addresses
{
    public class Create
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
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                req.Address.Ad_Creator = user.Id;
                _ctx.Addresses.Add(req.Address);

                if(req.Address.Ad_IsDefault)
                {
                    var addresses = _ctx.Addresses.Where(s => s.User==user && s.Ad_IsDefault);
                    foreach(var address in addresses)
                    {
                        address.Ad_IsDefault = false;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<AddressDto>.Failure("Failed to create Address");
                return Result<AddressDto>.Success(_mpr.Map<AddressDto>(req.Address)); 
            }
        }
    }
}