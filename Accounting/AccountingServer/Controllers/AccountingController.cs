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
    [Route("handmade/Accounting")]
    public class AccountingController : Controller
    {
        // GET: api/Accounting
        [HttpGet]
        public JsonResult Get()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Get all accounts");
            Console.WriteLine("===========================================");
            try
            {
                using (var db = new LiteDatabase("DB.db"))
                {
                    var account = db.GetCollection<Account>("Accounts");
                    var obj = account.FindAll();
                    var json = Json(obj);
                    return json;
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                JsonResult result = new JsonResult(e);
                return result;


            }
            


        }

        // GET: api/Accounting/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Accounting
        [HttpPost("AddAccount/")]
        public JObject Post()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("ADDING ACCOUNT");
            JObject result = new JObject();
            try
            {
                var data = HttpContext.Request.Form;
               
                var owner = data["owner"];
                var name = data["name"];
                var lastName = data["lastName"];
                var tempObj = new Account()
                {
                    Owner = owner,
                    Name = name,
                    LastName = lastName,
                    AccountLogs = new List<AccountLogs>()
                };
                using (var db = new LiteDatabase("DB.db"))
                {
                    var accountDb = db.GetCollection<Account>("Accounts");
                    if (accountDb.Exists(a => a.Owner == tempObj.Owner && a.Name == tempObj.Name))
                    {
                        result["Result"] = "THIS ACCOUNT ALLREADY EXISTS";
                        Console.WriteLine("THIS ACCOUNT ALLREADY EXISTS");
                        return result;
                    }
                    else
                    {
                        accountDb.Insert(tempObj);
                        result["Result"] = "ACCOUNT ADDED";
                        Console.WriteLine("ACCOUNT ADDED");
                        return result;
                    }
                }
            }
            catch (Exception EX_NAME)
            {
                Console.WriteLine(EX_NAME);
                result["Result"] = EX_NAME.Message;
                return result;

            }

            Console.WriteLine("===========================================");
            
        }
        
       
        // PUT: api/Accounting/5
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
