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
using System.Security.Policy;
using Application.PaymentMehods.Core;

namespace Application.PaymentMethods
{
    public class Create
    {
        public class Command : IRequest<Result<PaymentMethodQryDto>>
        {
            public PaymentMethodCmdDto PaymentMethod { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PaymentMethodQryDto>>
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

            public async Task<Result<PaymentMethodQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                PaymentMethod paymentMethod = _mpr.Map<PaymentMethod>(req.PaymentMethod);
                paymentMethod.Py_Creator = user.Id;
                


                _ctx.PaymentMethods.Add(paymentMethod);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PaymentMethodQryDto>.Failure("Failed to create PaymentMethod");
                paymentMethod = await _ctx.PaymentMethods.Where(s => s.Py_ID == paymentMethod.Py_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return Result<PaymentMethodQryDto>.Success(_mpr.Map<PaymentMethodQryDto>(paymentMethod)); 
            }
        }
    }
}