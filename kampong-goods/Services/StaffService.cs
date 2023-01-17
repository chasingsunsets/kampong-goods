using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;

namespace kampong_goods.Services
{
    public class StaffService
    {
        private readonly AppUsersDbContext _db;

        private readonly UserManager<AppUser> _user;
        public StaffService(AppUsersDbContext db, UserManager<AppUser> user)
        {
            _db = db;
            _user = user;
        }

        public List<StaffInfo> GetAll()
        {
            return _db.StaffInfos.OrderBy(m => m.ID).ToList();

        }

        public void AddStaff(StaffInfo staff)
        {

            _db.StaffInfos.Add(staff);
            _db.SaveChanges();

        }

        public StaffInfo? GetICById(string id)
        {

            StaffInfo? staff = _db.StaffInfos.FirstOrDefault(x => x.UserId.Equals(id));

            return staff;
        }

        public StaffInfo? GetStaffbyIC(string ic)
        {
            StaffInfo? staff = _db.StaffInfos.FirstOrDefault(
            x => x.NRIC.Equals(ic));

            return staff;
        }
        /*        public List<AppUser> GetAll()
                {
                    return _db.Users.OrderBy(m => m.Id).ToList();

                }*/

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



