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

        [Authorize(Policy="Claim.DOB")]
        public IActionResult SecretPolicy(){
            return View("secret");
        }

        [Authorize(Roles="Admin")]
        public IActionResult SecretRole(){
            return View("secret");
        }

        public IActionResult Authenticate(){
            var myClaims = new List<Claim>(){
                new Claim(ClaimTypes.Name, "onik"),
                new Claim(ClaimTypes.Email, "onik@mail.com"),
                new Claim(ClaimTypes.DateOfBirth, "01/01/2020"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("Onik Claims", "very good claim")
            };
            var licenseClaims = new List<Claim>(){
                new Claim(ClaimTypes.Email, "onik@mail.com"),
                new Claim(ClaimTypes.DateOfBirth, "01/01/2020"),
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