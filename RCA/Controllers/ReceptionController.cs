using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCA.Data;
using RCA.Models;
using RCA.Models.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly RCAContext _context;
        public ReceptionController(RCAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
