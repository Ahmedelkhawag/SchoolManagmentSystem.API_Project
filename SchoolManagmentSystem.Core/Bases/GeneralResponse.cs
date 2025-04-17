using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Bases
{
    public class GeneralResponse<T>
    {

        public GeneralResponse(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public GeneralResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public GeneralResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public GeneralResponse()
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
