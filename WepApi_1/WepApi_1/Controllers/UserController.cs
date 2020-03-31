using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WepApi_1.Models;

namespace WepApi_1.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        [HttpPost]

        [EnableCors(origins: "*", headers: "*", methods: "*")]
       
        [Route("api/logout")]
       
        public IHttpActionResult Logout([FromBody] User user)
        {
            MySqlCommand cmd = new MySqlCommand("Set_Ofline", WebApiConfig.con());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@u", MySqlDbType.VarChar).Value = user.username;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
           
            return Ok();
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Post([FromBody] LoginUser loginUser)
        {
            MySqlCommand cmd = new MySqlCommand("login", WebApiConfig.con());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@u", MySqlDbType.VarChar).Value = loginUser.username;
            cmd.Parameters.Add("@p", MySqlDbType.VarChar).Value = loginUser.password;
            cmd.Connection.Open();
            MySqlDataReader query = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            User user = null;

            while (query.Read())
            {
                user = new User { id = Convert.ToInt32(query["id"].ToString()), username = query["username"].ToString(), password = query["password"].ToString(), online = Convert.ToInt32(query["online"].ToString()) };

            }
            if (user == null)
            {

                return Ok(HttpStatusCode.NotFound);

            }
            if (user.online==1)
            {
                
                return Ok(HttpStatusCode.BadRequest);
            }
            MySqlCommand upd = new MySqlCommand("Set_Online", WebApiConfig.con());
            upd.Connection.Open();
            upd.CommandType = CommandType.StoredProcedure;

            upd.Parameters.Add("@u", MySqlDbType.VarChar).Value = loginUser.username;

            upd.ExecuteNonQuery();
           
            return Ok( user);
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/alluser")]
        public IHttpActionResult Getalluser()
        {

            List<User> alluser = new List<User>();
            MySqlCommand cmd = new MySqlCommand("get_all_user", WebApiConfig.con());
            cmd.CommandType = CommandType.StoredProcedure;

          
            cmd.Connection.Open();
            MySqlDataReader query = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            User user = null;

            while (query.Read())
            {
                user = new User { id = Convert.ToInt32(query["id"].ToString()), username = query["username"].ToString(), password = query["password"].ToString(), online = Convert.ToInt32(query["online"].ToString()) };
                alluser.Add(user);
            }
            query.Close();


          

            
            return Ok(alluser);
        }


    }
}
