using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Services
{
    public class CustomerService
    {
        private readonly AppUsersDbContext _db;
        private readonly UserManager<AppUser> _user;
        public CustomerService(AppUsersDbContext db, UserManager<AppUser> user)
        {
            _db = db;
            _user = user;
        }


        public List<AppUser> GetAll()
        {
            return _db.Users.OrderBy(m => m.Id).ToList();

        }

        /*        TAKE NOTE Role thing later
        */
        public AppUser? GetCustomerByUN(string username)
        {
            AppUser? customer = _db.Users.FirstOrDefault(
            x => x.UserName.ToUpper().Equals(username.ToUpper()));

            return customer;
        }

        public AppUser? GetCustomerById(string id)
        {

            AppUser? customer = _db.Users.FirstOrDefault(
                x => x.Id.Equals(id));
            return customer;
        }
        
       public void AddCustomer(AppUser customer)
       {
            
            _db.Users.Add(customer);
           _db.SaveChanges();

       }
       public void UpdateCustomer(AppUser customer)
       {
           _db.Users.Update(customer);
           _db.SaveChanges();
       }


/*
        var users = await(from user in _db.Users
                          join userRole in _db.UserRoles
                          on user.Id equals userRole.UserId
                          join role in _db.Roles
                          on userRole.RoleId equals role.Id
                          where role.Name == "User"
                          select user)
                                 .ToListAsync();*/


        /*        [HttpGet]
                public async Task<string?> GetCurrentUserId()
                {
                    AppUser usr = await GetCurrentUserAsync();
                    return usr?.Id;
                }*/

        /*        private Task<AppUser> GetCurrentUserAsync() => _user.GetUserAsync(HttpContext.User);
        */        /*        public AppUser?{
                 var userid = userManager.GetUserId(HttpContext.User);
                            if (userid != null)
                            {
                                AppUser user = userManager.FindByIdAsync(userid).Result;
                                return user;
                            }
                }*/

        /*            public IActionResult CustList()
                    {
                        var userList = _db.Users.ToList();
                        *//*            var roleList= _db.Roles.ToList();
                        *//*
                        foreach (var user in userList)
                        {

                        }
                        return View(userList);
                    }*/
    }
    }


    /*   private readonly CustomersInfoDbContext _context;
       public CustomerService(CustomersInfoDbContext context)
       {
           _context = context;
       }


       public List<CustomersInfo> GetAll()
       {
           return AllCustomers.OrderBy(m => m.FName).ToList();

           return _context.Customers.OrderBy(m => m.ID).ToList();
       }
       public Account? GetCustomerByUN(string username)
       {
           Account? customer = AllCustomers.FirstOrDefault(
           x => x.Username.ToUpper().Equals(username.ToUpper()));

           Account? customer = _context.Customers.FirstOrDefault(
               x => x.Username.ToUpper().Equals(username.ToUpper()));
           return customer;
       }


       public Account? GetCustomerById(int id)
       {

           Account? customer = _context.Customers.FirstOrDefault(
               x => x.ID.Equals(id));
           return customer;
       }

       public void AddCustomer(Account customer)
       {
           _context.Customers.Add(customer);
           _context.SaveChanges();
           AllCustomers.Add(customer);

       }
       public void UpdateCustomer(Account customer)
       {
           _context.Customers.Update(customer);
           _context.SaveChanges();
       }*/

