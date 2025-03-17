using Dot_net_ModelViewController.Data;
using Dot_net_ModelViewController.Interface;
using Dot_net_ModelViewController.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Dot_net_ModelViewController.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuth userAuthRepository;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext appDbContext;

        //public LoginController(AppDbContext appDbContext, IConfiguration configuration)
        //{
        //    this.appDbContext = appDbContext;
        //    _configuration = configuration;
        //}

        public LoginController(AppDbContext appDbContext, IConfiguration configuration,IUserAuth userAuthRepository)
        {
            this.userAuthRepository = userAuthRepository;
            this.appDbContext = appDbContext;
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind] User model)
        {
            User UserInfo = new User();
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token) &&
                !HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                HttpContext.Session.Remove("JWToken");
            }
            if (model.UserName.Trim() != null && model.Password.Trim() != null)
            {
                UserInfo = appDbContext.User.FirstOrDefault(x => x.UserName == model.UserName.Trim() && x.Password == model.Password.Trim());

            }
            if (UserInfo != null)
            {
                token = userAuthRepository.Authentication(model.UserName, model.Password);
                if (token == null)
                // var statusCode = HttpStatusCode.Unauthorized;
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //return Unauthorized();
                //return StatusCode;
                //  return new HttpResponseException(HttpStatusCode.Unauthorized);
                // store token in a session
                if (HttpContext.Response.StatusCode != 401)
                {
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["InvalidUser"] = "Invalid User";

            //switch (StatusCode)
            //{
            //    case HttpStatusCode.Unauthorized:
            //        return RedirectToAction("Unauthorized");
            //    case HttpStatusCode.OK:
            //        return RedirectToAction("Authorized"); // it enters this case
            //    default:
            //        return RedirectToAction("Index");
            //}
            if (HttpContext.Response.StatusCode == 401)
            {
                TempData["InvalidUser"] = "Unauthorized user";
            }
  
            return View();
        }
    }
}
