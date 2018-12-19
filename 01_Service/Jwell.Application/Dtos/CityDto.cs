using Jwell.Domain.Entities;
using Jwell.Framework.Excel;
using Jwell.Framework.Paging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jwell.Application.Dtos
{
    /// <summary>
    /// Filter,Column 
    /// 这些Attribute用于Excel的导出，可选择使用
    /// </summary>
    //[Statistics(Name = "合计", Formula = "SUM", Columns = new[] { 6, 7 })]
    [Filter(FirstCol = 0, FirstRow = 0, LastCol = 5)]
    //[Freeze(ColSplit = 2, RowSplit = 1, LeftMostColumn = 2, TopRow = 1)]
    public class CityDto
    {
        [Column(Index = 0, Title = "ID", AllowMerge = false)]
        public int ID { get; set; }

        [Column(Index = 1, Title = "名称", AllowMerge = false)]
        public string Name { get; set; }

        [Column(Index = 2, Title = "国家编码", AllowMerge = false)]
        public string CountryCode { get; set; }

        [Column(Index = 3, Title = "地区", AllowMerge = false)]
        public string District { get; set; }

        [Column(Index = 4, Title = "人口", AllowMerge = false)]
        public int Population { get; set; }

        [Column(IsIgnored = true)] //Excel字段忽略
        [JsonIgnore] //SwaggerAPI 忽略
        public string IgonerName { get; set;}
    }

    public static class CityDtoExt
    {
        public static CityDto ToDto(this City city)
        {
            CityDto dto = null;
            if (city != null)
            {
                dto = new CityDto
                {
                    ID = city.ID,
                    Name = city.Name,
                    CountryCode = city.CountryCode,
                    District = city.District,
                    Population = city.Population
                };
            }
            return dto;
        }

        public static IEnumerable<CityDto> ToDtos(this IEnumerable<City> citys)
        {
            IEnumerable<CityDto> dtos = null;
            if (citys != null)
            {
                dtos = citys.Select(m => new CityDto()
                {
                    CountryCode = m.CountryCode,
                    District = m.District,
                    ID = m.ID,
                    Name = m.Name,
                    Population = m.Population

                });
            }
            return dtos;
        }

        public static PageResult<CityDto> GetPageResult(this PageResult<City> citys)
        {
            var queryDto = from q in citys.Pager
                           select new CityDto
                           {
                               Name = q.Name,
                               CountryCode = q.CountryCode,
                               District = q.District,
                               ID = q.ID,
                               Population = q.Population
                           };
            return new PageResult<CityDto>(queryDto, citys.PageIndex, citys.PageSize, citys.TotalCount);
        }

        public static City ToEntity(this CityDto dto)
        {
            City city = null;
            if (dto != null)
            {
                city = new City
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    CountryCode = dto.CountryCode,
                    District = dto.District,
                    Population = dto.Population
                };
            }
            return city;
        }
    }
}
