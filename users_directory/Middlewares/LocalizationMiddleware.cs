using System.Globalization;

namespace users_directory.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrWhiteSpace(acceptLanguage))
            {
                try
                {
                    var culture = acceptLanguage.Split(',').FirstOrDefault();
                    if (!string.IsNullOrEmpty(culture))
                    {
                        CultureInfo.CurrentCulture = new CultureInfo(culture);
                        CultureInfo.CurrentUICulture = new CultureInfo(culture);
                    }
                }
                catch (CultureNotFoundException)
                {
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                }
            }

            await _next(context);
        }
    }
}
