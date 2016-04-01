using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BLL;
using Model;
using Microsoft.AspNet.Mvc.Filters;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.PlatformAbstractions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageProcessorTest.Controllers
{
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        private IPictureBLL _pictureRepo;
        private IApplicationEnvironment _hostingEnvironment;

        public PictureController(IPictureBLL pictureRepo, IApplicationEnvironment hostingEnvironment)
        {
            _pictureRepo = pictureRepo;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/values
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<Picture>))]
        public List<Picture> Get()
        {
            return _pictureRepo.getPictures();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("Picture")]
        public async Task<IActionResult> PicturePost(ICollection<IFormFile> value) {

            try {
                await _pictureRepo.uploadPicture(value, _hostingEnvironment.ApplicationBasePath);
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(500);
            }
            return new ObjectResult("Ok");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

   
}
