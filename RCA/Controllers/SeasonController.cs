using Microsoft.AspNetCore.Http;
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
    public class SeasonController : Controller
    {
        private readonly RCAContext _context;
        public SeasonController(RCAContext context)
        {
            _context = context;
        }


        // GET: Channel
        public async Task<IActionResult> Index()
        {
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);

            var _Season = from s
                          in _context.Class_Season
                          where s.CompanyId == _CompanyId
                          orderby s.StatusId, s.Name
                          select s;

            ViewBag.CompanyId = _CompanyId;
            return View(await _Season.AsNoTracking().ToListAsync());
        }



        // GET: Channel/Details
        public async Task<IActionResult> PriceList(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _Season = await _context.Class_Season.FirstOrDefaultAsync(m => m.Id == id);
            if (_Season == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            ViewBag.CompanyId = _Season.CompanyId;
            return View(_Season);
        }



        // GET: Channel/Create
        public IActionResult Create(int _CompanyId)
        {
            Class_Season _Season = new Class_Season
            {
                Id = 0,
                CompanyId = _CompanyId,
                StatusId = SeasonStatus.Ativo
            };

            ViewBag.CompanyId = _CompanyId;
            return View(_Season);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,CompanyId,Name")] Class_Season _Season)
        {
            if (ModelState.IsValid)
            {
                var _Find = _context.Class_Season.FirstOrDefaultAsync(m => m.Name == _Season.Name && m.CompanyId == _Season.CompanyId && m.Id != _Season.Id);
                if (_Find.Result != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _CompanyId = _Season.CompanyId });
                }

                _context.Add(_Season);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { _CompanyId = _Season.CompanyId });
            }

            ViewBag.CompanyId = _Season.CompanyId;
            return View(_Season);
        }



        // GET: Channel/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var _Season = await _context.Class_Season.FindAsync(id);
            if (_Season == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _CompanyId = _Season.CompanyId });
            }

            ViewBag.CompanyId = _Season.CompanyId;
            return View(_Season);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,CompanyId,Name")] Class_Season _Season)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Season.StatusId = SeasonStatus.Ativo;

                    var _Find = _context.Class_Season.FirstOrDefaultAsync(m => m.Name == _Season.Name && m.CompanyId == _Season.CompanyId && m.Id != _Season.Id);
                    if (_Find.Result != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _CompanyId = _Season.CompanyId });
                    }

                    _context.Update(_Season);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message, _CompanyId = _Season.CompanyId });
                }
                return RedirectToAction(nameof(Index), new { _CompanyId = _Season.CompanyId });
            }

            ViewBag.CompanyId = _Season.CompanyId;
            return View(_Season);
        }



        // GET: Channel/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var _Season = await _context.Class_Season.FirstOrDefaultAsync(m => m.Id == id);
            if (_Season == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _CompanyId = _Season.CompanyId });
            }

            ViewBag.CompanyId = _Season.CompanyId;
            return View(_Season);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _Season = await _context.Class_Season.FindAsync(id);
            _Season.StatusId = SeasonStatus.Suspenso;

            _context.Class_Season.Update(_Season);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { _CompanyId = _Season.CompanyId });
        }



        // POST: Error Message
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string _Message, int _CompanyId)
        {
            var _vm = new ErrorViewModel
            {
                Message = _Message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                CompanyId = _CompanyId
            };

            return View(_vm);
        }
    }
}
