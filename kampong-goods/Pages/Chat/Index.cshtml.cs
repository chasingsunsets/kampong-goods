using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace kampong_goods.Pages.Chat
{
    public class IndexModel : PageModel
    {
        private readonly CustomerService _customerService;
        private readonly AppUsersDbContext _appUsersDbContext;
        private UserManager<AppUser> _userManager { get; }

        public IndexModel(CustomerService customerService, AppUsersDbContext appUsersDbContext, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            _appUsersDbContext = appUsersDbContext;
            _userManager = userManager;
        }

        public List<Message> MessageList { get; set; } = new();

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserName = currentUser.UserName;
            var messages = await _appUsersDbContext.Messages.ToListAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> Create(Message message)
        {
            if(ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User); //get current user
                message.UserId = sender.Id;
                await _appUsersDbContext.Messages.AddAsync(message);
                await _appUsersDbContext.SaveChangesAsync();
                return Page();
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
