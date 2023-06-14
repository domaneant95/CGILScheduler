namespace API.Services
{
    public class RequestValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.ContentLength == null || context.Request.ContentLength == 0)
                && context.Request.ContentType != null
                && context.Request.ContentType.ToUpper().StartsWith("MULTIPART/"))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Multipart request body must not be empty.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}

