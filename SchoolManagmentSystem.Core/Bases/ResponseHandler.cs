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
        public GeneralResponse<T> Deleted<T>()
        {
            return new GeneralResponse<T>
            {
                statusCode = System.Net.HttpStatusCode.OK ,
                Message = "Deleted Successfully",
                Succeeded = true,
                
            };
        }

        public GeneralResponse<T> Success<T>(T entity , object Meta = null)
        { 
         return new GeneralResponse<T>
         {
             statusCode = System.Net.HttpStatusCode.OK,
             Message = "Done Successfully",
             Succeeded = true,
             Data = entity,
             Meta = Meta
         };
        }

        public GeneralResponse<T> Unauthorized<T>()
        {
            return new GeneralResponse<T>()
            {
                statusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public GeneralResponse<T> BadRequest<T>(string Message = null)
        {
            return new GeneralResponse<T>()
            {
                statusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public GeneralResponse<T> UnprocessableEntity<T>(string Message = null)
        {
            return new GeneralResponse<T>()
            {
                statusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "Unprocessable Entity" : Message
            };
        }

        public GeneralResponse<T> NotFound<T>(string message = null)
        {
            return new GeneralResponse<T>()
            {
                statusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public GeneralResponse<T> Created<T>(T entity, object Meta = null)
        {
            return new GeneralResponse<T>()
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
