using System;
using System.Collections.Generic;
using System.Text;
using Jwell.Application.Dtos;
using Jwell.Repository.Repositories;

namespace Jwell.Application.Service
{
    public class ProvinceService : IProvinceService
    {
        private IProvinceRepository _provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public long Add(ProvinceDto dto)
        {
            return _provinceRepository.Add(dto.ToEntity());
        }

        public long AddList(IEnumerable<ProvinceDto> dtos)
        {
            return _provinceRepository.AddList(dtos.ToEntities());
        }

        public long ExecSql(ProvinceDto dto)
        {
            return _provinceRepository.ExecSql("addProvince",dto.ToEntity());
        }

        public bool Modify(ProvinceDto dto)
        {
            return _provinceRepository.Modify(dto.ToEntity());
        }

        public bool DeleteProvince(ProvinceDto dto)
        {
            return _provinceRepository.DeleteProvince(dto.ToEntity());
        }

        public ProvinceDto GetProvinceById(long id)
        {
            return _provinceRepository.GetProvinceById(id).ToDto();
        }

        public long TotalCount()
        {
            return _provinceRepository.TotalCount();
        }
    }
}
