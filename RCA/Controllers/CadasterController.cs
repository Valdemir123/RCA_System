using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RCA.Data;
using RCA.Models;
using RCA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RCA.Controllers
{
    public class CadasterController : Controller
    {
        private readonly RCAContext _context;
        public CadasterController(RCAContext context)
        {
            _context = context;
        }
        //warning
        public int _CompanyId = 1;


        //********************
        // Index
        public ActionResult Index(GroupType? _GroupId)
        {
            if (_GroupId == null)
            {
                _GroupId = GroupType.HOSPEDAGEM;
            }

            //
            Class_Cadaster _Cadaster = new Class_Cadaster
            {
                CompanyId = _CompanyId,
                GroupId = _GroupId.Value,
                GroupName = _GroupId.ToString()
            };

            //1o estagio
            var _GL = from s in _context.Class_GroupLevel
                      where s.CompanyId == _CompanyId && s.GroupId == _GroupId
                      orderby s.StatusId, s.Name
                      select s;
            //
            foreach (var _Lgl in _GL)
            {
                var _Ig = new Class_CadasterItem()
                {
                    TypeLine = "Cabec",

                    GroupLevelId = 0,
                    GroupLevelItemId = 0,
                    GroupLevelItemTaxId = 0,

                    ItemDesc = "Grupo",
                };
                _Cadaster.CadasterLIST.Add(_Ig);
                //
                var _Igl = new Class_CadasterItem()
                {
                    TypeLine = "Item",

                    GroupLevelId = _Lgl.Id,
                    GroupLevelName = _Lgl.Name,
                    GroupLevelItemId = 0,
                    GroupLevelItemTaxId = 0,

                    ItemDesc = string.Format("{0} - {1}", _Lgl.StatusId.ToString(), _Lgl.Name),
                };
                _Cadaster.CadasterLIST.Add(_Igl);

                if (_Lgl.StatusId != GroupLevelStatus.Suspenso)
                {
                    _Igl = new Class_CadasterItem()
                    {
                        TypeLine = "Cabec",

                        GroupLevelId = _Lgl.Id,
                        GroupLevelName = _Lgl.Name,
                        GroupLevelItemId = 0,
                        GroupLevelItemTaxId = 0,

                        ItemDesc = "Item do Grupo"
                    };
                    _Cadaster.CadasterLIST.Add(_Igl);
                    //

                    //2o estagio                
                    var _GLI = from s in _context.Class_GroupLevelItem
                               where s.GroupLevelId == _Lgl.Id
                               orderby s.StatusId, s.Name
                               select s;
                    //
                    foreach (var _Lgli in _GLI)
                    {
                        var _Igli = new Class_CadasterItem()
                        {
                            TypeLine = "Item",

                            GroupLevelId = _Lgl.Id,
                            GroupLevelItemId = _Lgli.Id,
                            GroupLevelItemName = _Lgli.Name,
                            GroupLevelItemTaxId = 0,

                            ItemDesc = string.Format("{0} - {1}", _Lgli.StatusId.ToString(), _Lgli.Name)
                        };
                        if (_GroupId == GroupType.HOSPEDAGEM)
                        {
                            _Igli.ItemDesc += string.Format(" - No.Ocup. {0} - Prep.PCD? {1}", _Lgli.OccupantsNum, _Lgli.PCD.ToString());
                        }
                        _Cadaster.CadasterLIST.Add(_Igli);

                        if (_Lgli.StatusId != GroupLevelItemStatus.Suspenso)
                        {

                            //3o estagio
                            var _GLIT = from s in _context.Class_GroupLevelItemTax
                                        where s.GroupLevelItemId == _Lgli.Id
                                        orderby s.Name
                                        select s;
                            //
                            if (_GLIT.Count() > 0)
                            {
                                _Igli = new Class_CadasterItem()
                                {
                                    TypeLine = "Cabec",

                                    GroupLevelId = _Lgl.Id,
                                    GroupLevelItemId = _Lgli.Id,
                                    GroupLevelItemName = _Lgli.Name,
                                    GroupLevelItemTaxId = 0,

                                    ItemDesc = "Listagem de Preço"
                                };
                                _Cadaster.CadasterLIST.Add(_Igli);
                                //
                                foreach (var _Lglit in _GLIT)
                                {
                                    var _Iglit = new Class_CadasterItem()
                                    {
                                        TypeLine = "Item",

                                        GroupLevelId = _Lgl.Id,
                                        GroupLevelItemId = _Lgli.Id,
                                        GroupLevelItemTaxId = _Lglit.Id,
                                        GroupLevelItemTaxName = _Lglit.Name,

                                        ItemDesc = string.Format("{0} - {1}", _Lglit.Name, _Lglit.Tax.ToString("C2"))
                                    };
                                    _Cadaster.CadasterLIST.Add(_Iglit);
                                }
                            }

                            _Igli = new Class_CadasterItem()
                            {
                                TypeLine = "Linha",

                                GroupLevelId = 0,
                                GroupLevelItemId = 0,
                                GroupLevelItemTaxId = 0,

                                ItemDesc = ""
                            };
                            _Cadaster.CadasterLIST.Add(_Igli);
                        }
                    }
                }
            }
            return View(_Cadaster);
        }


        //********************
        //Create - GroupLevel
        public IActionResult CreateGL(GroupType _GroupId)
        {
            Class_GroupLevel _GroupLevel = new Class_GroupLevel
            {
                Id = 0,
                StatusId = GroupLevelStatus.Ativo,
                CompanyId = _CompanyId,
                GroupId = _GroupId
            };

            ViewBag.GroupName = _GroupId.ToString();
            return View(_GroupLevel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGL([Bind("Id,StatusId,CompanyId,GroupId,Name")] Class_GroupLevel _GroupLevel)
        {
            if (ModelState.IsValid)
            {
                _GroupLevel.Name = _GroupLevel.Name.ToUpper();

                var _Find = _context.Class_GroupLevel.FirstOrDefaultAsync(m => m.Name == _GroupLevel.Name && m.CompanyId == _GroupLevel.CompanyId && m.Id != _GroupLevel.Id);
                if (_Find != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                }

                _context.Add(_GroupLevel);
                await _context.SaveChangesAsync();

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
            return View(_GroupLevel);
        }
        //Edit - GroupLevel
        public async Task<IActionResult> EditGL(int _GroupLevelId)
        {
            var _GroupLevel = await _context.Class_GroupLevel.FindAsync(_GroupLevelId);
            if (_GroupLevel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
            return View(_GroupLevel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGL(int id, [Bind("Id,StatusId,CompanyId,GroupId,Name")] Class_GroupLevel _GroupLevel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _GroupLevel.StatusId = GroupLevelStatus.Ativo;
                    _GroupLevel.Name = _GroupLevel.Name.ToUpper();

                    var _Find = _context.Class_GroupLevel.FirstOrDefaultAsync(m => m.Name == _GroupLevel.Name && m.CompanyId == _GroupLevel.CompanyId && m.Id != _GroupLevel.Id);
                    if (_Find != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                    }

                    _context.Update(_GroupLevel);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
            return View(_GroupLevel);
        }
        //Delete - GroupLevel
        public async Task<IActionResult> DeleteGL(int _GroupLevelId)
        {
            var _GroupLevel = await _context.Class_GroupLevel.FindAsync(_GroupLevelId);
            if (_GroupLevel == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
            return View(_GroupLevel);
        }
        [HttpPost, ActionName("DeleteGL")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGLConfirmed(int Id)
        {
            var _GroupLevel = await _context.Class_GroupLevel.FindAsync(Id);
            _GroupLevel.StatusId = GroupLevelStatus.Suspenso;

            _context.Class_GroupLevel.Update(_GroupLevel);
            await _context.SaveChangesAsync();

            //retorno Listagem
            return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
        }



        //********************
        //Create - GroupLevelItem
        public IActionResult CreateGLI(int _GroupLevelId)
        {
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelId);

            Class_GroupLevelItem _GroupLevelItem = new Class_GroupLevelItem
            {
                Id = 0,
                StatusId = GroupLevelItemStatus.Ativo,
                GroupLevelId = _GroupLevelId,
                OccupantsNum = 0,
                PCD = GroupLevelItemPCD.Nao
            };

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItemPCD)).Cast<GroupLevelItemPCD>().ToList());
            return View(_GroupLevelItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGLI([Bind("Id,StatusId,GroupLevelId,Name,OccupantsNum,PCD")] Class_GroupLevelItem _GroupLevelItem)
        {
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            if (ModelState.IsValid)
            {
                var _Find = _context.Class_GroupLevelItem.FirstOrDefaultAsync(m => m.Name == _GroupLevelItem.Name && m.GroupLevelId == _GroupLevelItem.GroupLevelId && m.Id != _GroupLevelItem.Id);
                if (_Find != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                }

                _context.Add(_GroupLevelItem);
                await _context.SaveChangesAsync();

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItemPCD)).Cast<GroupLevelItemPCD>().ToList());
            return View(_GroupLevelItem);
        }
        //Edit - GroupLevelItem
        public async Task<IActionResult> EditGLI(int _GroupLevelItemId)
        {
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(_GroupLevelItemId);
            if (_GroupLevelItem == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);
            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItemPCD)).Cast<GroupLevelItemPCD>().ToList());
            return View(_GroupLevelItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGLI(int id, [Bind("Id,StatusId,GroupLevelId,Name,OccupantsNum,PCD")] Class_GroupLevelItem _GroupLevelItem)
        {
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            if (ModelState.IsValid)
            {
                try
                {
                    _GroupLevelItem.StatusId = GroupLevelItemStatus.Ativo;

                    var _Find = _context.Class_GroupLevelItem.FirstOrDefaultAsync(m => m.Name == _GroupLevelItem.Name && m.GroupLevelId == _GroupLevelItem.GroupLevelId && m.Id != _GroupLevelItem.Id);
                    if (_Find != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!" });
                    }

                    _context.Update(_GroupLevelItem);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItemPCD)).Cast<GroupLevelItemPCD>().ToList());
            return View(_GroupLevel);
        }
        //Delete - GroupLevel
        public async Task<IActionResult> DeleteGLI(int _GroupLevelItemId)
        {
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(_GroupLevelItemId);
            if (_GroupLevelItem == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            return View(_GroupLevelItem);
        }
        [HttpPost, ActionName("DeleteGLI")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGLIConfirmed(int Id)
        {
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(Id);
            _GroupLevelItem.StatusId = GroupLevelItemStatus.Suspenso;

            _context.Class_GroupLevelItem.Update(_GroupLevelItem);
            await _context.SaveChangesAsync();

            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
        }


        //********************
        //Edit - GroupLevelItemTax





        //********************
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
