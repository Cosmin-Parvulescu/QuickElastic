using System.Web.Http;
using QuickElastic.Data;

namespace QuickElastic.Web.Controllers
{
    public class SearchController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Users([FromUri] string text)
        {
            var elasticSearcher = new UserElasticSearcher();
            var results = elasticSearcher.Search(text);

            return Ok(results);
        }
    }
}
