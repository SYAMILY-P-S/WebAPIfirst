﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAPIfirst
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                // These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
