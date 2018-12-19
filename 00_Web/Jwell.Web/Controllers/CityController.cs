using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Jwell.Application.Dtos;
using Jwell.Application.Service;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.Web.Controllers.Base;
using Jwell.Application.Params;
using Jwell.Configuration;

namespace Jwell.Web.Controllers
{
    /// <summary>
    /// 示例代码
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseApiController
    {
        private ICityService _cityService;

        private IJwellConfiguration _config;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="cityService">城市业务逻辑</param>
        /// <param name="config">统一配置</param>
        public CityController(ICityService cityService, IJwellConfiguration config)
        {
            _cityService = cityService;
            _config = config;
        }

        /// <summary>
        /// 城市分页
        /// </summary>
        /// <param name="pageParam">分页参数</param>
        /// <returns>城市分页信息</returns>
        [HttpPost("GetCityByPage")]
        public StandardJsonResult<PageResult<CityDto>> GetCityByPage(SearchCityParam pageParam)
        {
            return StandardAction(() =>
            _cityService.GetCitiesPageResult(pageParam, out int totalCount));
        }

        /// <summary>
        /// 获取所有城市信息
        /// </summary>
        /// <returns>城市信息</returns>
        [HttpGet("GetCitys")]
        public StandardJsonResult<IEnumerable<CityDto>> GetCitys()
        {
            //获取appsettings.json配置
            string logLevel = _config.GetAppSettingConfig("Logging:LogLevel:Default");
            //获取appCustomSettings.json配置
            string timeout = _config.GetCustomSettingConfig("timeout"); 
            //获取配置中心配置
            //string section= _config.GetConfig("timeout");

            return StandardAction(() => _cityService.GetCitys());
        }

        /// <summary>
        /// 根据城市ID获取城市信息
        /// </summary>
        /// <param name="ids">城市ID</param>
        /// <returns>城市信息</returns>
        [HttpGet("GetCitiesByIds")]
        public StandardJsonResult<IEnumerable<CityDto>> GetCitiesByIds(long[] ids)
        {
            return StandardAction(() => _cityService.GetCitiesByIds(ids));
        }

        /// <summary>
        /// 根据城市名称获取城市信息
        /// </summary>
        /// <param name="name">城市名称</param>
        /// <returns>城市信息</returns>
        [HttpGet("GetCitiesByName")]
        public StandardJsonResult<IEnumerable<CityDto>> GetCitiesByName(string name)
        {
            return StandardAction(() => _cityService.GetCitiesByName(name));
        }
    }
}