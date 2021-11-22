using AplikacjaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AplikacjaMVC.Controllers
{
    public class RegisterController : Controller
    {
        public RegisterController(ProjectDatabase _context)
        {
            this.Context = _context;
        }


        private ProjectDatabase Context { get; }
        //const string SessionName = "_Name";


        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginAction(Login logowanie)
        {
            string  email = logowanie.LoginEmail;
            string password = logowanie.LoginPassword;
            var StoredHash = Context.Register
                    .Where(m => m.Email == logowanie.LoginEmail)
                     .Select(m => m.Hash)
                    .SingleOrDefault();
            var StoredSalt = Context.Register
                    .Where(m => m.Email == logowanie.LoginEmail)
                     .Select(m => m.Salt)
                    .SingleOrDefault();
            static bool VerifyPassword(string password, string storedHash, string storedSalt)
            {
                var saltBytes = Convert.FromBase64String(storedSalt);
                var rfcBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
                return Convert.ToBase64String(rfcBytes.GetBytes(256)) == storedHash;

            }



            if (ModelState.IsValid)
            {
               

                {
                    var Username = Context.Register
                    .Where(m => m.Email == logowanie.LoginEmail)
                     .Select(m => m.Username)
                    .SingleOrDefault();


                    var EmailExists = Context.Register.Where(a => a.Email.Equals(email));
                    var CheckHashPassword = VerifyPassword(password, StoredHash, StoredSalt);



                    if (EmailExists != null && Username != null && CheckHashPassword == true)
                    {
                        string.IsNullOrEmpty(email);
                        HttpContext.Session.SetString("_Name", Username);
                        return View("LoginSuccess");
                    }
                    else
                    {
                        ViewBag.ErrorMessageLogin = "Either login or password are incorrect.";
                        return View("Login");
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            string username = HttpContext.Session.GetString("_Name");
            HttpContext.Session.Remove(username);
            HttpContext.Session.Clear();
            //HttpContext.Session.Abandon();
            //HttpContext.Current.Session.Abandon();
            //HttpContext.Session["_Name"] = null;

            return View("Login");
        }

        public IActionResult Index()
        {
            return View("Register");
        }

        public IActionResult Profile()
        {
            return View("LoginSuccess");
        }
        public static void GenerateSaltedHash(string password, out string hash, out string salt)
        {
            var saltBytes = new Byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            var rfcBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            hash = Convert.ToBase64String(rfcBytes.GetBytes(256));

        }


        [HttpPost]
        public IActionResult RegisterAction(Register register)
        {
            string outHash;
            string outSalt;
            if (ModelState.IsValid)
            {


                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(register.Email);
                if (match.Success)
                {
                    var EmailExists = Context.Register.Any(x => x.Email == register.Email);
                    var UsernameExists = Context.Register.Any(x => x.Username == register.Username);
                    if (EmailExists)
                    {

                        ViewBag.ErrorMessageEmail = "Email is already in use.";
                        return View("Register");
                    }
                    else if (UsernameExists)
                    {
                        ViewBag.ErrorMessageUsername = "Username is already in use.";
                        return View("Register");
                    }


                    GenerateSaltedHash(register.Password, out outHash, out outSalt);
                    Register newUser = new Register();
                    newUser.Username = register.Username;
                    newUser.Email = register.Email;
                    newUser.Password = register.Password;
                    newUser.Hash = outHash;
                    newUser.Salt = outSalt;

                    this.Context.Register.Add(newUser);
                    this.Context.SaveChanges();
                    int id = register.Id;

                    string.IsNullOrEmpty(register.Email);
                    HttpContext.Session.SetString("_Name", register.Username);

                    return View("RegisterSuccess");


                }
                else
                {
                    throw new Exception($"{register.Email} is NOT Valid Email Address");
                }

               

            }
            
            ViewBag.ErrorMessage = "One of the fields is empty, please enter the values in all boxes";
            return View("Register");


        }






        
    }
}
