using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Repository.Repositories
{
    public interface ICityRepository: IRepository
    {
        IEnumerable<City> GetCities();

        PageResult<City> QueryPageResult(int pageIndex,int pageSize, out int count, object parameters = null);

        IEnumerable<City> QueryPaged(int pageIndex, int pageSize, out int count, object parameters = null);


        IEnumerable<City> GetCitiesByIds(long[] ids);

        IEnumerable<City> GetCitiesByName(string name);
    }
}
