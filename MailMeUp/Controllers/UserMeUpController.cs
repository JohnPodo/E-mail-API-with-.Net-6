using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailMeUp.Controllers
{ 
    public class UserMeUpController : SuperController
    {

        // GET api/<UserMeUpController>/5
        [HttpGet("{id}")]
        public string GetUserById(int id)
        {
            return "value";
        }

        // POST api/<UserMeUpController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserMeUpController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserMeUpController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
