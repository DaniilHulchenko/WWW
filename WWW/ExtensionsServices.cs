﻿using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WWW.DAL;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Hubs;
using WWW.Jobs;
using WWW.Jobs.Implementations;
using WWW.Jobs.Jobs;
using WWW.Service.Helpers;
using WWW.Service.Helpers.Api;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

public static class ExtensionsServices
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
        Services.AddTransient<IUserRepository, UserRepository>();
        //Services.AddTransient<IBaseRepository<Tags>, TagRepository>();
        //Services.AddTransient<IDateRepository, DateRepository>();
        //Services.AddTransient<IPictureRepository, PictureRepository>();
        //Services.AddTransient<ILocationRepository, LocationRepository>();
        
        //Services.AddScoped<IBaseRepository<Base>, EntityBaseRepository<Base>>();

        Services.AddTransient<IArticleService, ArticleService>();
        Services.AddTransient<ICategoryService, CategoryService>();
        Services.AddTransient<IUserService, UserService>();
        /*#####################################  Add Other Services ###############################################*/
//              account 
        Services.AddTransient<WWW.Service.Implementations.AccountService>();

//                          Picture 
        Services.AddScoped<DownloadService>();

//                          API
        //Services.AddTransient<IApiRepository<HttpApiRequest>, HttpApiRequest>();
        Services.AddTransient<IRestApiRequest, RestApiRequest>();
//                    GoogleOAuth
        Services.AddTransient<GoogleOAuthService>();

//                          Jobs
        Services.AddTransient<IBackgroundJob, EventApiJob_ParseToDb>();

//                          google
        //Services.AddTransient<GoogleApiService>();
        Services.AddTransient<GoogleSingInService>();

        //Tests
        //Services.AddTransient<ArticleServiceTests>();
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
/*#####################################  Add SignalR ###############################################*/
        builder.Services.AddSignalR();
        builder.Services.AddTransient<CityHub>();


        ///*#####################################  Add ElasticSearch  ###############################################*/

        ////var elasticsearchUrl = builder.Configuration.GetSection("Elasticsearch:Url").Value ?? "http://localhost:9200";

        ////Services.AddSingleton<IElasticClient>(x => new ElasticClient(new ConnectionSettings(new Uri(elasticsearchUrl))));


        //Services.AddSingleton<ConnectionSettings>(sp =>
        //{
        //    var configuration = sp.GetRequiredService<IConfiguration>();
        //    var elasticsearchUrl = configuration.GetSection("Elasticsearch:Url").Value;
        //    var connectionSettings = new ConnectionSettings(new Uri(elasticsearchUrl)); // Замените "my_index" на имя вашего индекса .DefaultIndex("my_index")
        //    return connectionSettings;
        //});

        //Services.AddSingleton<IElasticClient>(sp =>
        //{
        //    var connectionSettings = sp.GetRequiredService<ConnectionSettings>();
        //    var elasticClient = new ElasticClient(connectionSettings);
        //    return elasticClient;
        //});


        //Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
        //       Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));




    }
}