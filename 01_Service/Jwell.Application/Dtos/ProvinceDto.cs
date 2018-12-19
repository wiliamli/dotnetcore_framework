using Jwell.Domian.Entities;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jwell.Application.Dtos
{
    public class ProvinceDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        private DateTime createdDate = DateTime.Now;
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                if (value != new DateTime(1, 1, 1))
                {
                    createdDate = value;
                }
            }
        }

        private DateTime modifiedDate = DateTime.Now;
        public DateTime ModifiedDate {
            get
            {
                return modifiedDate;
            }
            set
            {
                if (value != new DateTime(1, 1, 1))
                {
                    modifiedDate = value;
                }
            }
        }
    }

    public static class ProvinceDtoExt
    {
        public static ProvinceDto ToDto(this Province entity)
        {
            ProvinceDto dto = null;
            if (entity != null)
            {
                dto = new ProvinceDto
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    CountryCode = entity.CountryCode,
                    CreatedDate = entity.CreatedDate,
                    ModifiedDate = entity.ModifiedDate
                };
            }
            return dto;
        }

        public static IEnumerable<ProvinceDto> ToDtos(this IEnumerable<Province> entities)
        {
            IEnumerable<ProvinceDto> dtos = null;
            if (entities != null)
            {
                dtos = entities.Select(m => new ProvinceDto()
                {
                    CountryCode = m.CountryCode,
                    ID = m.ID,
                    Name = m.Name,
                    CreatedDate = m.CreatedDate,
                    ModifiedDate = m.ModifiedDate
                });
            }
            return dtos;
        }

        public static PageResult<ProvinceDto> GetPageResult(this PageResult<Province> entities)
        {
            var queryDto = from q in entities.Pager
                           select new ProvinceDto
                           {
                               Name = q.Name,
                               CountryCode = q.CountryCode,
                               ID = q.ID,
                               CreatedDate = q.CreatedDate,
                               ModifiedDate = q.ModifiedDate
                           };
            return new PageResult<ProvinceDto>(queryDto, entities.PageIndex, entities.PageSize, entities.TotalCount);
        }

        public static Province ToEntity(this ProvinceDto dto)
        {
            Province entity = null;
            if (dto != null)
            {
                entity = new Province
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    CountryCode = dto.CountryCode,
                    CreatedDate = dto.CreatedDate,
                    ModifiedDate = dto.ModifiedDate
                };
            }
            return entity;
        }

        public static IEnumerable<Province> ToEntities(this IEnumerable<ProvinceDto> dtos)
        {
            IEnumerable<Province> entities = null;
            if (dtos != null)
            {
                entities = dtos.Select(m => new Province()
                {
                    ID = m.ID,
                    Name = m.Name,
                    CountryCode = m.CountryCode,
                    CreatedDate = m.CreatedDate,
                    ModifiedDate = m.ModifiedDate
                });
            }
            return entities;
        }
    }
}
