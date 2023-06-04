using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.ViewModels.Account;
using WWW.Service.Helpers;
using WWW.Service.Interfaces;
using WWW.Service.Implementations;



namespace WWW.Controllers.Account
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly EntityBaseRepository<User> _accountrepository;
        private readonly EntityBaseRepository<User_Details> _account_det_repository;
        private readonly IMapper _mapper;

        public AccountController(AccountService accountService, EntityBaseRepository<User> accountrepository, IMapper mapper, EntityBaseRepository<User_Details> account_det_repository)
        {
            _account_det_repository = account_det_repository;
            _accountrepository = accountrepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == WWW.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.ErrorDescription);//!!!!!!!!!!!!!!!!!!!
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.ErrorDescription);
            }
            return View(model);
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int userid = int.Parse(User.FindFirst("UserId").Value);
            User user= await _accountrepository.GetValueByID(userid);
            RegisterViewModel model = _mapper.Map<RegisterViewModel>(user);;
            return View(model);
        }


        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(RegisterViewModel model)
        {
            User user = await _accountrepository.GetValueByID(int.Parse(User.FindFirst("UserId").Value));
            User newuser = _mapper.Map<User>(model);
            if (model.Avatar != null){
                using (var memoryStream = new MemoryStream())
                {
                    await model.Avatar.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                }}

            if (user.Details == null){   user.Details = newuser.Details;  }
            else{   Marge<User_Details>.marge(user.Details, newuser.Details, new string[] {"UserID"});  }

            Marge<User>.marge(user, newuser, new string[] { "Details", "Avatar" });
            await _accountrepository.Update(user);
            await _account_det_repository.Update(user.Details);

            // logout & login
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var response = _accountService.Authenticate(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));


            model = _mapper.Map<RegisterViewModel>(await _accountrepository.GetValueByID(int.Parse(User.FindFirst("UserId").Value))); ;

            return RedirectToAction("Profile", model);
        }


        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _accountService.ChangePassword(model);
        //        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //        {
        //            return Json(new { description = response.Description });
        //        }
        //    }
        //    var modelError = ModelState.Values.SelectMany(v => v.Errors);

        //    return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        //}

    }


    

}


