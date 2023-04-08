using Microsoft.EntityFrameworkCore;
using WWW.DAL;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;
using WWW.API;
using WWW.Domain.Entity;
using WWW.Jobs;

public static class ServicesExtensions
{
    public static void AddMyServices(this WebApplicationBuilder builder)
    {
        //WebApplicationBuilder _builder = builder;
        //                          SQL
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            connectionString: builder.Configuration.GetConnectionString("StoreDatabase")
            ));
        //                        Services
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IBaseRepository<Tags>, TagRepository>();

        builder.Services.AddTransient<IArticleService, ArticleService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IAccountService, AccountService>();

        //                          API
        builder.Services.AddTransient<IBackgroundApiJob<APIRequest>, APIRequest>();
        builder.Services.AddTransient<IBackgroundApiJob<RestApiRequest>, RestApiRequest>();

        //                          HangFire(Job Schedule)
        //builder.Services.AddHostedService<EventsApiJobWorker>();

        //return services;
    }
}