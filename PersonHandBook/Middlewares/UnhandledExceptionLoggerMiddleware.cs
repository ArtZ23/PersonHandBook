namespace PersonHandBook.Middlewares
{
	public class UnhandledExceptionLoggerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<UnhandledExceptionLoggerMiddleware> _logger;

		public UnhandledExceptionLoggerMiddleware(RequestDelegate next, ILogger<UnhandledExceptionLoggerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception occurred");
				throw;
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline
	public static class UnhandledExceptionLoggerMiddlewareExtensions
	{
		public static IApplicationBuilder UseUnhandledExceptionLogger(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<UnhandledExceptionLoggerMiddleware>();
		}
	}
}





