
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
        private readonly CustomerService _custService;

        public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager, AppUsersDbContext context, StaffService staffService, CustomerService custService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _staffService = staffService;
            _custService = custService;
        }

        [BindProperty]
        public List<SelectListItem> Users { get; set; }

        [BindProperty]
        public Message MyMessage { get; set; } = new();

        public List<Message> messages { get; set; }

        [BindProperty]
        public string MyUser { get; set; }

        public AppUser user { get; set; } = new();

        public AppUser receiver { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            //see the logged in user
            user = await _userManager.GetUserAsync(User);

            messages = _custService.GetAllMessages();

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

        public async Task<IActionResult> OnGetMessage()
        {
            if (ModelState.IsValid)
            {

                user = await _userManager.GetUserAsync(User);
                //get all the users from the database
                Users = _userManager.Users.ToList()
                    .Select(a => new SelectListItem { Text = a.UserName, Value = a.UserName })
                    .OrderBy(s => s.Text).ToList();

                Debug.WriteLine("IS it working?");
                MyMessage.UserName = "Natthida";
                MyMessage.When = DateTime.Now;
                MyMessage.UserId = "12fe0157-fda8-406f-af50-bc1ee89d8c5f";
                MyMessage.Text = "Hello";
                await _context.Messages.AddAsync(MyMessage);
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
