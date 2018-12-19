using System.Collections.Generic;
using Jwell.Application.Dtos;
using Jwell.Application.Service;
using Jwell.Framework.Mvc;
using Jwell.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jwell.Web.Controllers
{
    /// <summary>
    /// 示例代码
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvinceController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private IProvinceService _provinceService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="provinceService"></param>
        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        /// <summary>
        /// 新增省份
        /// </summary>
        /// <param name="province">省份信息</param>
        /// <returns>自增序列</returns>
        [HttpPost]
        public StandardJsonResult<long> Add(ProvinceDto province)
        {
            return StandardAction<long>(() =>
            {
                return _provinceService.Add(province);
            });
        }

        /// <summary>
        /// 批量新增省份
        /// </summary>
        /// <param name="provinces">省份集合</param>
        /// <returns>影响行数</returns>
        [HttpPost]
        public StandardJsonResult<long> AddList(IEnumerable<ProvinceDto> provinces)
        {
            List<ProvinceDto> provinceList = new List<ProvinceDto>
            {
                new ProvinceDto()
                {
                    CountryCode = "CN",
                    Name = "XIZANG"
                },
                new ProvinceDto()
                {
                    CountryCode = "CN",
                    Name = "QINGHAI"
                }
            };
            provinces = provinceList;
            return StandardAction<long>(() =>
            {
                return _provinceService.AddList(provinces);
            });
        }

        /// <summary>
        /// 执行sql，新增省份
        /// </summary>
        /// <param name="province">省份信息</param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult ExecSql(ProvinceDto province)
        {
            return StandardAction(() =>
            {
                _provinceService.ExecSql(province);
            });
        }

        /// <summary>
        /// 更新省份信息
        /// </summary>
        /// <param name="province">省份信息</param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult Update(ProvinceDto province)
        {
            return StandardAction(() =>
            {
                _provinceService.Modify(province);
            });
        }

        /// <summary>
        /// 删除省份
        /// </summary>
        /// <param name="province">省份信息</param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult Delete(ProvinceDto province)
        {
            return StandardAction(() =>
            {
                _provinceService.DeleteProvince(province);
            });
        }

        /// <summary>
        /// 根据Id获取省份
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<ProvinceDto> GetProvinceById(long id)
        {
            return StandardAction<ProvinceDto>(() =>
            {
                return _provinceService.GetProvinceById(id);
            });
        }

        /// <summary>
        /// 获取省份数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<long> GetProvinceCount()
        {
            return StandardAction<long>(() =>
            {
                return _provinceService.TotalCount();
            });
        }
    }
}
