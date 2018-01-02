using GamePool.PL.MVC.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GamePool.PL.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMaps();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            bool isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");

            if (isGet && !HttpContext.Current.Request.Url.AbsolutePath.Contains("."))
            {
                string lowercaseURL = $"{Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}{HttpContext.Current.Request.Url.AbsolutePath}";

                if (Regex.IsMatch(lowercaseURL, @"[A-Z]"))
                {
                    lowercaseURL = lowercaseURL.ToLower() + HttpContext.Current.Request.Url.Query;

                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", lowercaseURL);
                    Response.End();
                }
            }
        }
    }
}