using System;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Profiles
{
    public class AssignTags
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ListGuidDto Ids { get; set; }
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
                

                var user = await _ctx.Users
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                if (req.Ids != null && req.Ids.Ids != null && req.Ids.Ids.Count > 0)
                {
                    

                    foreach (var customerTag in user.Tags.Where(s => !req.Ids.Ids.Contains(s.Tg_ID)))
                    {
                        customerTag.CsTg_Active = false;
                        customerTag.CsTg_Modifier = user.Id;
                        customerTag.CsTg_ModifyOn = dateTime;
                    }

                    foreach (Guid id in req.Ids.Ids)
                    {
                        var tag = user.Tags.Where(s => s.Tg_ID == id).FirstOrDefault();
                        if (tag == null)
                        {
                            var customerTag = new CustomerTag
                            {
                                Tg_ID = id,
                                CsTg_Ordinal = 0,
                                CsTg_Active = true,
                                CsTg_Creator = user.Id,
                                CsTg_CreatedOn = dateTime
                            };
                            user.Tags.Add(customerTag);
                        }
                        else
                        {
                            tag.CsTg_Ordinal = 0;
                            if (!tag.CsTg_Active) tag.CsTg_Active = true;
                            tag.CsTg_Modifier = user.Id;
                            tag.CsTg_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerTag in user.Tags)
                    {
                        customerTag.CsTg_Active = false;
                        customerTag.CsTg_Modifier = user.Id;
                        customerTag.CsTg_ModifyOn = dateTime;
                    }
                }


                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to assign Thematic Areas to user");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}