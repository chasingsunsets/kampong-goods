
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kampong_goods.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using kampong_goods.Services;

namespace kampong_goods.Pages.Chat
{

    //only authenticated can access this page
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppUsersDbContext _context;
        private readonly StaffService _staffService;

        public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager, AppUsersDbContext context, StaffService staffService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _staffService = staffService;
        }

        [BindProperty]
        public List<SelectListItem> Users { get; set; }

        [BindProperty]
        public string MyUser { get; set; }

        public AppUser user { get; set; } = new();

        public AppUser receiver { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            //see the logged in user
            user = await _userManager.GetUserAsync(User);

            receiver = _staffService.GetCustomerById(id);

            //get all the users from the database
            Users = _userManager.Users.ToList()
                .Select(a => new SelectListItem { Text = a.UserName, Value = a.UserName })
                .OrderBy(s => s.Text).ToList();

            //get logged in user name
            MyUser = User.Identity.Name;

            Debug.WriteLine(user);
            Debug.WriteLine(receiver);
            return Page();
        }

        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Page();
            }
            return Page();
        }

        //public async Task<IActionResult> Index()
        //{
        //    var currentUser = _userManager.GetUserAsync(User);
        //    var messages = await _context.Messages.ToListAsync();
        //    return Page();
        //}

        //public async Task<IActionResult> Create(Message message)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        message.UserName = User.Identity.Name;
        //        var sender = await _userManager.GetUserAsync(User);
        //        message.UserId = sender.Id;
        //        await _context.Messages.AddAsync(message);
        //        await _context.SaveChangesAsync();
        //        return Page();
        //    }
        //    return Page();
        //}
    }

}
