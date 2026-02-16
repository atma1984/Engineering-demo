using EngineeringSystem.Backend.Api.Diagnostics.Integration;
using EngineeringSystem.Backend.Api.Middleware;

namespace EngineeringSystem.Backend.Api
{
    public static class MiddlewarePipeline
    {

        public static async Task<WebApplication> UseApiAsync(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();                        
                app.MapDevDiagnosticsEndpoints();
            }
            await app.UseDevDiagnosticsAsync();
            app.UseHttpsRedirection();
            app.MapControllers();

            return app;
        }
    }
}
