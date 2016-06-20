using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Natmir.ImageService.Controllers
{
    public class RefreshTokensController : ApiController
    {
        [RoutePrefix("api/RefreshTokens")]
        public class RefreshTokensController : ApiController
        {

          

         
            [Authorize(Users = "Admin")]
            [Route("")]
            public IHttpActionResult Get()
            {
                return Ok();
            }

            //[Authorize(Users = "Admin")]
            [AllowAnonymous]
            [Route("")]
            public async Task<IHttpActionResult> Delete(string tokenId)
            {
                //var result = await _repo.RemoveRefreshToken(tokenId);
                //if (result)
                //{
                //    return Ok();
                //}
                //return BadRequest("Token Id does not exist");


                return Ok();

            }

            protected override void Dispose(bool disposing)
            {
                //if (disposing)
                //{
                //    _repo.Dispose();
                //}

                base.Dispose(disposing);
            }
        }
    }
}