using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Azure Insights
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddControllersWithViews();


/*####################################### Add Services ############################################*/

ExtensionsServices.AddMyServices(builder);

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
        //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    })/*
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })*/;

builder.Services.AddAuthorization();

builder.Services.AddSession();

/*##################################################################################################*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

ExtensionsApp.AddMyAppExtensions(app);

app.Run();
