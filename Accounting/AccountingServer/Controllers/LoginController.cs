using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AccountingServer.Controllers
{
    [Produces("application/json")]
    [Route("handmade/Login")]
    public class LoginController : Controller
    {
        // GET: api/Login
        [HttpGet]
        public JObject Get()
        {
          return null;
        }

        // GET: api/Login/5
        [HttpGet("{username}/{password}", Name = "Get")]
        public JObject Get(string username,string password)
        {
            var data = new JObject();
            using (var db = new LiteDatabase("DB.db"))
            {
                var users = db.GetCollection<User>("users");
                if (users.Exists(u => u.UserName == username && u.PassWord == password))
                {
                    data["Result"] = "OK";
                    Console.WriteLine($"{username} loging in");
                }
                else
                {
                    data["Result"] = "USERNAME NOT EXIST OR PASSWORD IS WRONG";
                }

            }
           
            return data;
        }
        
        // POST: api/Login
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/Login/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
