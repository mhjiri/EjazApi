using System.Linq;
using Application.Banners.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Banners
{
    public class GetByLocation
    {
        public class Query : IRequest<Result<BannerDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BannerDto>>
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

            public async Task<Result<BannerDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users
                    .Include(i => i.Categories)
                    .Include(j => j.ThematicAreas)
                    .Include(k => k.Genres)
                    .Include(m => m.Tags)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (user == null) return null;

                DateTime today = DateTime.Now.Date;

                int age = (user.Us_DOB != DateTime.MinValue) ? Common.GetAge(user.Us_DOB) : 0;
                List<Guid> userCategories = user.Categories.Where(s => s.CsCt_Active).Select(s => s.Ct_ID).ToList();
                List<Guid> userThematicAreas = user.ThematicAreas.Where(s => s.CsTh_Active).Select(s => s.Th_ID).ToList();
                List<Guid> userGenres = user.Genres.Where(s => s.CsGn_Active).Select(s => s.Gn_ID).ToList();
                List<Guid> userTags = user.Tags.Where(s => s.CsTg_Active).Select(s => s.Tg_ID).ToList();

                var userGroups = await _ctx.Groups
                    .Include(i => i.Categories)
                    .Include(j => j.ThematicAreas)
                    .Include(k => k.Genres)
                    .Include(m => m.Tags)
                    .Where(s => s.Gr_Active
                    && (user.Us_language == null || (s.Gr_Language.ToLower() == "no preference" || s.Gr_Language.ToLower() == user.Us_language.ToLower()))
                    && (user.Us_Gender == null || (s.Gr_Gender.ToLower() == "no preference" || s.Gr_Gender.ToLower() == user.Us_Gender.ToLower()))
                    && (age == 0 || (s.Gr_AgeFrom <= age) && (s.Gr_AgeTill == 0 || s.Gr_AgeTill > age))
                    && (s.Categories.Count() == 0 || (userCategories.Count > 0 && s.Categories.Where(z=> z.GrCt_Active).Select(z=> z.Ct_ID).Any(z => userCategories.Contains(z))))
                    && (s.ThematicAreas.Count() == 0 || (userThematicAreas.Count > 0 && s.ThematicAreas.Where(z => z.GrTh_Active).Select(z => z.Th_ID).Any(z => userThematicAreas.Contains(z))))
                    && (s.Genres.Count() == 0 || (userGenres.Count > 0 && s.Genres.Where(z => z.GrGn_Active).Select(z => z.Gn_ID).Any(z => userGenres.Contains(z))))
                    && (s.Tags.Count() == 0 || (userTags.Count > 0 && s.Tags.Where(z => z.GrTg_Active).Select(z => z.Tg_ID).Any(z => userTags.Contains(z))))

                ).ToListAsync();

                var banner = await _ctx.Banners.Include(i => i.Group).Where(s => s.Bn_Active
                                && (s.Bn_PublishFrom == null || s.Bn_PublishFrom < today)
                                && (s.Bn_PublishTill == null || s.Bn_PublishTill >= today)
                                && s.Bl_ID == req.Id
                                && userGroups.Select(z => z.Gr_ID).Contains(s.Gr_ID)
                                ).OrderBy(s => s.Bn_CreatedOn)
                    .ProjectTo<BannerDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .LastOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<BannerDto>.Success(banner);
            }
        }
    }
}

