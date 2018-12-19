using Jwell.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Application.Service
{
    public interface IProvinceService : IApplicationService
    {
        long Add(ProvinceDto dto);

        long AddList(IEnumerable<ProvinceDto> dtos);

        bool Modify(ProvinceDto dto);

        long ExecSql(ProvinceDto dto);

        bool DeleteProvince(ProvinceDto dto);

        ProvinceDto GetProvinceById(long id);

        long TotalCount();
    }
}
