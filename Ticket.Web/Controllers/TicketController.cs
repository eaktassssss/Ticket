using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Dtos.Tickets;

namespace Ticket.Web.Controllers
{
    public class TicketController : Controller
    {
        ITicketService _ticketService;
        ICustomerService _customerService;
        public TicketController(ITicketService ticketService, ICustomerService customerService )
        {
            _ticketService = ticketService;
            _customerService = customerService;

        }
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var response = await _ticketService.List();
            return View(response);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Urgencies = await _ticketService.DropUrgencies();
            ViewBag.Impacts = await _ticketService.DropImpacts();
            ViewBag.Customers = await _customerService.DropCustomer();
            ViewBag.Types = await _ticketService.DropTypes();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(TicketDto ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }
            else
            {

                await _ticketService.Insert(ticket);
                return RedirectToAction("Create", "Ticket");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ViewBag.Urgencies = await _ticketService.DropUrgencies();
            ViewBag.Impacts = await _ticketService.DropImpacts();
            ViewBag.Customers = await _customerService.DropCustomer();
            ViewBag.Types = await _ticketService.DropTypes();
            ViewBag.Status = await _ticketService.DropStuations();
            var user = await _ticketService.GetById(id);
            return View(user.Entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(TicketDto ticket)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _ticketService.List();
                return View(ticket);
            }
            else
            {
                await _ticketService.Update(ticket);
                return RedirectToAction("List", "Ticket");
            }
        }
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _ticketService.Delete(id);
            return Json(result);
        }
    }
}
