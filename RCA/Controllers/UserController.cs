using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class UserController : Controller
    {
        private readonly RCAContext _context;
        public UserController(RCAContext context)
        {
            _context = context;
        }


        // GET: User
        public ActionResult Index(int? _CompanyId)
        {
            if(_CompanyId == null || _CompanyId == 0)
            {
                _CompanyId = -1;
            }
            var _View = new List<Class_UserLIST>();

            var _Index = from s in _context.Class_Company
                         where s.StatusId == CompanyStatus.Ativo && (s.Id == _CompanyId || _CompanyId == -1)
                         orderby s.Name
                         select new { s.Id, s.Name };

            foreach (var _ItemC in _Index)
            {

                var _CompanyItem = new Class_UserLIST()
                {
                    CompanyId = _ItemC.Id,
                    CompanyName = _ItemC.Name
                };

                var _User = from s in _context.Class_User
                            where s.CompanyId == _ItemC.Id
                            orderby s.StatusId, s.TypeAccessId, s.UserName
                            select s;
                foreach (var _ItemU in _User)
                {
                    var _UserItem = new Class_UserITEM()
                    {
                        Id = _ItemU.Id,
                        StatusId = _ItemU.StatusId,
                        UserName = _ItemU.UserName,
                        Name = _ItemU.Name,
                        TypeAccessId = _ItemU.TypeAccessId
                    };

                    _CompanyItem.UserLIST.Add(_UserItem);
                }

                _View.Add(_CompanyItem);
            }

            var _CompanyLIST = from s in _context.Class_Company
                               where s.StatusId == CompanyStatus.Ativo
                               orderby s.Name
                               select new { s.Id, s.Name };
            ViewBag.Company_LIST = new SelectList(_CompanyLIST, "Id", "Name");
            return View(_View);
        }



        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _User = await _context.Class_User.FirstOrDefaultAsync(m => m.Id == id);
            if (_User == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            var _Company = await _context.Class_Company.FirstOrDefaultAsync(m => m.Id == _User.CompanyId);
            ViewBag.CompanyName = _Company.Name;
            return View(_User);
        }



        // GET: User/Create
        public IActionResult Create(int? _CompanyId)
        {
            if(_CompanyId == null) { _CompanyId = -1; }

            Class_User _User = new Class_User
            {
                Id = 0,
                StatusId = UserStatus.Ativo,
                CompanyId = (int)_CompanyId,
                TypeAccessId = UserTypeAccess.User,
                Password = "Inicial"
            };

            var _CompanyLIST = from s in _context.Class_Company
                               where s.StatusId == CompanyStatus.Ativo
                               orderby s.Name
                               select new { s.Id, s.Name };
            ViewBag.UserCompany_LIST = new SelectList(_CompanyLIST, "Id", "Name");
            ViewBag.UserTypeAcess_LIST = new SelectList(Enum.GetValues(typeof(UserTypeAccess)).Cast<UserTypeAccess>().ToList());
            return View(_User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,CompanyId,TypeAccessId,Name,Email,Phone,UserName,Password")] Class_User _User)
        {
            if (ModelState.IsValid)
            {
                _User.Email = _User.Email.ToLower();
                _User.UserName = _User.UserName.ToUpper();
                if (_User.CompanyId != -1 && _User.TypeAccessId == UserTypeAccess.Master)
                {
                    _User.TypeAccessId = UserTypeAccess.Admin;
                }
                else if (_User.CompanyId == -1 && _User.TypeAccessId != UserTypeAccess.Master)
                {
                    _User.TypeAccessId = UserTypeAccess.Master;
                }
                var _Find = await _context.Class_User.FirstOrDefaultAsync(m => m.UserName == _User.UserName && m.Id != _User.Id);
                if (_Find != null)
                {
                    return RedirectToAction(nameof(Error), new { _Message = "(UserName) já está cadastrado para " + _Find.Name });
                }

                _context.Add(_User);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var _CompanyLIST = from s in _context.Class_Company
                               where s.StatusId == CompanyStatus.Ativo
                               orderby s.Name
                               select new { s.Id, s.Name };
            ViewBag.UserCompany_LIST = new SelectList(_CompanyLIST, "Id", "Name");
            ViewBag.UserTypeAcess_LIST = new SelectList(Enum.GetValues(typeof(UserTypeAccess)).Cast<UserTypeAccess>().ToList());
            return View(_User);
        }



        // GET: User/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _User = await _context.Class_User.FindAsync(id);
            if (_User == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            var _CompanyLIST = from s in _context.Class_Company
                               where s.StatusId == CompanyStatus.Ativo
                               orderby s.Name
                               select new { s.Id, s.Name };
            ViewBag.UserCompany_LIST = new SelectList(_CompanyLIST, "Id", "Name");
            ViewBag.UserTypeAcess_LIST = new SelectList(Enum.GetValues(typeof(UserTypeAccess)).Cast<UserTypeAccess>().ToList());
            return View(_User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,CompanyId,TypeAccessId,Name,Email,Phone,UserName,Password")] Class_User _User)
        {
            if (id != _User.Id)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não é o mesmo!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _User.StatusId = UserStatus.Ativo;
                    _User.Email = _User.Email.ToLower();
                    _User.UserName = _User.UserName.ToUpper();
                    if (_User.CompanyId != -1 && _User.TypeAccessId == UserTypeAccess.Master)
                    {
                        _User.TypeAccessId = UserTypeAccess.Admin;
                    }
                    else if (_User.CompanyId == -1 && _User.TypeAccessId != UserTypeAccess.Master)
                    {
                        _User.TypeAccessId = UserTypeAccess.Master;
                    }

                    var _Find = await _context.Class_User.FirstOrDefaultAsync(m => m.UserName == _User.UserName && m.Id != _User.Id);
                    if (_Find != null)
                    {
                        return RedirectToAction(nameof(Error), new { _Message = "(UserName) já está cadastrado para " + _Find.Name });
                    }

                    _context.Update(_User);
                    await _context.SaveChangesAsync();
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { _Message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }

            var _CompanyLIST = from s in _context.Class_Company
                               where s.StatusId == CompanyStatus.Ativo
                               orderby s.Name
                               select new { s.Id, s.Name };
            ViewBag.UserCompany_LIST = new SelectList(_CompanyLIST, "Id", "Name");
            ViewBag.UserTypeAcess_LIST = new SelectList(Enum.GetValues(typeof(UserTypeAccess)).Cast<UserTypeAccess>().ToList());
            return View(_User);
        }



        // GET: User/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não informado!" });
            }
            var _User = await _context.Class_User.FirstOrDefaultAsync(m => m.Id == id);
            if (_User == null)
            {
                return RedirectToAction(nameof(Error), new { _Message = "Id não encontrado!" });
            }

            var _Company = await _context.Class_Company.FirstOrDefaultAsync(m => m.Id == _User.CompanyId);
            ViewBag.CompanyName = _Company.Name;
            return View(_User);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _User = await _context.Class_User.FindAsync(id);
            _User.StatusId = UserStatus.Suspenso;

            _context.Class_User.Update(_User);
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
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(_vm);
        }
    }
}
