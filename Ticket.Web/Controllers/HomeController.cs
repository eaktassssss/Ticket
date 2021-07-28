using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Business.Abstract;

namespace Ticket.Web.Controllers
{
    public class HomeController : Controller
    {
        IUserService _userService;
        ITicketService _ticketService;
        public HomeController(IUserService userService,ITicketService ticketService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }
        public async Task< ActionResult>Index()
        {
            ViewBag.OpenTicket = await _ticketService.OpenTicketCount();
            ViewBag.CloseTicket = await _ticketService.CloseTicketCount();
            ViewBag.AllTicket = await _ticketService.CountAsync();
            ViewBag.Cusromers = 1550;
            return View();
        }
    }
}
