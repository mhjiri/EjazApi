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
    public class GetAuthorData
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

                int totalAuthors = await _ctx.Authors.Where(s => s.At_Active == true).CountAsync();
                int thisMonthAuthors = await _ctx.Authors.Where(s => s.At_Active == true && s.At_CreatedOn > new DateTime(dateTime.Year, dateTime.Month, 1)).CountAsync();
                int todayAuthors = await _ctx.Authors.Where(s => s.At_Active == true && s.At_CreatedOn > new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)).CountAsync();
                List<DataDto> dataDtos = await _ctx.Authors.Where(s => s.At_Active == true && s.At_CreatedOn > new DateTime(dateTime.Year - 1, dateTime.Month, 1) && s.At_CreatedOn < new DateTime(dateTime.Year, dateTime.Month, 1)).GroupBy(s => new {Year = s.At_CreatedOn.Year, Month = s.At_CreatedOn.Month }).Select(s => new DataDto { Year=s.Key.Year, Month=s.Key.Month, Value=s.Count()  }).OrderBy(s =>s.Year).OrderBy(s => s.Month).ToListAsync();

                List<string> categories = new List<string>(); List<int> data = new List<int>(); int lastMonth = 0;

                foreach(DataDto dataDto in dataDtos)
                {
                    if((lastMonth!=0 && dataDto.Month!=lastMonth+1) || (lastMonth==12 && dataDto.Month!=1))
                    {
                        if (lastMonth == 12) lastMonth = 0;
                        if (dataDto.Month > lastMonth) {
                            for (int i = lastMonth + 1; i < dataDto.Month; i++)
                            {
                                data.Add(0);
                                categories.Add(String.Format("{0}-{1}", Common.GetMonthText(i), dataDto.Year));
                            }
                        } else
                        {
                            for (int i = lastMonth + 1; i < 13; i++)
                            {
                                data.Add(0);
                                categories.Add(String.Format("{0}-{1}", Common.GetMonthText(i), dataDto.Year-1));
                            }

                            for (int i = 1; i < dataDto.Month; i++)
                            {
                                data.Add(0);
                                categories.Add(String.Format("{0}-{1}", Common.GetMonthText(i), dataDto.Year));
                            }
                        }

                    }

                    data.Add(dataDto.Value);
                    categories.Add(String.Format("{0}-{1}", Common.GetMonthText(dataDto.Month), dataDto.Year));
                    lastMonth = dataDto.Month;
                }

                ChartDto chart = new ChartDto()
                {
                    Value1 = string.Format("{0:#,0}", totalAuthors),
                    Value2 = string.Format("{0:#,0}", thisMonthAuthors),
                    Value3 = string.Format("{0:#,0}", todayAuthors),
                    MaxValue = data.Count != 0 ? data.Max() : 0,
                    MinValue = data.Count != 0 ? data.Min() : 0,
                    Data = data.ToArray(),
                    Categories = categories.ToArray()

                };
                return Result<ChartDto>.Success(chart);
            }
        }
    }
}

