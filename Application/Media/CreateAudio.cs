using System.Diagnostics;
using System.IO;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using Application.Media.Core;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Media
{
    public class CreateAudio
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
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var file = req.Medium.Md_Medium;
                req.Medium.Md_Medium = null;
                var medium = new Medium(); //= _mpr.Map<Medium>(req.Medium);
                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        stream.CopyTo(memStream);
                        medium.Md_Medium = memStream.ToArray();
                    }
                    medium.Md_Title = req.Medium.Md_Title;
                    medium.Md_Title_Ar = req.Medium.Md_Title_Ar;
                    medium.Md_Extension = Path.GetExtension(file.FileName);
                    medium.Md_FileName = file.FileName;
                    medium.Md_FileType = file.ContentType;
                    medium.Md_Creator = user.Id;
                    _ctx.Media.Add(medium);

                    var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<MediumDto>.Failure("Failed to update Medium");
                    return Result<MediumDto>.Success(_mpr.Map<MediumDto>(medium));

                } else return Result<MediumDto>.Failure("Failed to update Medium");

                
            }
        }
    }
}