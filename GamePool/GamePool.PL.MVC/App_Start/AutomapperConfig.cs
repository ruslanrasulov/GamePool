using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePool.PL.MVC.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {

            });
        }
    }
}