using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Bases
{
    public class ResponseHandler
    {
        public ResponseHandler()
        {
        }
        public Response<T> Deleted<T>()
        {
            return new Response<T>
            {
                statusCode = System.Net.HttpStatusCode.OK ,
                Message = "Deleted Successfully",
                Succeeded = true,
                
            };
        }

        public Response<T> Success<T>(T entity , object Meta = null)
        { 
         return new Response<T>
         {
             statusCode = System.Net.HttpStatusCode.OK,
             Message = "Done Successfully",
             Succeeded = true,
             Data = entity,
             Meta = Meta
         };
        }

        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created",
                Meta = Meta
            };
        }
    }

}
