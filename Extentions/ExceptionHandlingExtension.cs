using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RestFullWebApi.Models;
using System.Net;
using System.Text.Json;

namespace RestFullWebApi.Extentions
{
	public static class ExceptionHandlingExtension
	{
		public static void ConfigureExecptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						//Log.Error($"Something went wrong: {contextFeature.Error}"); logla
						await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
						{

							StatusCode = context.Response.StatusCode,
							Message = $"Internal Server Error: {contextFeature.Error}"
						}));

					};
				});
			});
		}
	}

}
