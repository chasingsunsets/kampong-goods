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

        public List<Request> GetNotMy(string userid)
        {
            return _db.Requests.Where(m => m.UserId != userid && m.Status != "Available").ToList();

        }

        public List<Request> GetIAccepted(string userid)
        {
            return _db.Requests.Where(m=>m.Status==userid).ToList();

        }

        public List<Request> GetMyA(string userid)
        {
            return _db.Requests.Where(m => m.UserId == userid && m.Status!="Available" ).ToList();

        }

        public List<Request> GetMyNotA(string userid)
        {
            return _db.Requests.Where(m => m.UserId == userid && m.Status == "Available").ToList();

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

