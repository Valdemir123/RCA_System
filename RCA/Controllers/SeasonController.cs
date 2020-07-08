using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCA.Data;
using RCA.Models;
using RCA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

            return View(await _Season.AsNoTracking().ToListAsync());
        }



        // GET: Price LIST
        public IActionResult PriceList(int _SeasonId)
        {
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);
            var _SeasonREL = new List<Class_SeasonREL>();

            //GroupLevel
            var _GroupLevel = from s in _context.Class_GroupLevel
                              where s.CompanyId == _CompanyId && s.StatusId == GroupLevelStatus.Ativo
                              orderby s.GroupId, s.Name
                              select s;
            foreach (var _GL in _GroupLevel)
            {
                var _Item = new Class_SeasonREL()
                {
                    TypeLine = "",
                    GroupLevelNAME = string.Format("{0} - {1}", _GL.GroupId.ToString(), _GL.Name)
                };
                _SeasonREL.Add(_Item);

                //GroupLevelItem
                var _GroupLevelItem = from s in _context.Class_GroupLevelItem
                                      where s.GroupLevelId == _GL.Id && s.StatusId == GroupLevelItemStatus.Ativo
                                      orderby s.Name
                                      select s;
                foreach (var _GLI in _GroupLevelItem)
                {
                    int _ID = 0;
                    double _TAX = 0;

                    var _SeasonItem = _context.Class_SeasonItem.FirstOrDefault(m => m.GroupLevelItemId == _GLI.Id && m.SeasonId == _SeasonId);
                    if (_SeasonItem != null)
                    {
                        _ID = _SeasonItem.Id;
                        _TAX = _SeasonItem.Tax;
                    }
                    _Item = new Class_SeasonREL()
                    {
                        TypeLine = "Item",
                        GroupLevelItemNAME = _GLI.Name,
                        GroupLevelItemID = _GLI.Id,

                        SeasonID = _SeasonId,

                        SeasonItemID = _ID,
                        SeasonItemTAX = _TAX
                    };
                    _SeasonREL.Add(_Item);
                }
            }

            var _Season = _context.Class_Season.FirstOrDefaultAsync(m => m.Id == _SeasonId);
            ViewBag.SeasonNAME = _Season.Result.Name;
            ViewBag.SeasonId = _Season.Result.Id;

            var _SeasonLIST = from s in _context.Class_Season
                              where s.StatusId == SeasonStatus.Ativo && s.Id != _SeasonId
                              orderby s.Name
                              select new { s.Id, s.Name };
            ViewBag.SeasonLIST = new SelectList(_SeasonLIST, "Id", "Name");
            ViewBag.SeasonCopyId = 0;
            ViewBag.PercentAdd = 0.0;
            return View(_SeasonREL);
        }
        public async Task<IActionResult> PriceEdit(int _SeasonId, int _GroupLevelItemId, int _SeasonItemId)
        {
            var _SeasonREL = new Class_SeasonREL();
            //
            var _Season = await _context.Class_Season.FindAsync(_SeasonId);
            _SeasonREL.SeasonID = _Season.Id;
            _SeasonREL.SeasonNAME = _Season.Name;
            _SeasonREL.SeasonItemID = 0;
            _SeasonREL.SeasonItemTAX = 0;
            //
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(_GroupLevelItemId);
            _SeasonREL.GroupLevelItemNAME = _GroupLevelItem.Name;
            _SeasonREL.GroupLevelItemID = _GroupLevelItem.Id;
            //
            var _GroupLevel = await _context.Class_GroupLevel.FindAsync(_GroupLevelItem.GroupLevelId);
            _SeasonREL.GroupLevelNAME = string.Format("{0} - {1}", _GroupLevel.GroupId.ToString(), _GroupLevel.Name);
            //
            if (_SeasonItemId != 0)
            {
                var _SeasonItem = await _context.Class_SeasonItem.FindAsync(_SeasonItemId);
                _SeasonREL.SeasonItemID = _SeasonItem.Id;
                _SeasonREL.SeasonItemTAX = (double) _SeasonItem.Tax;
            }
            //
            return View(_SeasonREL);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PriceEdit(int _SeasonId, [Bind("TypeLine,GroupLevelNAME,GroupLevelItemNAME,GroupLevelItemID,SeasonNAME,SeasonID,SeasonItemID,SeasonItemTAX")] Class_SeasonREL _SeasonREL)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _SeasonItem = new Class_SeasonItem()
                    {
                        Id = _SeasonREL.SeasonItemID,
                        SeasonId = _SeasonREL.SeasonID,
                        GroupLevelItemId = _SeasonREL.GroupLevelItemID,
                        Tax = _SeasonREL.SeasonItemTAX
                    };
                    //
                    if (_SeasonItem.Id == 0)
                    {
                        _context.Add(_SeasonItem);
                    }
                    else
                    {
                        _context.Update(_SeasonItem);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }
                return RedirectToAction(nameof(PriceList), new { _SeasonId = _SeasonREL.SeasonID });
            }
            //
            return View(_SeasonREL);
        }
        public IActionResult PriceCopy(int _SeasonId, int _SeasonCopyId, int _PercentAdd)
        {
            if (_SeasonId != 0 && _SeasonCopyId != 0)
            {
                var _SeasonItemCopy = from s in _context.Class_SeasonItem
                                      where s.SeasonId == _SeasonCopyId
                                      orderby s.GroupLevelItemId
                                      select s;
                foreach (var _Item in _SeasonItemCopy)
                {
                    var _Find = _context.Class_SeasonItem.FirstOrDefault(m => m.SeasonId == _SeasonId && m.GroupLevelItemId == _Item.GroupLevelItemId);
                    if (_Find == null)
                    {
                        _Find = new Class_SeasonItem()
                        {
                            Id = 0,
                            SeasonId = _SeasonId,
                            GroupLevelItemId = _Item.GroupLevelItemId
                        };
                    }
                    _Find.Tax = Math.Round(_Item.Tax * (1 + Convert.ToDouble(_PercentAdd) / 100), 1);
                    //
                    if (_Find.Id == 0)
                    {
                        _context.Add(_Find);
                    }
                    else
                    {
                        _context.Update(_Find);
                    }
                }
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(PriceList), new { _SeasonId = _SeasonId });
        }


        // GET: Create
        public IActionResult Create()
        {
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);

            Class_Season _Season = new Class_Season
            {
                Id = 0,
                CompanyId = _CompanyId,
                StatusId = SeasonStatus.Ativo
            };

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
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                }

                _context.Add(_Season);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(_Season);
        }



        // GET: Edit
        public async Task<IActionResult> Edit(int _SeasonId)
        {
            var _Season = await _context.Class_Season.FindAsync(_SeasonId);
            if (_Season == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            return View(_Season);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int _SeasonId, [Bind("Id,StatusId,CompanyId,Name")] Class_Season _Season)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Season.StatusId = SeasonStatus.Ativo;

                    var _Find = _context.Class_Season.FirstOrDefaultAsync(m => m.Name == _Season.Name && m.CompanyId == _Season.CompanyId && m.Id != _Season.Id);
                    if (_Find.Result != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                    }

                    _context.Update(_Season);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }

            return View(_Season);
        }



        // GET: Delete
        public async Task<IActionResult> Delete(int _SeasonId)
        {
            var _Season = await _context.Class_Season.FirstOrDefaultAsync(m => m.Id == _SeasonId);
            if (_Season == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            return View(_Season);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int _SeasonId)
        {
            var _Season = await _context.Class_Season.FindAsync(_SeasonId);
            _Season.StatusId = SeasonStatus.Suspenso;

            _context.Class_Season.Update(_Season);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // POST: Error Message
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string _Message)
        {
            var _vm = new ErrorViewModel
            {
                Message = _Message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            return View(_vm);
        }
    }
}
