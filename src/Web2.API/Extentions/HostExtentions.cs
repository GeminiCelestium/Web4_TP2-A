using System.Runtime.CompilerServices;
using Web2.API.Data;

namespace Web2.API.Extentions
{
    public static class IHostExtentions
    {
        public static void CreatDBIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope()) 
            {
                var services = scope.ServiceProvider;
                try 
                {
                    var context = services.GetRequiredService<TP2A_Context>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex) 
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB");
                }
            }
        }
    }
}
