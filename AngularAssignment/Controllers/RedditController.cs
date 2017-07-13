using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AngularAssignment.Models;

namespace AngularAssignment.Controllers
{
    [RoutePrefix("api")]
    public class RedditController : ApiController
    {
        private static List<Data2> _datas = new List<Data2>();

        [HttpGet]
        [Route("Reddit")]
        public IEnumerable<Data2> Get()
        {
            if (_datas != null && _datas.Any())
                return _datas;

            var request = WebRequest.Create("https://www.reddit.com/r/aww.json");
            var response = request.GetResponse();

            string json;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }


            var serializer = new JavaScriptSerializer();

            var persons = serializer.Deserialize<RootObject>(json);

            _datas = persons.data.children.Select(x => x.data).ToList();

            return _datas;
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(bool isApprove)
        {
            if (_datas != null && _datas.Any())
                return Ok(_datas.Where(x => x.isApprove == isApprove));

            var request = WebRequest.Create("https://www.reddit.com/r/aww.json");
            var response = request.GetResponse();

            string json;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }

            var serializer = new JavaScriptSerializer();

            var persons = serializer.Deserialize<RootObject>(json);

            _datas = persons.data.children.Select(x => x.data).ToList();

            return Ok(_datas.Where(x => x.isApprove == isApprove));
        }

        public Data2 Get(string id)
        {
            var data = _datas.FirstOrDefault(x => x.id == id);
            if (data == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return data;
        }

        [HttpPost]
        [Route("Reddit")]
        public HttpResponseMessage Post(Data2 friend)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);


            var data = _datas.FirstOrDefault(x => x.id == friend.id);
            if (data == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            data.isApprove = friend.isApprove;

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}