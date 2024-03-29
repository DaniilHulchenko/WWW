﻿using AutoMapper;
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
using WWW.Jobs;

namespace WWW.Controllers.Account
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly EntityBaseRepository<User> _accountrepository;
        private readonly EntityBaseRepository<User_Details> _account_det_repository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJob _backgroundJob;
        public AccountController(AccountService accountService, EntityBaseRepository<User> accountrepository, IMapper mapper, EntityBaseRepository<User_Details> account_det_repository, IBackgroundJob backgroundJob)
        {
            _backgroundJob = backgroundJob;
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



        [HttpGet, Authorize]
        public async Task<IActionResult> Profile()
        {
            int userid = int.Parse(User.FindFirst("UserId").Value);
            User user= await _accountrepository.GetValueByID(userid);
            EditViewModal model = _mapper.Map<EditViewModal>(user);;
            return View(model);
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Profile(EditViewModal model)
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
            else{   Merge<User_Details>.merge(user.Details, newuser.Details, new string[] {"UserID"}, new string[] { "Introdaction" });  }

            Merge<User>.merge(user, newuser, new string[] { "Details", "Avatar", "Role" });
            await _accountrepository.Update(user);
            await _account_det_repository.Update(user.Details);

            // logout & login
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var response = _accountService.Authenticate(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));


            model = _mapper.Map<EditViewModal>(await _accountrepository.GetValueByID(int.Parse(User.FindFirst("UserId").Value))); ;

            return RedirectToAction("Profile", model);
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> ChangePassword(EditViewModal model)
        {
            User user = await _accountrepository.GetValueByID(int.Parse(User.FindFirst("UserId").Value));
            if (HashPasswordHelper.HashPassowrd(model.OldPassword) != user.Details.Password) ModelState.AddModelError("", "Old Passwords disent match");

            if (ModelState.IsValid)
            {
                user.Details.Password = HashPasswordHelper.HashPassowrd((string)model.NewPassword);
                await _account_det_repository.Update(user.Details);
            }


            model.Email = user.Email;
            model.NickName = user.NickName;
            model.Introdaction = user.Details.Introdaction;
            return View("Profile", model);
            //return RedirectToAction("Profile",model);
        }

        public async Task<IActionResult> SessionMemory(string name, string? value) 
        {
            if (value == null)
                HttpContext.Session.Remove(name);
            else {
                HttpContext.Session.SetString(name, value);
                if(name== "City")
                    await _backgroundJob.ExecuteAsync();
            }

            return Redirect("/");
        }



    }




}


