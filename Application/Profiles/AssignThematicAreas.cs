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
    public class AssignThematicAreas
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
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                if (req.Ids != null && req.Ids.Ids != null && req.Ids.Ids.Count > 0)
                {
                    

                    foreach (var customerThematicArea in user.ThematicAreas.Where(s => !req.Ids.Ids.Contains(s.Th_ID)))
                    {
                        customerThematicArea.CsTh_Active = false;
                        customerThematicArea.CsTh_Modifier = user.Id;
                        customerThematicArea.CsTh_ModifyOn = dateTime;
                    }

                    foreach (Guid id in req.Ids.Ids)
                    {
                        var thematicArea = user.ThematicAreas.Where(s => s.Th_ID == id).FirstOrDefault();
                        if (thematicArea == null)
                        {
                            var customerThematicArea = new CustomerThematicArea
                            {
                                Th_ID = id,
                                CsTh_Ordinal = 0,
                                CsTh_Active = true,
                                CsTh_Creator = user.Id,
                                CsTh_CreatedOn = dateTime
                            };
                            user.ThematicAreas.Add(customerThematicArea);
                        }
                        else
                        {
                            thematicArea.CsTh_Ordinal = 0;
                            if (!thematicArea.CsTh_Active) thematicArea.CsTh_Active = true;
                            thematicArea.CsTh_Modifier = user.Id;
                            thematicArea.CsTh_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerThematicArea in user.ThematicAreas)
                    {
                        customerThematicArea.CsTh_Active = false;
                        customerThematicArea.CsTh_Modifier = user.Id;
                        customerThematicArea.CsTh_ModifyOn = dateTime;
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