﻿using Microsoft.AspNetCore.Http;
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


        // GET: Channel
        public async Task<IActionResult> Index()
        {
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);

            var _Channel = from s
                           in _context.Class_Channel
                           where s.CompanyId == _CompanyId
                           orderby s.StatusId, s.TypeId, s.Name
                           select s;

            ViewBag.CompanyId = _CompanyId;
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

            ViewBag.CompanyId = _Channel.CompanyId;
            return View(_Channel);
        }



        // GET: Channel/Create
        public IActionResult Create(int _CompanyId)
        {
            Class_Channel _Channel = new Class_Channel
            {
                Id = 0,
                CompanyId = _CompanyId,
                StatusId = ChannelStatus.Ativo,
                TypeId = ChannelType.RESERVA
            };

            ViewBag.CompanyId = _CompanyId;
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
                if (_Find.Result != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _CompanyId = _Channel.CompanyId });
                }

                _context.Add(_Channel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { _CompanyId = _Channel.CompanyId });
            }

            ViewBag.CompanyId = _Channel.CompanyId;
            ViewBag.ChannelType_LIST = new SelectList(Enum.GetValues(typeof(ChannelType)).Cast<ChannelType>().ToList());
            return View(_Channel);
        }



        // GET: Channel/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var _Channel = await _context.Class_Channel.FindAsync(id);
            if (_Channel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _CompanyId = _Channel.CompanyId });
            }

            ViewBag.CompanyId = _Channel.CompanyId;
            ViewBag.ChannelType_LIST = new SelectList(Enum.GetValues(typeof(ChannelType)).Cast<ChannelType>().ToList());
            return View(_Channel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,CompanyId,TypeId,Name,Tax,Percent")] Class_Channel _Channel)
        {
           if (ModelState.IsValid)
            {
                try
                {
                    _Channel.StatusId = ChannelStatus.Ativo;

                    var _Find = _context.Class_Channel.FirstOrDefaultAsync(m => m.Name == _Channel.Name && m.CompanyId == _Channel.CompanyId && m.Id != _Channel.Id);
                    if (_Find.Result != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _CompanyId = _Channel.CompanyId });
                    }

                    _context.Update(_Channel);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message, _CompanyId = _Channel.CompanyId });
                }
                return RedirectToAction(nameof(Index), new { _CompanyId = _Channel.CompanyId });
            }

            ViewBag.CompanyId = _Channel.CompanyId;
            ViewBag.ChannelType_LIST = new SelectList(Enum.GetValues(typeof(ChannelType)).Cast<ChannelType>().ToList());
            return View(_Channel);
        }



        // GET: Channel/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var _Channel = await _context.Class_Channel.FirstOrDefaultAsync(m => m.Id == id);
            if (_Channel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _CompanyId = _Channel.CompanyId });
            }

            ViewBag.CompanyId = _Channel.CompanyId;
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

            return RedirectToAction(nameof(Index), new { _CompanyId = _Channel.CompanyId });
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
