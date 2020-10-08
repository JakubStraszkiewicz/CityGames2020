using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGames2019.Data;
using CityGames2019.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CityGames2019.Controllers
{
    public class UserController : Controller
    {
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private string currentViewUserId;

        public UserController(IStringLocalizer<HomeController> stringLocalizer, UserManager<User> userManager, ApplicationDbContext context)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<ActionResult> RenderImage(long id)
        {
            Game game = await _context.Games.FindAsync(id);

            byte[] photoBack = game.Photo;

            return File(photoBack, "image/png");
        }

        public async Task<ActionResult> RenderImageForUser(string id)
        {
            User user = _context.ApplicationUsers
                .Select(n=>n)
                .Where(n=>n.UserName.Equals(id))
                .ToList().First();

            byte[] photoBack = user.Photo;

            return File(photoBack, "image/png");
        }

        public string GetCurrentViewUsername()
        {
            string currentUserName = "";

            bool isAnyRecord = _context.Users
                    .Select(n => n)
                    .Where(n => n.Id == currentViewUserId)
                    .Any();

            if (isAnyRecord)
                currentUserName = _context.Users
                    .Select(n => n)
                    .Where(n => n.Id == currentViewUserId)
                    .First().UserName;

            return currentUserName;
        }

        public bool IsThisLoggedUserGamesView()
        {
            return currentViewUserId.Equals(_userManager.GetUserId(User));
        }

        /*
        // GET: User
        public ActionResult Index()
        {
            //return View(_context.Games.Where(game => game.CreatorId == MyId));
            string myId = _userManager.GetUserId(HttpContext.User);
            _context.Games.Where(game => game.CreatorId == myId);
            return View(_context.Games);
        }
        */

        // GET: User
        public ActionResult Index(string id)
        {
            /*
            if (id == null)
                id = _userManager.GetUserId(HttpContext.User);
            */
            this.currentViewUserId = id;
            ViewBag.GetCurrentViewUsername = new Func<string>(GetCurrentViewUsername);
            ViewBag.IsThisLoggedUserGamesView = new Func<bool>(IsThisLoggedUserGamesView);

            var games = _context.Games.Where(game => game.CreatorId == id);
            return View(games);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult FindUser(string username, string email, string name, string surname)
        {
            var users = _context.ApplicationUsers;
            List<User> searchedUsers = new List<User>();

            try
            {
                if (!String.IsNullOrEmpty(username))
                {
                    searchedUsers = users.Where(user => user.UserName.Contains(username)).ToList();
                }

                if (!String.IsNullOrEmpty(email))
                {
                    var temp = users.Where(user => user.Email.Contains(email)).ToList();
                    searchedUsers = searchedUsers.Concat(temp).ToList();
                }

                if (!String.IsNullOrEmpty(name))
                {
                    var temp = users.Where(user => user.Name.Contains(name)).ToList();
                    searchedUsers = searchedUsers.Concat(temp).ToList();
                }

                if (!String.IsNullOrEmpty(surname))
                {
                    var temp = users.Where(user => user.Surname.Contains(surname)).ToList();
                    searchedUsers = searchedUsers.Concat(temp).ToList();
                }

                searchedUsers = searchedUsers.Distinct().ToList();

                return View(searchedUsers);
            } catch (Exception e)
            {
                return View(searchedUsers);
            }
        }
    }
}