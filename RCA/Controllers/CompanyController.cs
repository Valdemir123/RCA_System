using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCA.Data;
using RCA.Models;
using RCA.Models.ViewModels;

namespace RCA.Controllers
{
    public class CompanyController : Controller
    {
        private readonly RCAContext _context;
        public CompanyController(RCAContext context)
        {
            _context = context;
        }



        // GET: Company
        public async Task<IActionResult> Index()
        {
            var _Company = from s in _context.Class_Company orderby s.StatusId, s.Name select s;

            return View(await _Company.AsNoTracking().ToListAsync());
        }



        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _Company = await _context.Class_Company.FirstOrDefaultAsync(m => m.Id == id);
            if (_Company == null)
            {
                return NotFound();
            }

            return View(_Company);
        }



        // GET: Company/Create
        public IActionResult Create()
        {
            Class_Company _Company = new Class_Company();

            _Company.Country = CompanyCountry.Brasil.ToString();
            _Company.Id = 0;
            _Company.StatusId = CompanyStatus.Ativo;

            ViewBag.Country_LIST = new SelectList(Enum.GetValues(typeof(CompanyCountry)).Cast<CompanyCountry>().ToList());

            return View(_Company);
        }
        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,Name,CNPJ,Site,ContactName,Phone1,Phone2,Email,PostalCode,Address,Complement,City,State,Country")] Class_Company _Company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_Company);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(_Company);
        }



        // GET: Company/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var _Company = await _context.Class_Company.FindAsync(id);
            if (_Company == null)
            {
                return NotFound();
            }

            ViewBag.Country_LIST = new SelectList(Enum.GetValues(typeof(CompanyCountry)).Cast<CompanyCountry>().ToList());
            _Company.StatusId = CompanyStatus.Ativo;

            return View(_Company);
        }
        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,Name,CNPJ,Site,ContactName,Phone1,Phone2,Email,PostalCode,Address,Complement,City,State,Country")] Class_Company _Company)
        {
            if (id != _Company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_Company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_CompanyExists(_Company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_Company);
        }



        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _Company = await _context.Class_Company.FirstOrDefaultAsync(m => m.Id == id);
            if (_Company == null)
            {
                return NotFound();
            }

            return View(_Company);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _Company = await _context.Class_Company.FindAsync(id);
            _Company.StatusId = CompanyStatus.Suspenso;

            _context.Class_Company.Update(_Company);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool Class_CompanyExists(int id)
        {
            return _context.Class_Company.Any(e => e.Id == id);
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
