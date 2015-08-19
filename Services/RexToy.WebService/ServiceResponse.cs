using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    public class ServiceResponse
    {
        public int HttpStatus;
        public string Json;

        public ServiceResponse() { }
        internal ServiceResponse(int status, string json)
        {
            HttpStatus = status;
            Json = json;
        }

        public static readonly ServiceResponse InternalError = new ServiceResponse(500, "{\"message\": \"internal error\"}");
        public static readonly ServiceResponse MethodNotAllow = new ServiceResponse(405, "{\"message\": \"method not allow\"}");
        public static readonly ServiceResponse OKWithEmptyBody = new ServiceResponse(200, "");
        public static readonly ServiceResponse BadRequest = new ServiceResponse(400, "{\"message\": \"bad reuqest\"}");
        public static readonly ServiceResponse NotImplemented = new ServiceResponse(400, "{\"message\": \"not implemented\"}");
    }
}
