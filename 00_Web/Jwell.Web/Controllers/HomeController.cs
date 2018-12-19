using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Jwell.Framework.Excel;
using Jwell.Application.Params;
using Jwell.Application.Service;
using Jwell.Web.Controllers.Base;
using Jwell.Web.Models;

namespace Jwell.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : BaseController
    {
        private readonly ICityService _cityService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityService"></param>
        public HomeController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="searchCityParam"></param>
        /// <returns></returns>
        public void Export(SearchCityParam searchCityParam)
        {
            string fileName = "fileName.xlsx";
            byte[] content = _cityService.GetCitiesPaged(searchCityParam, out int count).ToExcelContent(fileName);

            DownloadFile(fileName, content);
        }
    }
}
