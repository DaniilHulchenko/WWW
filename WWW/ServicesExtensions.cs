using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WWW.DAL;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Jobs;
using WWW.Jobs.Implementations;
using WWW.Jobs.Workers;
using WWW.Service.Helpers;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

public static class ServicesExtensions
{
    public static void AddMyServices(this WebApplicationBuilder builder)
    {
        IServiceCollection Services = builder.Services;
/*#####################################  Add SQL ###############################################*/
        Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            connectionString: builder.Configuration.GetConnectionString("StoreDatabase")
            ));
/*#####################################  Add  Services DB ###############################################*/
        Services.AddScoped(typeof(EntityBaseRepository<>)); 

        Services.AddTransient<ICategoryRepository, CategoryRepository>();
        Services.AddTransient<IArticleRepository, ArticleRepository>();
        Services.AddTransient<IAccountRepository, AccountRepository>();
        Services.AddTransient<IBaseRepository<Tags>, TagRepository>();
        Services.AddTransient<IDateRepository, DateRepository>();
        Services.AddTransient<IPictureRepository, PictureRepository>();
        Services.AddTransient<ILocationRepository, LocationRepository>();
        
        //Services.AddScoped<IBaseRepository<Base>, EntityBaseRepository<Base>>();

        Services.AddTransient<IArticleService, ArticleService>();
        Services.AddTransient<ICategoryService, CategoryService>();
        Services.AddTransient<IAccountService, AccountService>();
/*#####################################  Add Other Services ###############################################*/
        //                          Picture 
        Services.AddScoped<DownloadService>();

//                          API
        //Services.AddTransient<IApiRepository<HttpApiRequest>, HttpApiRequest>();
        Services.AddTransient<RestApiRequest>();
//                    GoogleOAuth
        Services.AddTransient<GoogleOAuthService>();

//                          Jobs
        Services.AddTransient<ArticleApiJob_ParseToDb>();

//                          google
        //Services.AddTransient<GoogleApiService>();
        Services.AddTransient<SingInFromGoogleService>();
        /*#####################################   HangFire (Job Schedule) ###############################################*/
        builder.Services.AddHostedService<JobWorker>();

        Services.AddSingleton<IJobService, JobService>();

        Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage()
        );
        Services.AddHangfireServer();

/*#####################################  Add AutoMapper ###############################################*/
        Services.AddAutoMapper(typeof(Program));

/*#####################################  Add Swager ###############################################*/

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });


        //Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
        //       Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


    }
}