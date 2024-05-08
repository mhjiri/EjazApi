using System.Diagnostics;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using Application.Media.Core;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using Firebase.Storage;
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
            private readonly FirebaseStorage _firebaseStorage;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor, FirebaseStorage firebaseStorage)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
                _firebaseStorage = firebaseStorage;
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

                var url = req.Medium.Md_URL;

                if (!string.IsNullOrEmpty(url))
                {
                    medium.Md_URL = url;
                    medium.Md_Extension = Path.GetExtension(url);
                    medium.Md_FileName = Path.GetFileName(url);
                    medium.Md_FileType = "application/octet-stream";
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