using Microsoft.AspNetCore.SignalR;
using NuGet.Versioning;
using System.Linq;
using WWW.Service.Interfaces;

namespace WWW.Hubs
{
    public class DataHub : Hub
    {
        private readonly IArticleService _articleService;
        ILogger<DataHub> _logger;
        public DataHub(IArticleService articleService, ILogger<DataHub> logger)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task GetTitle()
        {
            var data = (await _articleService.GetAll()).Data;
            await Clients.Caller.SendAsync("ReceiveTitle", data.First().Title);
        }
    }
}