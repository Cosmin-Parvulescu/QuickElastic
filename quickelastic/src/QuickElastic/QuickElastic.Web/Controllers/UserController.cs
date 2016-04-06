using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using QuickElastic.Core.Domain;
using QuickElastic.Data;

namespace QuickElastic.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DbContext _context;

        public UserController(DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var user = await _context.Set(typeof (User)).FindAsync(id);
            if(user == null)
                return new HttpNotFoundResult();

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Lucky()
        {
            var random = new Random();

            var users = await _context.Set(typeof (User)).ToListAsync();

            var totalUsers = users.Count;
            var luckyUserIndex = random.Next(totalUsers);

            return View("Detail", users.ElementAt(luckyUserIndex));
        }
    }
}