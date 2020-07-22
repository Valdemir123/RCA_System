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
    public class GroupController : Controller
    {
        private readonly RCAContext _context;
        public GroupController(RCAContext context)
        {
            _context = context;
        }

        //********************
        // Index
        public ActionResult Index(GroupType _GroupId)
        {
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            //
            Class_Cadaster _Cadaster = new Class_Cadaster
            {
                CompanyId = _CompanyId,
                GroupId = _GroupId,
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
                    GroupLevelItemTaxID = 0,

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
                    GroupLevelItemTaxID = 0,

                    ItemDesc = "| " + _Lgl.Name
                };
                if (_Lgl.StatusId != GroupLevelStatus.Ativo)
                {
                    _Igl.ItemDesc = "| " + _Lgl.StatusId.ToString() + "  " + _Igl.ItemDesc;
                }
                _Cadaster.CadasterLIST.Add(_Igl);

                if (_Lgl.StatusId != GroupLevelStatus.Suspenso)
                {
                    _Igl = new Class_CadasterItem()
                    {
                        TypeLine = "Cabec",

                        GroupLevelId = _Lgl.Id,
                        GroupLevelName = _Lgl.Name,
                        GroupLevelItemId = 0,
                        GroupLevelItemTaxID = 0,

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
                            GroupLevelItemTaxID = 0,

                            ItemDesc = "| " + _Lgli.Name
                        };
                        if (_Lgli.StatusId != GroupLevelItemStatus.Ativo)
                        {
                            _Igli.ItemDesc = "| " + _Lgli.StatusId.ToString() + "  " + _Igli.ItemDesc;
                        }
                        if (_Lgl.GroupId == GroupType.ACOMODACAO)
                        {
                            _Igli.ItemDesc = string.Format("{0} - No.Ocup. {1} - Prep.PCD? {2}", _Igli.ItemDesc, _Lgli.OccupantsNum, _Lgli.PCD.ToString());
                        }
                        _Cadaster.CadasterLIST.Add(_Igli);

                        if (_Lgli.StatusId != GroupLevelItemStatus.Suspenso)
                        {

                            //3o estagio
                            var _SI = from s1 in _context.Class_SeasonItem
                                      join s2 in _context.Class_Season on s1.SeasonId equals s2.Id
                                      where s1.GroupLevelItemId == _Lgli.Id
                                      orderby s2.Name
                                      select new { SeasonItemID = s1.Id, SeasonItemTAX = s1.Tax, SeasonItemNAME = s2.Name };
                            //
                            if (_SI.Count() > 0)
                            {
                                _Igli = new Class_CadasterItem()
                                {
                                    TypeLine = "Item",

                                    GroupLevelId = _Lgl.Id,
                                    GroupLevelItemId = _Lgli.Id,
                                    GroupLevelItemTaxID = 0,
                                    GroupLevelItemTaxNAME="",

                                    ItemDesc = "| " + _Lgli.Name
                                };
                                _Cadaster.CadasterLIST.Add(_Igli);
                                //
                                foreach (var _SIitem in _SI)
                                {
                                    var _Iglit = new Class_CadasterItem()
                                    {
                                        TypeLine = "Item",

                                        GroupLevelId = _Lgl.Id,
                                        GroupLevelItemId = _Lgli.Id,
                                        GroupLevelItemName = _Lgli.Name,
                                        GroupLevelItemTaxID = _SIitem.SeasonItemID,
                                        GroupLevelItemTaxNAME = _SIitem.SeasonItemNAME,
                                        GroupLevelItemTaxTAX = _SIitem.SeasonItemTAX,

                                        ItemDesc = string.Format("{0} - {1}", _SIitem.SeasonItemNAME, _SIitem.SeasonItemTAX.ToString("C2"))
                                    };
                                    _Cadaster.CadasterLIST.Add(_Iglit);
                                }
                            }

                            _Igli = new Class_CadasterItem()
                            {
                                TypeLine = "Linha",

                                GroupLevelId = 0,
                                GroupLevelItemId = 0,
                                GroupLevelItemTaxID = 0,

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
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            //
            Class_GroupLevel _GroupLevel = new Class_GroupLevel
            {
                Id = 0,
                StatusId = GroupLevelStatus.Ativo,
                CompanyId = _CompanyId,
                GroupId = _GroupId
            };

            ViewBag.GroupName = _GroupLevel.GroupId.ToString();
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
                if (_Find.Result != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _GroupId = _GroupLevel.GroupId });
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
            var _GroupLevel = await _context.Class_GroupLevel.FirstOrDefaultAsync(m => m.Id == _GroupLevelId);

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
                    if (_Find.Result != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _GroupId = _GroupLevel.GroupId });
                    }

                    _context.Update(_GroupLevel);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message, _GroupId = _GroupLevel.GroupId });
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
                PCD = GroupLevelItem_YN.Nao
            };

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.GroupType = _GroupLevel.GroupId;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)).Cast<GroupLevelItem_YN>().ToList());
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
                if (_Find.Result != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _GroupId = _GroupLevel.GroupId });
                }

                _context.Add(_GroupLevelItem);
                await _context.SaveChangesAsync();

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.GroupType = _GroupLevel.GroupId;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)).Cast<GroupLevelItem_YN>().ToList());
            return View(_GroupLevelItem);
        }
        //Edit - GroupLevelItem
        public async Task<IActionResult> EditGLI(int _GroupLevelItemId)
        {
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(_GroupLevelItemId);
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            if (_GroupLevelItem == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.GroupType = _GroupLevel.GroupId;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)).Cast<GroupLevelItem_YN>().ToList());
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
                    if (_Find.Result != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "Nome já esta cadastrado!", _GroupId = _GroupLevel.GroupId });
                    }

                    _context.Update(_GroupLevelItem);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message, _GroupId = _GroupLevel.GroupId });
                }

                //retorno Listagem
                return RedirectToAction(nameof(Index), new { _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.GroupType = _GroupLevel.GroupId;
            ViewBag.PCD_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)).Cast<GroupLevelItem_YN>().ToList());
            return View(_GroupLevel);
        }
        //Delete - GroupLevel
        public async Task<IActionResult> DeleteGLI(int _GroupLevelItemId)
        {
            var _GroupLevelItem = await _context.Class_GroupLevelItem.FindAsync(_GroupLevelItemId);
            var _GroupLevel = _context.Class_GroupLevel.Find(_GroupLevelItem.GroupLevelId);

            if (_GroupLevelItem == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!", _GroupId = _GroupLevel.GroupId });
            }

            ViewBag.GroupId = Convert.ToInt32(_GroupLevel.GroupId);
            ViewBag.GroupLevelName = _GroupLevel.Name;
            ViewBag.GroupType = _GroupLevel.GroupId;
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
        public IActionResult Error(string _Message, GroupType? _GroupId)
        {
            var _vm = new ErrorViewModel
            {
                Message = _Message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                GroupId = (GroupType)_GroupId
            };

            return View(_vm);
        }
    }
}
