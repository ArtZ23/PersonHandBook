using Microsoft.Extensions.Options;
using System.Globalization;

namespace PersonHandBook.Middlewares
{
	public class LocalizationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LocalizationMiddleware> _logger;
		public LocalizationMiddleware(RequestDelegate next, ILogger<LocalizationMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			var supportedCultures =
				context.RequestServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value.SupportedCultures;

			var langHeader = context.Request.Headers["Accept-Language"].ToString();
			var culture = langHeader.Split(',').FirstOrDefault()?.Trim();

			if (!string.IsNullOrEmpty(culture))
			{
				try
				{
					var cultureInfo = new CultureInfo(culture);
					if (supportedCultures.Contains(cultureInfo))
					{
						CultureInfo.CurrentCulture = cultureInfo;
						CultureInfo.CurrentUICulture = cultureInfo;
					}
				}
				catch (CultureNotFoundException)
				{
					// Handle invalid culture
				}
			}

			await _next(context);
		}
	}

	public static class LocalizationMiddlewareExtensions
	{
		public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LocalizationMiddleware>();
		}
	}
}

