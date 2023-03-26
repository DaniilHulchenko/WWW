using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WWW.DAL;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

using WWW.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

// Azure Insights
builder.Services.AddApplicationInsightsTelemetry();

/*############# Add services to the container #####################################*/
builder.Services.AddControllersWithViews();
//                          SQL
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("StoreDatabase")
    ));
//                        Services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBaseService<User>, UserService>();
/*#################################################################################*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
