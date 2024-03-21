using System.Diagnostics;   
using Application.Core;
using Application.PaymentMethods.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.PaymentMethods
{
    public class Update
    {
        public class Command : IRequest<Result<PaymentMethodDto>>
        {
            public PaymentMethodCmdDto PaymentMethod { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PaymentMethodDto>>
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
                    RuleFor(x => x.PaymentMethod).SetValidator(new PaymentMethodCmdValidator());
                }
            }

            public async Task<Result<PaymentMethodDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var paymentMethod = await _ctx.PaymentMethods.Where(s => s.Py_ID == req.PaymentMethod.Py_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (paymentMethod == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.PaymentMethod, paymentMethod);

                paymentMethod.Py_Modifier = user.Id;
                paymentMethod.Py_ModifyOn = dateTime;
                

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PaymentMethodDto>.Failure("Failed to update PaymentMethod");
                return Result<PaymentMethodDto>.Success(_mpr.Map<PaymentMethodDto>(paymentMethod));
            }
        }
    }
}