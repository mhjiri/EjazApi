using System;
using System.Diagnostics;
using Application.Core;
using Application.Groups.Core;
using Application.Interfaces;
using Application.Profiles.Core;
using Application.ThematicAreas.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class AssignAvatar
    {
        public class Command : IRequest<Result<ProfileDto>>
        {
            public ProfileAvatarDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ProfileDto>>
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

            public async Task<Result<ProfileDto>> Handle(Command req, CancellationToken cancellationToken)
            {

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (user == null) return null;

                DateTime dateTime = DateTime.UtcNow;

                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;
                user.Md_ID = req.Profile.Md_ID;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ProfileDto>.Failure("Failed to activate Group");
                return Result<ProfileDto>.Success(_mpr.Map<ProfileDto>(user));
            }
        }
    }
}