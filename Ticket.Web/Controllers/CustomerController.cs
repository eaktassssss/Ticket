using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Dtos.Customers;

namespace Ticket.Web.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Customers = await _customerService.List();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CustomerDto customer)
        {
            

            if (!ModelState.IsValid)
                return View(customer);
            else
            {
                await _customerService.Insert(customer);
                return RedirectToAction("Create", "Customer");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            var customer = await _customerService.GetById(id);
            return View(customer.Entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            else
            {
                await _customerService.Update(customer);
                return RedirectToAction("Create", "Customer");
            }
        }
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return Json(result);
        }
    }
}
