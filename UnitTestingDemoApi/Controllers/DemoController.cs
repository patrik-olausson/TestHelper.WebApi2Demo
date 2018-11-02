using System;
using System.Web.Http;
using UnitTestingDemoApi.Models;

namespace UnitTestingDemoApi.Controllers
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        private readonly IDemoService _service;

        public DemoController(IDemoService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var demoEntity = _service.GetById(id);
            if (demoEntity == null)
                return BadRequest();

            return Ok(demoEntity);
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody]DemoEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Guid.NewGuid());
        }
    }
}