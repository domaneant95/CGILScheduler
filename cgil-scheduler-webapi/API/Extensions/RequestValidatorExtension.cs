using API.Services;

namespace API.Extensions
{
    public static class RequestValidatorExtensions
    {
        // Extensions method to simplify RequestValidatorMiddleware usage 
        public static IApplicationBuilder UseRequestValidator(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestValidatorMiddleware>();
        }
    }
}
