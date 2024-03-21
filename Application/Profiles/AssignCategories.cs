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
    public class AssignCategories
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
                //var categories = _ctx.Categories.Where(s => req.Ids.Ids.Contains(s.Ct_ID));

                //if (categories == null || !categories.Any()) return null;

                var user = await _ctx.Users
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                if (req.Ids != null && req.Ids.Ids != null && req.Ids.Ids.Count() > 0)
                {
                    

                    foreach (var customerCategory in user.Categories.Where(s => !req.Ids.Ids.Contains(s.Ct_ID)))
                    {
                        customerCategory.CsCt_Active = false;
                        customerCategory.CsCt_Modifier = user.Id;
                        customerCategory.CsCt_ModifyOn = dateTime;
                    }

                    foreach (Guid id in req.Ids.Ids)
                    {
                        var category = user.Categories.Where(s => s.Ct_ID == id).FirstOrDefault();
                        if (category == null)
                        {
                            var customerCategory = new CustomerCategory
                            {
                                Ct_ID = id,
                                CsCt_Ordinal = 0,
                                CsCt_Active = true,
                                CsCt_Creator = user.Id,
                                CsCt_CreatedOn = dateTime
                            };
                            user.Categories.Add(customerCategory);
                        }
                        else
                        {
                            category.CsCt_Ordinal = 0;
                            if (!category.CsCt_Active) category.CsCt_Active = true;
                            category.CsCt_Modifier = user.Id;
                            category.CsCt_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerCategory in user.Categories)
                    {
                        customerCategory.CsCt_Active = false;
                        customerCategory.CsCt_Modifier = user.Id;
                        customerCategory.CsCt_ModifyOn = dateTime;
                    }
                }


                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to assign categories to user");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}