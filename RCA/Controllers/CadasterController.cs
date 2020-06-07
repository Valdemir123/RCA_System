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



        // GET: Cadaster
        public ActionResult Index(GroupType? _GroupId)
        {
            if (_GroupId == null)
            {
                _GroupId = GroupType.HOSPEDAGEM;
            }

            //
            var _Cadaster = new Class_Cadaster();
            _Cadaster.CompanyId = _CompanyId;
            _Cadaster.GroupId = _GroupId.Value;
            _Cadaster.GroupName = _GroupId.ToString();

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

                //2o estagio                
                var _GLI = from s in _context.Class_GroupLevelItem
                           where s.GroupLevelId == _Lgl.Id
                           orderby s.StatusId, s.Name
                           select s;
                //
                foreach (var _Lgli in _GLI)
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
                    var _Igli = new Class_CadasterItem()
                    {
                        TypeLine = "Item",

                        GroupLevelId = _Lgl.Id,
                        GroupLevelItemId = _Lgli.Id,
                        GroupLevelItemName = _Lgli.Name,
                        GroupLevelItemTaxId = 0,

                        ItemDesc = string.Format("{0} - {1}", _Lgli.StatusId.ToString(), _Lgli.Name)                        
                    };
                    if(_GroupId == GroupType.HOSPEDAGEM)
                    {
                        var _Pcd = "Não";
                        if (_Lgli.PCD) { _Pcd = "Sim"; }
                        _Igli.ItemDesc += string.Format(" - No.Ocup. {0} - PCD? {1}", _Lgli.OccupantsNum, _Pcd);
                    }
                    _Cadaster.CadasterLIST.Add(_Igli);

                    //3o estagio
                    var _GLIT = from s in _context.Class_GroupLevelItemTax
                                where s.GroupLevelItemId == _Lgli.Id
                                orderby s.Name
                                select s;
                    //
                    if (_GLIT.Count() >0)
                    {
                        _Igli = new Class_CadasterItem()
                        {
                            TypeLine = "Cabec",

                            GroupLevelId = _Lgl.Id,
                            GroupLevelItemId = _Lgli.Id,
                            GroupLevelItemName = _Lgli.Name,
                            GroupLevelItemTaxId = 0,

                            ItemDesc="Listagem de Preço"
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

                        ItemDesc=""
                    };
                    _Cadaster.CadasterLIST.Add(_Igli);
                }
            }
            return View(_Cadaster);
        }



        //Create - GroupLevel
        // GET: Create
        public IActionResult CreateGL(GroupType _GroupId)
        {
            Class_GroupLevel _GroupLevel = new Class_GroupLevel();

            _GroupLevel.Id = 0;
            _GroupLevel.StatusId = GroupLevelStatus.Ativo;
            _GroupLevel.CompanyId = _CompanyId;
            _GroupLevel.GroupId = _GroupId;

            ViewBag.GroupName = _GroupId.ToString();
            return View(_GroupLevel);
        }
        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGL([Bind("Id,StatusId,CompanyId,GroupId,Name")] Class_GroupLevel _GroupLevel)
        {
            _GroupLevel.Name = _GroupLevel.Name.ToUpper();

            if (ModelState.IsValid)
            {
                _context.Add(_GroupLevel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), _GroupLevel.GroupId);
            }

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
            return View(_GroupLevel);
        }
        //Create - GroupLevelItem
        // GET: Create
        public IActionResult CreateGLI(int _GroupLevelId)
        {
            Class_GroupLevelItem _GroupLevelItem = new Class_GroupLevelItem();

            _GroupLevelItem.Id = 0;
            _GroupLevelItem.StatusId = GroupLevelItemStatus.Ativo;
            _GroupLevelItem.GroupLevelId = _GroupLevelId;
            _GroupLevelItem.OccupantsNum = 0;
            _GroupLevelItem.PCD = false;

            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelId);

            ViewBag.GroupId = Convert.ToInt32( _GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            return View(_GroupLevelItem);
        }
        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGLI([Bind("Id,StatusId,GroupLevelId,Name,OccupantsNum,PCD")] Class_GroupLevelItem _GroupLevelItem)
        {
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            _GroupLevelItem.Name = _GroupLevelItem.Name.ToUpper();

            if (ModelState.IsValid)
            {
                _context.Add(_GroupLevelItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), _GroupLevel.Id);
            }

            ViewBag.GroupId = _GroupLevel.GroupId;
            ViewBag.GroupLevelName = _GroupLevel.Name;
            return View(_GroupLevelItem);
        }



        //Edit - GroupLevel
        //Edit - GroupLevelItem
        //Edit - GroupLevelItemTax


        //Delete - GroupLevel
        //Delete - GroupLevelItem



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
