using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Jwell.Web.Controllers.Base
{
    /// <summary>
    /// MVC基类
    /// </summary>
    public class BaseController:Controller
    {
        /// <summary>
        /// 流文件下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileStream">文件流</param>
        protected void DownloadFile(string fileName, byte[] fileStream)
        {
            Response.ContentType = GetMimeType(fileName);
            Response.Headers.Add(@"Content-Disposition", $"attachment; filename={fileName}");
            Response.Headers.Add("Content-Length", fileStream.Length.ToString());
            Response.Body.Write(fileStream);
            Response.Body.Flush();
            Response.Body.Close();
        }

        #region
        /// <summary>
        /// 可扩展
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private string GetMimeType(string fileName)
        {
            string suffix = Path.GetExtension(fileName).Trim().ToLower();

            switch (suffix)
            {
                case ".html":
                case ".htm":
                    return "text/html";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".xml":
                    return "text/xml";
                case ".pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }
        #endregion
    }
}
