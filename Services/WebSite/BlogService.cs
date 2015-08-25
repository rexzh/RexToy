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
            return string.Format("A post of id {0}", id);
        }

        [Route("post/:id/comments")]
        public string GetComments(int id)
        {
            List<string> comments = new List<string>();
            comments.Add("comment 1");
            comments.Add("comment 2");
            var x = new { Post = id, Comments = comments };
            return x.ToJsonString();
        }

        [Route("post/:id/comment/:commentId")]
        public string GetComment([RouteParam("id")]int postId, int commentId)
        {
            return string.Format("a comment of post {0} and comment {1}", postId, commentId);
        }
    }
}
