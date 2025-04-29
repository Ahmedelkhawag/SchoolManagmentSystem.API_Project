using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResourse> _localizer;
        public ResponseHandler(IStringLocalizer<SharedResourse> localizer)
        {
            _localizer = localizer;
        }
        public GeneralResponse<T> Deleted<T>(string Message = null)
        {
            return new GeneralResponse<T>
            {
                statusCode = System.Net.HttpStatusCode.OK,
                Message = Message == null ? _localizer[SharedResourseKeys.Deleted] : Message,
                Succeeded = true,

            };
        }

        public GeneralResponse<T> Success<T>(T entity, object Meta = null, string Messege = null)
        {
            return new GeneralResponse<T>
            {
                statusCode = System.Net.HttpStatusCode.OK,
                Message = Messege == null ? _localizer[SharedResourseKeys.Succeeded] : Messege,
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
                Message = Message == null ? _localizer[SharedResourseKeys.BadRequest] : Message
            };
        }

        public GeneralResponse<T> UnprocessableEntity<T>(string Message = null)
        {
            return new GeneralResponse<T>()
            {
                statusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? _localizer[SharedResourseKeys.Unprocessable] : Message
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

        public GeneralResponse<T> Created<T>(T entity, object Meta = null, string Messege = null)
        {
            return new GeneralResponse<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = Messege == null ? _localizer[SharedResourseKeys.Created] : Messege,
                Meta = Meta
            };
        }

        public GeneralResponse<T> NoContent<T>(T entity, object Meta = null)
        {
            return new GeneralResponse<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.NoContent,
                Succeeded = true,
                Message = _localizer[SharedResourseKeys.Updated],
                Meta = Meta
            };
        }

        public GeneralResponse<T> Deleted<T>(T entity, string Message = null)
        {
            return new GeneralResponse<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.NoContent,
                Succeeded = true,
                Message = Message == null ? "Deleted Successfully.." : Message

            };
        }
    }

}
