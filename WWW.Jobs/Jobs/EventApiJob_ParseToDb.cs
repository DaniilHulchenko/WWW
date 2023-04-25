using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.API;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;

namespace WWW.Jobs.Workers
{
    public class ArticleApiJob_ParseToDb : IBackgroundJob
    {
        ILogger<ArticleApiJob_ParseToDb> _logger;
        private readonly RestApiRequest _restapiRepository;
        private readonly IArticleRepository _articleRepository;
        public ArticleApiJob_ParseToDb(RestApiRequest restapiRepository, ILogger<ArticleApiJob_ParseToDb> logger, IArticleRepository articleRepository)
        {
            _restapiRepository = restapiRepository;
            _logger = logger;
            _articleRepository = articleRepository;

        }
        public async Task ExecuteAsync()
        {
            _restapiRepository.ApiSelector("Articles:ticketmaster");
            dynamic ApiData = (await _restapiRepository.GetDataAsync())._embedded.events;
            await _articleRepository.Create(new Article()
            {
                Title = ApiData.name
            });
            
            _logger.LogInformation($"!!! : ArticleApiJob done");



        }
    }
}
