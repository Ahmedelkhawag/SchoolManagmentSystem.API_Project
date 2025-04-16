using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Bases
{
    public class Response<T>
    {

        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response()
        {
        }

        public HttpStatusCode  statusCode { get; set; }
        public string Message { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }


    }
}
