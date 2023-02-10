using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.DAL;
using BankingApi.Model;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BankingApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUpdateController : ControllerBase
    {
        private IUserService _userService;
        public ProfileUpdateController(IUserService UserService)
        {
            _userService = UserService;

        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            //return new string[] { "value1", "value2" };
            using (var context = new BankContext())
            {                
                return context.User.ToList();
            }
        }

        [HttpPost]
        [Route("Update_Profile")]
        public IActionResult UpdateUserProfile([FromBody] UserModel Profilerequest)
        {
            if (!ModelState.IsValid) return BadRequest(Profilerequest);
            bool _result = _userService.UpdateProfile(Profilerequest);
            if (_result)
                return Ok("User Updated Successfully");
            else
                return BadRequest("User Updated Failed");
        }
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       

            // POST api/<CustomerController>
            [HttpPost]
        public void Post([FromBody] User value)
        {
            using (var context = new BankContext())
            {
                value.Password = "ggoo";
                Account accModel = new Account();
                // value.GetType().GetProperties
                //accModel =\

                ICollection<Account> lisadd = new List<Account>();

                //lisvalue.Address.OfType<ICollection<Account>>().FirstOrDefault().to;

                accModel = value.Account.OfType<Account>().FirstOrDefault(c => c.AccountNumberGenerated == null);
                // accModel =
                value.Account.OfType<Account>().Where(c => c.AccountNumberGenerated == null).ToList().Select(c=>c.AccountNumberGenerated="100");
                accModel.AccountNumberGenerated = "3333333333";
               // Account obj1 = new Account();
                //Account obj2 = JsonConvert.DeserializeObject<Account>(JsonConvert.SerializeObject(lisadd));
                lisadd.Add(accModel);
                value.Account = JsonConvert.DeserializeObject<ICollection<Account>>(JsonConvert.SerializeObject(lisadd));
                //UserModel addUser = new UserModel();
                //AddressModel useraddress = new AddressModel();
                //AddressModel addAddress = new AddressModel();
                //ICollection<AddressModel> lisadd = new List<AddressModel>();
                //addUser.FirstName = value.FirstName;
                //addUser.LastName = value.LastName;
                //addUser.Email = value.Email;
                //addUser.Mobile = value.Mobile;
                //addUser.CustomerId = value.CustomerId;

                //useraddress = value.Address.OfType<AddressModel>().FirstOrDefault();
                //addAddress.CustomerId = useraddress.CustomerId;
                //addAddress.Address1 = useraddress.Address1;
                //addAddress.Address2 = useraddress.Address2;
                //addAddress.Address3 = useraddress.Address3;
                //addAddress.Mobile = useraddress.Mobile;
                //lisadd.Add(addAddress);
                //addUser.Address = lisadd;
                // (ICollection<Address>)addAddress;

                //UserModel obj1 = new UserModel();
                //User obj2 = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(addUser));

                //context.User.Add(obj2);
                context.User.Add(value);

                context.SaveChanges();



            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
