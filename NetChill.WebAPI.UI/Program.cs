using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using NetChill.Infrastructure.Identity.Models;
using NetChill.Infrastructure.Identity.Seeds;
using NetChill.Ioc.Dependency_Injection;
using NetChill.Persistence.Contexts;
using NetChill.WebAPI.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

/***Add services to the container.***/

//Configure DI into WebAPI program class
DependencyContainer.RegisterServices(builder.Services, builder.Configuration);

//Configure WebUI Services into program class
builder.Services.AddPresentationLayer();



var app = builder.Build();

#region Database Seeding 
//Database seeding upon application start
using (IServiceScope? scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<IdentityDbContext>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultUserRoles.SeedUserRoles(roleManager);
        await DefaultUsers.SeedUsers(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
#endregion


// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    //Configures Swagger for API testing
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //Turns off syntax highlight which causing performance issues...
        options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
        //Reverts Swagger UI 2.x  theme which is simpler not much performance benefit...
        options.ConfigObject.AdditionalItems.Add("theme", "agate"); 

    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//Configures client app access by whitelisting its URL
app.UseCors("CorsPolicy");

//Serving static files
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Media Files")),
    RequestPath = new PathString("/Media Files")
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();