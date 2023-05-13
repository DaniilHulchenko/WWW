using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Azure Insights
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddControllersWithViews();

/*#####################################  Add Swager ###############################################*/

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

/*####################################### Add Services ############################################*/

ServicesExtensions.AddMyServices(builder);

/*#####################################  Add Authentication  #######################################*/
//SignInManager.CheckPasswordSignInAsync();
//builder.Services.AddAuthentication()
//        .AddGoogle(options =>
//        {
//            options.ClientId = Configuration["Authentication:Google:ClientId"];
//            options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
//        });
                                
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");

        options.LoginPath = "/account/login";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "your-client-id";
        options.ClientSecret = "your-client-secret";
    }); 

builder.Services.AddAuthorization();

/*##################################################################################################*/

var app = builder.Build();

// HangFire
app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


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
/*##########################################################################*/

app.Run();
