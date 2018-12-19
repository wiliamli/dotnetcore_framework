using Jwell.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Repository.Repositories
{
    public interface IProvinceRepository : IRepository
    {
        long Add(Province entity);

        long AddList(IEnumerable<Province> entities);

        bool Modify(Province entity);

        long ExecSql(string name,Province entity);

        bool DeleteProvince(Province entity);

        Province GetProvinceById(long id);

        long TotalCount();
    }
}
