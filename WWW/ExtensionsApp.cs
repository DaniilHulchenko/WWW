using Hangfire;
using WWW.Hubs;
//using WWW.Hubs;

public static class ExtensionsApp
{
    public static void AddMyAppExtensions(this WebApplication app)
    {
        /*################################### HangFire ###############################*/

        app.UseHangfireDashboard();

        /*################################### Swager ###############################*/
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.MapHub<ChatHub>("/СhatHub");


    }
}