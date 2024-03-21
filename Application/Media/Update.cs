using System.Diagnostics;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using Application.Media.Core;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Media
{
    public class Update
    {
        public class Command : IRequest<Result<MediumDto>>
        {
            public MediumCmdDto Medium { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediumDto>>
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
                    RuleFor(x => x.Medium).SetValidator(new MediumCmdValidator());
                }
            }

            public async Task<Result<MediumDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var medium = await _ctx.Media.FindAsync(new object[] { req.Medium.Md_ID }, cancellationToken: cancellationToken);

                if (medium == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var file = req.Medium.Md_Medium;

                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await memStream.CopyToAsync(stream, cancellationToken);
                        medium.Md_Medium = memStream.ToArray();
                    }

                    medium.Md_Extension = Path.GetExtension(file.FileName);
                    medium.Md_FileName = file.FileName;
                    medium.Md_FileType = file.ContentType;
                    medium.Md_Modifier = user.Id;
                    medium.Md_ModifyOn = DateTime.UtcNow;

                    var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<MediumDto>.Failure("Failed to update Medium");
                    return Result<MediumDto>.Success(_mpr.Map<MediumDto>(req.Medium));

                }
                else return Result<MediumDto>.Failure("Failed to update Medium");
            }
        }
    }
}