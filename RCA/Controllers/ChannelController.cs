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
    public class ChannelController : Controller
    {
        private readonly RCAContext _context;
        public ChannelController(RCAContext context)
        {
            _context = context;
        }


        //warning
        public int _CompanyId = 1;



        // GET: Channel
        public async Task<IActionResult> Index()
        {
            var _Channel = from s 
                           in _context.Class_Channel 
                           where s.CompanyId == _CompanyId 
                           orderby s.StatusId, s.TypeId, s.Name 
                           select s;

            return View(await _Channel.AsNoTracking().ToListAsync());
        }



        // GET: Channel/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _Channel = await _context.Class_Channel.FirstOrDefaultAsync(m => m.Id == id);
            if (_Channel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            return View(_Channel);
        }



        // GET: Channel/Create
        public IActionResult Create()
        {
            Class_Channel _Channel = new Class_Channel
            {
                Id = 0,
                CompanyId = _CompanyId,
                StatusId = ChannelStatus.Ativo,
                TypeId = ChannelType.RESERVA
            };

            ViewBag.ChannelType_LIST = new SelectList(Enum.GetValues(typeof(ChannelType)).Cast<ChannelType>().ToList());
            return View(_Channel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,CompanyId,TypeId,Name,Tax,Percent")] Class_Channel _Channel)
        {
            if (ModelState.IsValid)
            {
                var _Find = _context.Class_Channel.FirstOrDefaultAsync(m => m.Name == _Channel.Name && m.CompanyId == _Channel.CompanyId && m.Id != _Channel.Id);
                if (_Find != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                }

                _context.Add(_Channel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(_Channel);
        }



        // GET: Channel/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _Channel = await _context.Class_Channel.FindAsync(id);
            if (_Channel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            ViewBag.ChannelType_LIST = new SelectList(Enum.GetValues(typeof(ChannelType)).Cast<ChannelType>().ToList());
            return View(_Channel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,CompanyId,TypeId,Name,Tax,Percent")] Class_Channel _Channel)
        {
            if (id != _Channel.Id)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não é o mesmo!" });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _Channel.StatusId = ChannelStatus.Ativo;

                    var _Find = _context.Class_Channel.FirstOrDefaultAsync(m => m.Name == _Channel.Name && m.CompanyId == _Channel.CompanyId && m.Id != _Channel.Id);
                    if (_Find != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                    }

                    _context.Update(_Channel);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }

            return View(_Channel);
        }



        // GET: Channel/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _Channel = await _context.Class_Channel.FirstOrDefaultAsync(m => m.Id == id);
            if (_Channel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            return View(_Channel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _Channel = await _context.Class_Channel.FindAsync(id);
            _Channel.StatusId = ChannelStatus.Suspenso;

            _context.Class_Channel.Update(_Channel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool Class_ChannelExists(int id)
        {
            return _context.Class_Channel.Any(e => e.Id == id);
        }



        // POST: Error Message
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string _Message)
        {
            var _vm = new ErrorViewModel
            {
                Message = _Message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(_vm);
        }
    }
}
