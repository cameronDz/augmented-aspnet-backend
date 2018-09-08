﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AugmentedAspnetBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "v0.1/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}