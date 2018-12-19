using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AssessmentManagerAPI.Common
{
    public class Common
    {
        //public static string APIDataFolder =  ConfigurationManager.AppSettings["APIDataFolder"];
        public static string APIDataFolder = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") +"\\";

    }
}