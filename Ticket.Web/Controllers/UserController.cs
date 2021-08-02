using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Dtos.Users;

namespace Ticket.Web.Controllers
{
    public class UserController : Controller
    {

        IUserService _userService;
        IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
           
            ViewBag.Users = await _userService.List();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserCreatedDto user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _userService.List();
                return View(user);
            }
            else
            {
                await _userService.Insert(user);
                return RedirectToAction("Create", "User");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var user = await _userService.GetById(id);
            return View(user.Entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(UserUpdatedDto user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _userService.List();
                return View(user);
            }
            else
            {
                var result =await _userService.Update(user);
                if (!result.IsSuccessful)
                {
                    ViewBag.Message =result.Message;
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Create", "User");
                }
            }
        }
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Json(result);
        }
    }
}
