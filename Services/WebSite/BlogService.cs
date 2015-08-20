using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RexToy.WebService;
using RexToy.Json;

namespace WebSite
{
    public class BlogService : IRESTfulApi
    {
        [Route("post/:id")]
        public string GetPost(int id)
        {
            return "A post";
        }

        [Route("post/:id/comments")]
        public string GetComments(int id)
        {
            List<string> comments = new List<string>();
            comments.Add("comment 1");
            comments.Add("comment 2");            
            return comments.ToJsonString();
        }
    }
}
