using Jwell.Application.Dtos;
using Jwell.Application.Params;
using Jwell.Framework.Paging;
using System.Collections.Generic;

namespace Jwell.Application.Service
{
    public interface ICityService : IApplicationService
    {
        IEnumerable<CityDto> GetCitys();

        IEnumerable<CityDto> GetCitiesPaged(SearchCityParam searchCityParam, out int count);

        PageResult<CityDto> GetCitiesPageResult(SearchCityParam searchCityParam, out int count);

        IEnumerable<CityDto> GetCitiesByIds(long[] cityIds);

        IEnumerable<CityDto> GetCitiesByName(string name);
    }
}
