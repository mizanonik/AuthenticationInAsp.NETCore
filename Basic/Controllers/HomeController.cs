using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }
        [Authorize]
        public IActionResult Secret(){
            return View();
        }
        public IActionResult Authenticate(){
            var myClaims = new List<Claim>(){
                new Claim(ClaimTypes.Name, "onik"),
                new Claim(ClaimTypes.Email, "onik@mail.com"),
                new Claim("Onik Claims", "very good claim")
            };
            var licenseClaims = new List<Claim>(){
                new Claim(ClaimTypes.Email, "onik@mail.com"),
                new Claim("Driving license", "A+")
            };

            var myIdentity = new ClaimsIdentity(myClaims, "My Identity");

            var licenseIdentity = new ClaimsIdentity(myClaims, "License Identity");

            var userPrinciple = new ClaimsPrincipal(new []{myIdentity, licenseIdentity});

            HttpContext.SignInAsync(userPrinciple);

            return RedirectToAction("Index");
        }
    }
}