using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Dtos.UserRoles;

namespace Ticket.Web.Controllers
{
    public class UserRoleController : Controller
    {
        IUserRoleService _userRoleService;
        IRoleService _roleService;
        IUserService _userService;
        public UserRoleController(IUserRoleService userRoleService, IRoleService roleService, IUserService userService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var users = await _userService.SelectedItem();
            ViewBag.Users = users.Entity;
            var roles =  await _roleService.SelectedItem();
            ViewBag.Roles = roles.Entity;
            ViewBag.UserRoles = await _userRoleService.List();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                var users = await _userService.SelectedItem();
                ViewBag.Users = users.Entity;
                var roles = await _roleService.SelectedItem();
                ViewBag.Roles = roles.Entity;
                ViewBag.Roles = await _userRoleService.List();
                return View(dto);
            }
            else
            {
                await _userRoleService.Insert(dto);
                return RedirectToAction("Create", "UserRole");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var users = await _userService.SelectedItem();
            ViewBag.Users = users.Entity;
            var roles = await _roleService.SelectedItem();
            ViewBag.Roles = roles.Entity;
            var data = await _userRoleService.GetById(id);
            var model = data.Entity;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Update(UserRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            else
            {
                await _userRoleService.Update(dto);
                return RedirectToAction("Create", "UserRole");
            }
        }
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _userRoleService.Delete(id);
            return Json(result);
        }
    }
}
