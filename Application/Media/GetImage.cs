using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Media
{
    public class GetImage
    {
        public class Query : IRequest<Result<Medium>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Medium>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mpr = mpr;
                _ctx = ctx;
            }

            public async Task<Result<Medium>> Handle(Query req, CancellationToken cancellationToken)
            {
                var medium = await _ctx.Media
                    .FirstOrDefaultAsync(s => s.Md_ID == req.Id, cancellationToken: cancellationToken);

                return Result<Medium>.Success(medium);
            }

            //public async Task<Result<Medium>> Handle(Query req, CancellationToken cancellationToken)
            //{
            //    try
            //    {
            //        // Query the SQL Server database to get the DownloadURL and Md_FileType corresponding to the Md_ID
            //        var mediumData = await _ctx.Media
            //                                .Where(s => s.Md_ID == req.Id)
            //                                .Select(s => new { s.DownloadURL, s.Md_FileType })
            //                                .FirstOrDefaultAsync(cancellationToken);

            //        if (mediumData != null)
            //        {
            //            // Return the Medium object with the DownloadURL and Md_FileType
            //            var medium = new Medium
            //            {
            //                Md_ID = req.Id,
            //                DownloadURL = mediumData.DownloadURL,
            //                Md_FileType = mediumData.Md_FileType
            //                // Add other properties if needed
            //            };

            //            return Result<Medium>.Success(medium);
            //        }
            //        else
            //        {
            //            return Result<Medium>.Failure("Image not found."); // Handle the case where DownloadURL is null or Md_ID not found
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log or handle exceptions
            //        return Result<Medium>.Failure($"An error occurred: {ex.Message}");
            //    }
            //}

        }
    }
}

