using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MySql.Data.MySqlClient;
namespace WepApi_1
{
    public static class WebApiConfig
    {


        public static MySqlConnection con()
        {
            string con_string = "server=localhost;user id=root;password=7Miyemedimi;database=testing_mysql";
            MySqlConnection con = new MySqlConnection(con_string);
            return con;
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
