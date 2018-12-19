using Jwell.Framework.XmlDoc;

namespace Microsoft.AspNetCore.Builder
{
    public static class JwellBuidlerExtensions
    {
        public static IApplicationBuilder UseXmlConfig(this IApplicationBuilder app,string path)
        {
            XmlHelper.GetXmlDocuments(path);

            return app;
        }
        
    }
}
