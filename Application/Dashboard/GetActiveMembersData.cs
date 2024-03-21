using Application.Profiles.Core;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Dashboard.Core;
using Application.Groups.Core;
using System.Linq;

namespace Application.Dashboard

{
    public class GetActiveMembersData
    {
        public class Query : IRequest<Result<ChartDto>>
        {
            
        }

        public class Handler : IRequestHandler<Query, Result<ChartDto>>
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

            public async Task<Result<ChartDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                DateTime dateTime = DateTime.Now;

                int totalActiveMembers = await _ctx.Users.Where(s => s.Us_Customer == true && !s.Us_Deleted && s.Us_SubscriptionExpiryDate != null && s.Us_SubscriptionExpiryDate >= DateTime.Now).CountAsync();
                int MonthlyActiveMembers = await _ctx.Users.Where(s => s.Us_Customer == true && !s.Us_Deleted && s.Us_SubscriptionExpiryDate != null && s.Us_SubscriptionExpiryDate >= DateTime.Now && s.Us_SubscriptionDays == 30).CountAsync();
                int YearlyActiveMembers = await _ctx.Users.Where(s => s.Us_Customer == true && !s.Us_Deleted && s.Us_SubscriptionExpiryDate != null && s.Us_SubscriptionExpiryDate >= DateTime.Now && s.Us_SubscriptionDays == 360).CountAsync();


                DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);
                DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
                List<DataDto> dataDtos = new List<DataDto>();
                for (int i = -12; i < 0; i++)
                {
                    DataDto dataDto = new DataDto
                    {
                        Value = await _ctx.Users.Where(s => s.Us_Customer == true && !s.Us_Deleted && s.Us_SubscriptionExpiryDate != null && s.Us_SubscriptionStartDate <= dateTime2.AddMonths(i) && s.Us_SubscriptionExpiryDate >= dateTime1.AddMonths(i)).CountAsync(),
                        Month = dateTime1.AddMonths(i).Month,
                        Year = dateTime1.AddMonths(i).Year
                    };

                    dataDtos.Add(dataDto);
                }

                ChartDto chart = new ChartDto()
                {
                    Value1 = string.Format("{0:#,0}", totalActiveMembers),
                    Value2 = string.Format("{0:#,0}", MonthlyActiveMembers),
                    Value3 = string.Format("{0:#,0}", YearlyActiveMembers),
                    MaxValue = dataDtos.Count != 0 ? dataDtos.Select(s => s.Value).Max() : 0,
                    MinValue = dataDtos.Count != 0 ? dataDtos.Select(s => s.Value).Min() : 0,
                    Data = dataDtos.Select(s => s.Value).ToArray(),
                    Categories = dataDtos.Select(s => String.Format("{0}-{1}", Common.GetMonthText(s.Month), s.Year)).ToArray()

                };
                return Result<ChartDto>.Success(chart);
            }
        }
    }
}

