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
using Firebase.Storage;

namespace Application.Media
{
    public class Create
    {
        public class Command : IRequest<Result<MediumDto>>
        {
            public MediumFileCmdDto Medium { get; set; }
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
                    RuleFor(x => x.Medium).SetValidator(new MediumFileCmdValidator());
                }
            }

            public async Task<Result<MediumDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                        s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                    // Assuming req.Medium.Md_URL contains the file from the client
                    var file = req.Medium.Md_URL;

                    // Initialize Firebase Storage
                    var firebaseStorageBucket = "ejaz-290e6.appspot.com";
                    var fireBaseStorage = new FirebaseStorage(firebaseStorageBucket);

                    // Generate a unique storage path for the file
                    var storagePath = $"files/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                    // Upload the file to Firebase Storage
                    using (var fileStream = file.OpenReadStream())
                    {
                        var downloadUrl = await fireBaseStorage.Child(storagePath).PutAsync(fileStream);

                        // Create a new Medium object
                        var medium = new Medium
                        {
                            Md_URL = downloadUrl,
                            Md_Title = req.Medium.Md_Title,
                            Md_Title_Ar = req.Medium.Md_Title_Ar,
                            Md_Extension = Path.GetExtension(file.FileName),
                            Md_FileName = file.FileName,
                            Md_FileType = "application/octet-stream",
                            Md_Creator = user.Id,
                            Md_Active = true
                        };

                        // Add the Medium object to the database context
                        _ctx.Media.Add(medium);

                        // Save changes to the database
                        var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                        if (!result)
                            return Result<MediumDto>.Failure("Failed to update Medium");
                        else
                            return Result<MediumDto>.Success(_mpr.Map<MediumDto>(medium));
                    }
                }
                catch (Exception ex)
                {
                    return Result<MediumDto>.Failure($"Error: {ex.Message}");
                }

            }
        }
    }
}