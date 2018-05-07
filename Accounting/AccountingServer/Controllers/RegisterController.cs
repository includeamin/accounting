using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json.Linq;

namespace AccountingServer.Controllers
{
    [Produces("application/json")]
    [Route("handamde/Register")]
    public class RegisterController : Controller
    {
        // GET: api/Register
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        [HttpGet("user/{username}", Name = "Login")]
        public string Get(string username)
        {
            Console.WriteLine(username);
            return username;
        }
        
        // POST: api/Register
        [HttpPost]
        public JObject Post()
        {
            Console.WriteLine("=================================");
            var data = HttpContext.Request.Form;
            string username = data["username"];
            string password = data["password"];
            string email = data["email"];
            var result = new JObject();
          
            try
            {
                Console.WriteLine($"new user registeration[{username}] [{email}]");

                using (var db = new LiteDatabase("DB.db"))
                {
                    var userdb = db.GetCollection<User>("users");

                    if (userdb.Exists(t => t.UserName == username))
                    {
                      
                        result["Result"] = "User with this username already exist";
                    }
                    else
                    {
                        Console.WriteLine($"{username} registered");
                        var user = new User()
                        {
                            UserName = username,
                            PassWord = password,
                            Email = email
                        };
                        userdb.Insert(user);
                     
                        result["Result"] = "OK";
                       
                    }
               
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
                result["Result"] = "NOT OK";
                result["Description"] = e.Message;
              

            }
            Console.WriteLine("=================================");


            return result;


        }
        
        // PUT: api/Register/5
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
