using Dot_net_ModelViewController.Interface;
using Dot_net_ModelViewController.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_net_ModelViewController.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUser userRepository;
        public UserController(IUser userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> userList = userRepository.GetAllUsers().ToList();
                ViewBag.useremail = userList;

                    return View(userList);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex);

            }
        }
        [HttpPost]
        public IActionResult GetAllUsers(string? SearchName = "",string? SearchEmail ="")
        {
            try
            {
                List<User> userList = userRepository.GetAllUsers().ToList();

                if (!String.IsNullOrEmpty(SearchName) || !String.IsNullOrEmpty(SearchEmail))
                {
                    userList = userList.Where(s => s.Name.ToUpper().Contains(SearchName.ToUpper()) 
                    && s.Email.Contains(SearchEmail)).ToList();
                }

                ViewBag.useremail = userList;

                return View(userList);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex);

            }
        }
    }
}
