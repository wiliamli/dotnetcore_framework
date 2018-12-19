using Jwell.Domian.Entities;
using Jwell.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Repository.Repositories
{
    public class ProvinceRepository : JwellDataBase<Province>, IProvinceRepository
    {
        public override string DbName => "world";

        public long Add(Province entity)
        {
            return base.Add(entity, true);
        }

        public long AddList(IEnumerable<Province> entities)
        {
            return base.AddList(entities, true);
        }

        public long ExecSql(string name,Province entity)
        {
            return base.ExecuteWriteSql("addProvince",entity);
        }

        public bool Modify(Province entity)
        {
            return base.Update(entity,false);
        }

        public bool DeleteProvince(Province entity)
        {
            return base.Delete(entity);
        }

        public Province GetProvinceById(long id)
        {
            return base.GetById(id);
        }

        public long TotalCount()
        {
            return base.ExecuteScalar<long>("getProvincetCount");
        }
    }
}
