using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DS_AD_API_Demo.Data;
using System.Security.Claims;

namespace DS_AD_API_Demo.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static readonly string[] scopeRequiredByApi = new string[] { "Access_DocStream_As_A_User" };
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Authorize("Access_DocStream_As_A_User")]        
        [Route("verify-ad-user")]
        public ActionResult<dynamic> VerifyADUser()
        {
            var userClaims = HttpContext.User.Claims;
            
            DSEnterpriseUser dSEnterpriseUser = new DSEnterpriseUser();
            
            dSEnterpriseUser.FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
            dSEnterpriseUser.LastName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            dSEnterpriseUser.Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;




            return Ok(dSEnterpriseUser);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
