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
    public class AssignGenres
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
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                if (req.Ids != null && req.Ids.Ids != null && req.Ids.Ids.Count > 0)
                {
                    

                    foreach (var customerGenre in user.Genres.Where(s => !req.Ids.Ids.Contains(s.Gn_ID)))
                    {
                        customerGenre.CsGn_Active = false;
                        customerGenre.CsGn_Modifier = user.Id;
                        customerGenre.CsGn_ModifyOn = dateTime;
                    }

                    foreach (Guid id in req.Ids.Ids)
                    {
                        var genre = user.Genres.Where(s => s.Gn_ID == id).FirstOrDefault();
                        if (genre == null)
                        {
                            var customerGenre = new CustomerGenre
                            {
                                Gn_ID = id,
                                CsGn_Ordinal = 0,
                                CsGn_Active = true,
                                CsGn_Creator = user.Id,
                                CsGn_CreatedOn = dateTime
                            };
                            user.Genres.Add(customerGenre);
                        }
                        else
                        {
                            genre.CsGn_Ordinal = 0;
                            if (!genre.CsGn_Active) genre.CsGn_Active = true;
                            genre.CsGn_Modifier = user.Id;
                            genre.CsGn_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerGenre in user.Genres)
                    {
                        customerGenre.CsGn_Active = false;
                        customerGenre.CsGn_Modifier = user.Id;
                        customerGenre.CsGn_ModifyOn = dateTime;
                    }
                }


                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to assign Genres to user");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}