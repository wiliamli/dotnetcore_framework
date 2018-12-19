using System.Collections.Generic;
using Jwell.Application.Dtos;
using Jwell.Application.Params;
using Jwell.Framework.Paging;
using Jwell.Repository.Repositories;

namespace Jwell.Application.Service
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IEnumerable<CityDto> GetCitys()
        {
           return _cityRepository.GetCities().ToDtos();
        }

        public PageResult<CityDto> GetCitiesPageResult(SearchCityParam searchCityParam,out int count)
        {
            return _cityRepository.QueryPageResult(searchCityParam.PageIndex, searchCityParam.PageSize, out count).GetPageResult();
        }

        public IEnumerable<CityDto> GetCitiesPaged(SearchCityParam searchCityParam, out int count)
        {
            return _cityRepository.QueryPaged(searchCityParam.PageIndex, searchCityParam.PageSize, out count, new
            {
                Name = searchCityParam.Name
            }).ToDtos();
        }

        public IEnumerable<CityDto> GetCitiesByIds(long[] cityIds)
        {
            return _cityRepository.GetCitiesByIds(cityIds).ToDtos();
        }

        public IEnumerable<CityDto> GetCitiesByName(string name)
        {
            return _cityRepository.GetCitiesByName(name).ToDtos();
        }
    }
}
