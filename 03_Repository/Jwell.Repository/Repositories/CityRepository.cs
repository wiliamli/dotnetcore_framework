using System;
using System.Collections.Generic;
using System.Linq;
using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using Jwell.Repository.Context;

namespace Jwell.Repository.Repositories
{
    public class CityRepository : JwellDataBase<City>,ICityRepository
    {
        public override string DbName => "World";

        public IEnumerable<City> GetCities()
        {
            IEnumerable<City> list  = base.Query("getCitys");
            return list;
        }

        public IEnumerable<City> GetCitiesByIds(long[] cityIds)
        {
            return base.Query<City>("getCitiesByIds", new { ids = cityIds });
        }

        /// <summary>
        /// 返回分页后的集合数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<City> QueryPaged(int pageIndex, int pageSize, out int count, object parameters = null)
        {
            IEnumerable<City> list = base.QueryPaged("getCitys", pageIndex, pageSize, out count);
            return list;
        }

        /// <summary>
        /// 返回分页对象数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public PageResult<City> QueryPageResult(int pageIndex, int pageSize, out int count, object parameters)
        {
            PageResult<City> list = base.QueryPaged("getCitys", pageIndex, pageSize, out count).
                ToPageResult(pageIndex, pageSize, count);

            return list;
        }

        public IEnumerable<City> GetCitiesByName(string name)
        {
            return base.Query("getCitiesByName", new { name = $"%{name}%" });
        }
    }
}
