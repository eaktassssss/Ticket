using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Dtos.Roles;

namespace Ticket.Web.Controllers
{
    public class RoleController : Controller
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Roles = await _roleService.List();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.List();
                return View(role);
            }
            else
            {
                await _roleService.Insert(role);
                return RedirectToAction("Create", "Role");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var role = await _roleService.GetById(id);
            return View(role.Entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            else
            {
                await _roleService.Update(role);
                return RedirectToAction("Create", "Role");
            }
        }
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);
            return Json(result);
        }
    }
}
