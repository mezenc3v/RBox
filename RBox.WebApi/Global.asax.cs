using System.Web;
using System.Web.Http;


namespace RBox.WebApi
{
    /// <summary>
    /// WebApiApp
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// App start
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
