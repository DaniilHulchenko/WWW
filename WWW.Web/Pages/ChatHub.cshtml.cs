using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WWW.Web.Pages
{
    [AllowAnonymous]
    public class ChatHub : PageModel
    {
        private readonly ILogger<IndexModel> logger;

        public ChatHub(ILogger<IndexModel> logger)
        {
            this.logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
