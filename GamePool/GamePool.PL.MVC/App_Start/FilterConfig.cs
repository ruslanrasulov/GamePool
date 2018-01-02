using System.Web.Mvc;
using GamePool.PL.MVC.Infrastructure;

namespace GamePool.PL.MVC.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filterCollection)
        {
            filterCollection.Add(new ExceptionFilter());
        }
    }
}