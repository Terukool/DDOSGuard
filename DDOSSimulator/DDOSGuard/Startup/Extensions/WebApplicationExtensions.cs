namespace DDOSGuardService.Startup.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureSwagger(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI();
        }

    }
}