using Microsoft.AspNetCore.SignalR;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Service.Interfaces;

namespace WWW.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IArticleService _articleService;
        ILogger<DataHub> _logger;
        private readonly EntityBaseRepository<User> _userrepository;
        private readonly EntityBaseRepository<Chat> _chatrepository;
        public ChatHub(IArticleService articleService, ILogger<DataHub> logger, EntityBaseRepository<Chat> chatrepository, EntityBaseRepository<User> userrepository)
        {
            _userrepository = userrepository;
            _chatrepository = chatrepository;
            _logger = logger;
            _articleService = articleService;
        }
        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task LeaveGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }


        public async Task SendMessage(string userid, string message, string useravatar, string eventId)
        {
            if (message == "") return;


            Article Event = (await _articleService.GetById(int.Parse(eventId))).Data;
            User user = await _userrepository.GetValueByID(int.Parse(userid));
            await _chatrepository.Create(new Chat()
            {
                Article = Event,
                User = user,
                Message = message,
                DateTime = DateTime.Now,
            });

            await Clients.Group(eventId).SendAsync("ReceiveMessage", user.NickName, message, useravatar, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}