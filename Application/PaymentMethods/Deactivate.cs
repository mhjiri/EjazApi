using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.PaymentMethods
{
    public class Deactivate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
            }

            public async Task<Result<Unit>> Handle(Command req, CancellationToken cancellationToken)
            {
                var paymentMethod = await _ctx.PaymentMethods.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (paymentMethod == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);


                paymentMethod.Py_Modifier = user.Id;
                paymentMethod.Py_ModifyOn = DateTime.UtcNow;
                paymentMethod.Py_Active = false;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate PaymentMethod");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}