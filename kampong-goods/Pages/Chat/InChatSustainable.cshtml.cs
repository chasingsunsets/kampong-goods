
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kampong_goods.Models;
using System.Diagnostics;
using kampong_goods.Services;

namespace kampong_goods.Pages.Chat
{

    //only authenticated can access this page
    [Authorize]
    public class InChatSustainableModel : PageModel
    {
        //get current user
        private readonly UserManager<AppUser> _userManager;

        //save to db
        private readonly AppUsersDbContext _context;

        //gets the message db
        private readonly CustomerService _custService;

        public InChatSustainableModel(UserManager<AppUser> userManager, AppUsersDbContext context, CustomerService custService)
        {
            _userManager = userManager;
            _context = context;
            _custService = custService;
        }

        //gets the dropdown list option
        [BindProperty]
        public List<SelectListItem> Users { get; set; }

        //binds form
        [BindProperty]
        public Message MyMessage { get; set; } = new();

        //gets the list of messages
        public List<Message> messages { get; set; }

        //gets the list of customers
        public List<AppUser> CustomerList { get; set; }

        public AppUser Sender { get; set; } = new();

        public AppUser Receiver { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            //see the logged in user
            Sender = await _userManager.GetUserAsync(User);

            //gets the whole list of messages
            messages = _custService.GetAllMessages();

            //customer list
            CustomerList = _custService.GetAll();

            //get receiver
            Receiver = _custService.GetCustomerById(id);

            Debug.WriteLine("Sender " + Sender);

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(string id)
        //{
        //    Debug.WriteLine("Hello");

        //    //see the logged in user
        //    Sender = await _userManager.GetUserAsync(User);

        //    //gets the whole list of messages
        //    messages = _custService.GetAllMessages();

        //    //customer list
        //    CustomerList = _custService.GetAll();

        //    //get receiver
        //    Receiver = _custService.GetCustomerById(id);

        //    MyMessage.UserName = Sender.UserName;
        //    MyMessage.UserId = Sender.Id;
        //    MyMessage.Sender = Sender;

        //    //CHECK
        //    Debug.WriteLine("Sender Username " + MyMessage.UserName);
        //    Debug.WriteLine("Sender Username" + MyMessage.UserId);

        //    await _context.Messages.AddAsync(MyMessage);
        //    await _context.SaveChangesAsync();

        //    return Page();
        //}


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
