using kampong_goods.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Services
{
	public class RequestService
	{
        private readonly AppUsersDbContext _db;

        private readonly UserManager<AppUser> _user;
        public RequestService(AppUsersDbContext db, UserManager<AppUser> user)
        {
            _db = db;
            _user = user;
        }

        public List<Request> GetAll()
        {
            return _db.Requests.OrderBy(m => m.ID).ToList();

        }


        public Request? GetRequestById(int id)
        {

            Request? request = _db.Requests.FirstOrDefault(x => x.ID.Equals(id));

            //nv get all
            return request;
        }


        public void AddRequest(Request request)
        {
            _db.Requests.Add(request);
            _db.SaveChanges();
        }



        public void UpdateRequest(Request request)
        {
            _db.Requests.Update(request);
            _db.SaveChanges();
        }

        public void DeleteRequest(Request request)
        {
            _db.Requests.Remove(request);
            _db.SaveChanges();
        }


    }
}

