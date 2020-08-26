using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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
    public class ReceptionController : Controller
    {
        private readonly RCAContext _context;
        public ReceptionController(RCAContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            //
            var _Reception = new List<Class_ReceptionBOOK>();
            //
            //
            var _GroupLevel = from s in _context.Class_GroupLevel
                              where s.CompanyId == _CompanyId && s.GroupId == GroupType.ACOMODACAO && s.StatusId == GroupLevelStatus.Ativo
                              orderby s.Name
                              select s;
            foreach (var _Level in _GroupLevel)
            {
                var _GLi = new Class_ReceptionBOOK()
                {
                    LevelID = _Level.Id,
                    LevelNAME = _Level.Name
                };

                //
                var _GroupLevelItem = from s in _context.Class_GroupLevelItem
                                      where s.GroupLevelId == _Level.Id && s.StatusId != GroupLevelItemStatus.Suspenso
                                      orderby s.Name
                                      select s;
                foreach (var _Item in _GroupLevelItem)
                {
                    var _GLIi = new Class_ReceptionBOOKItem()
                    {
                        Item_Id = _Item.Id,
                        Item_Name = _Item.Name,

                        Reserve_Id = 0,
                        Reserve_StatusId = 0,
                        Reserve_StatusName = "",
                        Reserve_Format = string.Format("Máximo {0} Ocupantes", _Item.OccupantsNum),
                        Reserve_DateOut = "",

                        Reserve_GuestName = "",
                        Reserve_GuestPhone = ""
                    };
                    if (_Item.PCD.Equals(GroupLevelItem_YN.Sim))
                    {
                        _GLIi.Item_Name += string.Format(", PCD? {0}", _Item.PCD.ToString());
                    }

                    //
                    var _Book = _context.Class_Book.FirstOrDefault(s => s.GroupLevelItemId == _Item.Id && (s.StatusId == BookStatus.EmUso || s.StatusId == BookStatus.Reservado));
                    if (_Book != null)
                    {
                        _GLIi.Reserve_Id = _Book.Id;
                        _GLIi.Reserve_StatusId = _Book.StatusId.GetHashCode();
                        _GLIi.Reserve_StatusName = string.Format("( {0} )", _Book.StatusId.ToString());
                        _GLIi.Reserve_Format = string.Format("{0} Adultos", _Book.Book_AdultsNum);
                        if (_Book.Book_KidsNum != 0) { _GLIi.Reserve_Format += string.Format(", {0} Crianças", _Book.Book_KidsNum); }
                        if (_Book.Book_PCD == GroupLevelItem_YN.Sim) { _GLIi.Reserve_Format += string.Format(", PCD? {0}", _Book.Book_PCD); }
                        if (_Book.Book_PET == GroupLevelItem_YN.Sim) { _GLIi.Reserve_Format += string.Format(", PET? {0}", _Book.Book_PET); }
                        _GLIi.Reserve_DateOut = _Book.Book_DateOut.ToString("dd/MM/yyyy");
                        _GLIi.Reserve_GuestName = "???";
                        _GLIi.Reserve_GuestPhone = "";

                        var _Guest = _context.Class_Guest.FirstOrDefault(s => s.CPF == _Book.GuestCPF);
                        if (_Guest != null)
                        {
                            _GLIi.Reserve_GuestName = _Guest.Name;
                            _GLIi.Reserve_GuestPhone = string.Format("{0} ({1})", _Guest.Phone1, _Guest.City);
                        }

                        //
                        var _Entreteniment = from s in _context.Class_BookItem
                                             join r in _context.Class_GroupLevelItem on s.GroupLevelItemId equals r.Id
                                             where s.BookId == _Book.Id && s.StatusId == BookItemStatus.Reservado
                                             orderby s.DateConsume
                                             select new { s.DateConsume, r.Name };
                        foreach (var _Tour in _Entreteniment)
                        {
                            var _ItemTour = new Class_ReceptionENTRETENIMENT()
                            {
                                DayTime = string.Format("d{0} {1}", _Tour.DateConsume.ToString("dd"), _Tour.DateConsume.ToString("HH:mm")),
                                Name = _Tour.Name
                            };
                            _GLIi.Entreteniment_LIST.Add(_ItemTour);
                        }
                    }
                    _GLi.BookItem_LIST.Add(_GLIi);
                }

                _Reception.Add(_GLi);
            }

            //
            return View(await _Reception.ToAsyncEnumerable().ToList());
        }



        // CHECKin
        public IActionResult CHECKin(int _RoomID)
        {
            var _GroupLevelItem = _context.Class_GroupLevelItem.FirstOrDefault(s => s.Id == _RoomID);
            var _GroupLevel = _context.Class_GroupLevel.FirstOrDefault(s => s.Id == _GroupLevelItem.GroupLevelId);
            var _Checkin = new Class_Checkin()
            {
                GroupLevelItem_Id = _RoomID,
                GroupLevelItem_Name = string.Format("{0} - {1}", _GroupLevel.Name, _GroupLevelItem.Name),

                Season_Id = 0,

                Channel_Id = 0,
                Channel_Code = "",
                Channel_Percent = 0,
                Channel_Tax = 0,

                Book_DateIn = DateTime.Today.ToString("dd/MM/yyyy"),
                Book_DateOut = DateTime.Today.AddDays(5).ToString("dd/MM/yyyy"),
                Book_AdultsNum = _GroupLevelItem.OccupantsNum,
                Book_KidsNum = 0,
                Book_PCD = _GroupLevelItem.PCD,
                Book_PET = GroupLevelItem_YN.Nao,

                Guest_CPF = "",
                Guest_Name = "",
                Guest_Email = "",
                Guest_Phone1 = "",
                Guest_Phone2 = "",

                Guest_PostalCode = "",
                Guest_Address = "",
                Guest_Complement = "",
                Guest_City = "",
                Guest_State = "",
                Guest_Country = "Brasil"
            };
            //
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            //
            ViewBag.Season_LIST = from s in _context.Class_Season
                                  where s.StatusId == SeasonStatus.Ativo && s.CompanyId == _CompanyId
                                  orderby s.Name
                                  select new { s.Id, s.Name };
            //
            ViewBag.Channel_LIST = from s in _context.Class_Channel
                                   where s.CompanyId == _CompanyId && s.StatusId == ChannelStatus.Ativo
                                   orderby s.TypeId, s.Name
                                   select new { s.Id, s.Name };
            //
            ViewBag.yn_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)));
            //
            return View(_Checkin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CHECKin(Class_Checkin _Checkin)
        {
            if (ModelState.IsValid)
            {
                // Guest

                var _Find = _context.Class_Guest.Where(m => m.CPF == _Checkin.Guest_CPF).FirstOrDefault();
                if (_Find == null)
                {
                    _Find = new Class_Guest()
                    {
                        CPF = _Checkin.Guest_CPF,
                        Name = _Checkin.Guest_Name,
                        Phone1 = _Checkin.Guest_Phone1,
                        Phone2 = _Checkin.Guest_Phone2,
                        Email = _Checkin.Guest_Email,
                        PostalCode = _Checkin.Guest_PostalCode,
                        Address = _Checkin.Guest_Address,
                        Complement = _Checkin.Guest_Complement,
                        City = _Checkin.Guest_City,
                        State = _Checkin.Guest_State,
                        Country = _Checkin.Guest_Country
                    };
                    _context.Add(_Find);
                }
                else
                {
                    _Find.CPF = _Checkin.Guest_CPF;
                    _Find.Name = _Checkin.Guest_Name;
                    _Find.Phone1 = _Checkin.Guest_Phone1;
                    _Find.Phone2 = _Checkin.Guest_Phone2;
                    _Find.Email = _Checkin.Guest_Email;
                    _Find.PostalCode = _Checkin.Guest_PostalCode;
                    _Find.Address = _Checkin.Guest_Address;
                    _Find.Complement = _Checkin.Guest_Complement;
                    _Find.City = _Checkin.Guest_City;
                    _Find.State = _Checkin.Guest_State;
                    _Find.Country = _Checkin.Guest_Country;
                    _context.Update(_Find);
                }
                _context.SaveChanges();

                // Book
                var _Book = _context.Class_Book.Where(m => m.GroupLevelItemId == _Checkin.GroupLevelItem_Id && m.StatusId == BookStatus.EmUso).FirstOrDefault();
                if (_Book == null)
                {
                    _Book = new Class_Book
                    {
                        Id = 0
                    };
                }
                _Book.StatusId = BookStatus.EmUso;
                _Book.GroupLevelItemId = _Checkin.GroupLevelItem_Id;
                _Book.SeasonId = _Checkin.Season_Id;
                _Book.ChannelId = _Checkin.Channel_Id;
                _Book.Channel_Code = _Checkin.Channel_Code;
                _Book.Channel_Tax = _Checkin.Channel_Tax;
                _Book.Channel_Percent = _Checkin.Channel_Percent;
                _Book.Book_DateIn = DateTime.ParseExact(_Checkin.Book_DateIn, "dd/MM/yyyy", null);
                _Book.Book_DateOut = DateTime.ParseExact(_Checkin.Book_DateOut, "dd/MM/yyyy", null);
                _Book.Book_AdultsNum = _Checkin.Book_AdultsNum;
                _Book.Book_KidsNum = _Checkin.Book_KidsNum;
                _Book.Book_PCD = _Checkin.Book_PCD;
                _Book.Book_PET = _Checkin.Book_PET;
                _Book.GuestCPF = _Checkin.Guest_CPF;

                if (_Book.Id == 0)
                {
                    _context.Add(_Book);
                }
                else
                {
                    _context.Update(_Book);
                }
                _context.SaveChanges();
                _Book = _context.Class_Book.Where(m => m.GroupLevelItemId == _Checkin.GroupLevelItem_Id && m.StatusId == BookStatus.EmUso).FirstOrDefault();

                //Services
                var _VlTotal = _Checkin.Book_PayTax * _Checkin.Book_Days;
                var _VlDiscount = _VlTotal * ((double)_Checkin.Book_PayDiscount / 100);
                var _VlFinal = _VlTotal - _VlDiscount - _Checkin.Book_PayCash;
                var _BookItem = new Class_BookItem()
                {
                    Id = 0,
                    StatusId = BookItemStatus.Consumido,

                    BookId = _Book.Id,
                    DateConsume = _Book.Book_DateIn,
                    GroupLevelItemId = _Book.GroupLevelItemId,

                    SeasonDesciption = "Diárias de Hospedagem",
                    SeasonUnit = _Checkin.Book_Days,
                    SeasonValue = _Checkin.Book_PayTax,

                    SeasonDiscountValue = _VlDiscount,
                    SeasonDiscountPercent = _Checkin.Book_PayDiscount,
                    SeasonAdvance = _Checkin.Book_PayCash,

                    OBS = string.Format("Check-in ({0}-{1}) {2}d, {3}, {4}% desc.",
                    _Book.Book_DateIn.ToString("dd/MM"),
                    _Book.Book_DateOut.ToString("dd/MM"),
                    _Checkin.Book_Days,
                    _VlTotal.ToString("C2"),
                    _Checkin.Book_PayDiscount)

                };
                _context.Add(_BookItem);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            //
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            //
            ViewBag.Season_LIST = from s in _context.Class_Season
                                  where s.StatusId == SeasonStatus.Ativo && s.CompanyId == _CompanyId
                                  orderby s.Name
                                  select new { s.Id, s.Name };
            //
            ViewBag.Channel_LIST = from s in _context.Class_Channel
                                   where s.CompanyId == _CompanyId && s.StatusId == ChannelStatus.Ativo
                                   orderby s.TypeId, s.Name
                                   select new { s.Id, s.Name };
            //
            ViewBag.yn_LIST = new SelectList(Enum.GetValues(typeof(GroupLevelItem_YN)));
            //
            return View(_Checkin);
        }



        //SERVICE
        public IActionResult SERVICES(int _BookID, int _ExtratoID)
        {
            int _CompanyId;
            try { _CompanyId = int.Parse(User.FindFirst("CompanyId").Value); }
            catch (Exception) { return RedirectToAction(nameof(Error), new { _Message = "Login Necessário!" }); }
            // Find Reserve
            var _Reserve = _context.Class_Book.FirstOrDefault(m => m.Id == _BookID);
            // Find HOSPEDE
            var _Guest = _context.Class_Guest.FirstOrDefault(m => m.CPF == _Reserve.GuestCPF);
            // Find TEMPORADA
            var _Season = _context.Class_Season.FirstOrDefault(m => m.Id == _Reserve.SeasonId);
            // Find HOSPEDAGEM
            var _GroupLevelItem = _context.Class_GroupLevelItem.FirstOrDefault(m => m.Id == _Reserve.GroupLevelItemId);
            var _GroupLevel = _context.Class_GroupLevel.FirstOrDefault(m => m.Id == _GroupLevelItem.GroupLevelId);

            var _Services = new Class_Services()
            {
                Reserve_ID = _Reserve.Id,
                Reserve_BookName = string.Format("{0} - {1}", _GroupLevel.Name, _GroupLevelItem.Name),
                Reserve_Date = string.Format("Entrada: {0}, Saída: {1}", _Reserve.Book_DateIn.ToString("dd/MM"), _Reserve.Book_DateOut.ToString("dd/MM")),
                Reserve_GuestName = string.Format("{0} ({1}) {2}", _Guest.Name, _Guest.City, _Guest.Phone1),
                Reserve_Occupants = string.Format("Adulto(s) {0}, Criança(s) {1}, PCD? {2}, PET? {3}", _Reserve.Book_AdultsNum, _Reserve.Book_KidsNum, _Reserve.Book_PCD, _Reserve.Book_PET),

                Season_Id = _Season.Id,
                Season_NAME = _Season.Name,
            };

            // Find ExtractItem
            if (_ExtratoID != 0)
            {
                var _Item = _context.Class_BookItem.FirstOrDefault(m => m.Id == _ExtratoID);
                var _ConsumoGroupLevelItem = _context.Class_GroupLevelItem.FirstOrDefault(m => m.Id == _Item.GroupLevelItemId);
                var _ConsumoGroupLevel = _context.Class_GroupLevel.FirstOrDefault(m => m.Id == _ConsumoGroupLevelItem.GroupLevelId);

                var _Total = _Item.SeasonUnit * _Item.SeasonValue;
                var _Final = _Total;
                if(_Item.SeasonDiscountPercent !=0)
                {
                    _Final -= _Item.SeasonDiscountValue;
                }

                var _ViewBagGL = from s in _context.Class_GroupLevel
                              where s.Id == _ConsumoGroupLevel.Id
                              select new { s.Id, s.Name };
                var _ViewBagGLI = from s in _context.Class_GroupLevelItem
                               where s.Id == _ConsumoGroupLevelItem.Id
                               select new { s.Id, s.Name };

                if (_ConsumoGroupLevel.GroupId == GroupType.CONSUMO)
                {
                    //CONSUMO
                    _Services.Consumo_ExtractID = _Item.Id;
                    _Services.Consumo_StatusId = _Item.StatusId;
                    _Services.Consumo_Date = _Item.DateConsume.ToString("dd/MM/yyyy");
                    _Services.Consumo_GroupLevelID = _ConsumoGroupLevel.Id;
                    _Services.Consumo_GroupLevelItemID = _ConsumoGroupLevelItem.Id;
                    _Services.Consumo_NAME = _Item.SeasonDesciption;
                    _Services.Consumo_QtUnit = _Item.SeasonUnit;
                    _Services.Consumo_VlUnit_VIEW = _Item.SeasonValue.ToString("C2");
                    _Services.Consumo_VlUnit = _Item.SeasonValue;
                    _Services.Consumo_VlTotal_VIEW = _Total.ToString("C2");
                    _Services.Consumo_VlTotal = _Total;
                    _Services.Consumo_PercDiscount = _Item.SeasonDiscountPercent;
                    _Services.Consumo_VlDiscount_VIEW = _Item.SeasonDiscountValue.ToString("C2");
                    _Services.Consumo_VlDiscount = _Item.SeasonDiscountValue;
                    _Services.Consumo_VlFinal_VIEW = _Final.ToString("C2");
                    _Services.Consumo_OBS = _Item.OBS;
                }
                else
                {
                    //ENTRETENIMENTO
                    _Services.Entretenimento_ExtractID = _Item.Id;
                    _Services.Entretenimento_StatusId = _Item.StatusId;
                    _Services.Entretenimento_Date = _Item.DateConsume.ToString("dd/MM/yyyy");
                    _Services.Entretenimento_Time = _Item.DateConsume.ToString("hh:mm");
                    _Services.Entretenimento_GroupLevelID = _ConsumoGroupLevel.Id;
                    _Services.Entretenimento_GroupLevelItemID = _ConsumoGroupLevelItem.Id;
                    _Services.Entretenimento_NAME = _Item.SeasonDesciption;
                    _Services.Entretenimento_QtUnit = _Item.SeasonUnit;
                    _Services.Entretenimento_VlUnit_VIEW = _Item.SeasonValue.ToString("C2");
                    _Services.Entretenimento_VlUnit = _Item.SeasonValue;
                    _Services.Entretenimento_VlTotal_VIEW = _Total.ToString("C2");
                    _Services.Entretenimento_VlTotal = _Total;
                    _Services.Entretenimento_PercDiscount = _Item.SeasonDiscountPercent;
                    _Services.Entretenimento_VlDiscount_VIEW = _Item.SeasonDiscountValue.ToString("C2");
                    _Services.Entretenimento_VlDiscount = _Item.SeasonDiscountValue;
                    _Services.Entretenimento_VlFinal_VIEW = _Final.ToString("C2");
                    _Services.Entretenimento_OBS = _Item.OBS;
                }

                ViewBag.Consumo_GroupLevel_LIST = new SelectList(_ViewBagGL, "Id", "Name");
                ViewBag.Consumo_GroupLevelItem_LIST = new SelectList(_ViewBagGLI, "Id", "Name");

                ViewBag.Entretenimento_GroupLevel_LIST = new SelectList(_ViewBagGL, "Id", "Name");
                ViewBag.Entretenimento_GroupLevelItem_LIST = new SelectList(_ViewBagGLI, "Id", "Name");
            }
            else
            {
                _Services.TabDefault = 0;

                //CONSUMO
                _Services.Consumo_ExtractID = 0;
                _Services.Consumo_StatusId = BookItemStatus.Consumido;
                _Services.Consumo_Date = DateTime.Today.ToString("dd/MM/yyyy");
                _Services.Consumo_GroupLevelID = 0;
                _Services.Consumo_GroupLevelItemID = 0;
                _Services.Consumo_NAME = "";
                _Services.Consumo_QtUnit = 1;
                _Services.Consumo_VlUnit_VIEW = "R$ 0";
                _Services.Consumo_VlUnit = 0;
                _Services.Consumo_VlTotal_VIEW = "R$ 0";
                _Services.Consumo_VlTotal = 0;
                _Services.Consumo_PercDiscount = 0;
                _Services.Consumo_VlDiscount_VIEW = "R$ 0";
                _Services.Consumo_VlDiscount = 0;
                _Services.Consumo_VlFinal_VIEW = "R$ 0";
                _Services.Consumo_OBS = "";

                var _Item = from s in _context.Class_GroupLevel
                            where s.CompanyId == _CompanyId && s.StatusId == GroupLevelStatus.Ativo && s.GroupId == GroupType.CONSUMO
                            orderby s.Name
                            select new { s.Id, s.Name };
                ViewBag.Consumo_GroupLevel_LIST = new SelectList(_Item, "Id", "Name");
                ViewBag.Consumo_GroupLevelItem_LIST = new List<Class_Service_LIST> { new Class_Service_LIST { ID = 0, Name = "Selecione GRUPO..." } };

                //ENTRETENIMENTO
                _Services.Entretenimento_ExtractID = 0;
                _Services.Entretenimento_StatusId = BookItemStatus.Reservado;
                _Services.Entretenimento_Date = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
                _Services.Entretenimento_Time = "00:00";
                _Services.Entretenimento_GroupLevelID = 0;
                _Services.Entretenimento_GroupLevelItemID = 0;
                _Services.Entretenimento_NAME = "";
                _Services.Entretenimento_QtUnit = 1;
                _Services.Entretenimento_VlUnit_VIEW = "R$ 0";
                _Services.Entretenimento_VlUnit = 0;
                _Services.Entretenimento_VlTotal_VIEW = "R$ 0";
                _Services.Entretenimento_VlTotal = 0;
                _Services.Entretenimento_PercDiscount = 0;
                _Services.Entretenimento_VlDiscount_VIEW = "R$ 0";
                _Services.Entretenimento_VlDiscount = 0;
                _Services.Entretenimento_VlFinal_VIEW = "R$ 0";
                _Services.Entretenimento_OBS = "";

                _Item = from s in _context.Class_GroupLevel
                        where s.CompanyId == _CompanyId && s.StatusId == GroupLevelStatus.Ativo && s.GroupId == GroupType.ENTRETENIMENTO
                        orderby s.Name
                        select new { s.Id, s.Name };
                ViewBag.Entretenimento_GroupLevel_LIST = new SelectList(_Item, "Id", "Name");
                ViewBag.Entretenimento_GroupLevelItem_LIST = new List<Class_Service_LIST> { new Class_Service_LIST { ID = 0, Name = "Selecione GRUPO..." } };

                //AJUSTE PERIODO
                var _ReserveItem = _context.Class_BookItem.FirstOrDefault(m => m.BookId == _BookID && m.GroupLevelItemId == _Reserve.GroupLevelItemId);
                var _VlTotal = _ReserveItem.SeasonUnit * _ReserveItem.SeasonValue;
                var _VlFinal = _VlTotal - _ReserveItem.SeasonDiscountValue - _ReserveItem.SeasonAdvance;
                if (_ReserveItem != null)
                {
                    _Services.Ajuste_ExtractID = _ReserveItem.Id;
                    _Services.Ajuste_SeasonDays = _ReserveItem.SeasonUnit;
                    _Services.Ajuste_SeasonValue = _ReserveItem.SeasonValue;
                    _Services.Ajuste_SeasonAdvance = _ReserveItem.SeasonAdvance;
                    _Services.Ajuste_SeasonDiscountVALUE = _ReserveItem.SeasonDiscountValue;

                    _Services.Ajuste_DateIn = _Reserve.Book_DateIn.ToString("dd/MM/yyyy");
                    _Services.Ajuste_DateOut = _Reserve.Book_DateOut.ToString("dd/MM/yyyy");
                    _Services.Ajuste_Days_VIEW = _ReserveItem.SeasonUnit.ToString();
                    _Services.Ajuste_VlTotal_VIEW = _VlTotal.ToString("C2");

                    _Services.Ajuste_PercDiscount = _ReserveItem.SeasonDiscountPercent;
                    _Services.Ajuste_VlDiscount_VIEW = _ReserveItem.SeasonDiscountValue.ToString("C2");
                    _Services.Ajuste_VlAdvance_VIEW = _ReserveItem.SeasonAdvance.ToString("C2");
                    _Services.Ajuste_VlFinal_VIEW = _VlFinal.ToString("C2");
                    _Services.Ajuste_OBS_History = _ReserveItem.OBS;
                    _Services.Ajuste_OBS = "";
                }

                //CHECK-out - hospedagem
                var _Detail = new Class_Service_Detail()
                {
                    DESC = "Vl.Total",
                    VALUE = _VlTotal.ToString("C2")
                };
                _Services.CheckOut_Hospedagem_LIST.Add(_Detail);
                if (_ReserveItem.SeasonDiscountValue != 0)
                {
                    _Detail = new Class_Service_Detail()
                    {
                        DESC = string.Format("({0} %) desc.", _ReserveItem.SeasonDiscountPercent),
                        VALUE = _ReserveItem.SeasonDiscountValue.ToString("C2")
                    };
                    _Services.CheckOut_Hospedagem_LIST.Add(_Detail);
                }
                if (_ReserveItem.SeasonAdvance != 0)
                {
                    _Detail = new Class_Service_Detail()
                    {
                        DESC = "Adiant.",
                        VALUE = _ReserveItem.SeasonAdvance.ToString("C2")
                    };
                    _Services.CheckOut_Hospedagem_LIST.Add(_Detail);
                }
                if (_VlTotal != _VlFinal)
                {
                    _Detail = new Class_Service_Detail()
                    {
                        DESC = "Vl.Final",
                        VALUE = _VlFinal.ToString("C2")
                    };
                    _Services.CheckOut_Hospedagem_LIST.Add(_Detail);
                }
                //CHECK-out - consumo
                var _Consumo = from s in _context.Class_BookItem
                               join s2 in _context.Class_GroupLevelItem on s.GroupLevelItemId equals s2.Id
                               join s3 in _context.Class_GroupLevel on s2.GroupLevelId equals s3.Id
                               where s.BookId == _BookID && s3.GroupId == GroupType.CONSUMO
                               group s by s.BookId into BookGroup
                               select new { SomaTotal = BookGroup.Sum(f => f.SeasonValue * f.SeasonUnit), SomaDesc = BookGroup.Sum(f => f.SeasonDiscountValue) };
                foreach (var _loop in _Consumo)
                {
                    _Detail = new Class_Service_Detail()
                    {
                        DESC = "Vl.Total",
                        VALUE = _loop.SomaTotal.ToString("C2")
                    };
                    _Services.CheckOut_Consumo_LIST.Add(_Detail);
                    //
                    if (_loop.SomaDesc != 0)
                    {
                        var _PercDesc = Math.Round(_loop.SomaDesc / _loop.SomaTotal * 100, 1);
                        _Detail = new Class_Service_Detail()
                        {
                            DESC = string.Format("({0} %) desc.", _PercDesc),
                            VALUE = _loop.SomaDesc.ToString("C2")
                        };
                        _Services.CheckOut_Consumo_LIST.Add(_Detail);
                        //
                        var _vlTotal = _loop.SomaTotal - _loop.SomaDesc;
                        _Detail = new Class_Service_Detail()
                        {
                            DESC = "Vl.Final",
                            VALUE = _loop.SomaDesc.ToString("C2")
                        };
                        _Services.CheckOut_Consumo_LIST.Add(_Detail);
                    }
                }
                //CHECK-out - entretenimento
                var _Entretenimento = from s in _context.Class_BookItem
                                      join s2 in _context.Class_GroupLevelItem on s.GroupLevelItemId equals s2.Id
                                      join s3 in _context.Class_GroupLevel on s2.GroupLevelId equals s3.Id
                                      where s.BookId == _BookID && s3.GroupId == GroupType.ENTRETENIMENTO
                                      group s by s.BookId into BookGroup
                                      select new { SomaTotal = BookGroup.Sum(f => f.SeasonValue * f.SeasonUnit), SomaDesc = BookGroup.Sum(f => f.SeasonDiscountValue) };
                foreach (var _loop in _Consumo)
                {
                    _Detail = new Class_Service_Detail()
                    {
                        DESC = "Vl.Total",
                        VALUE = _loop.SomaTotal.ToString("C2")
                    };
                    _Services.CheckOut_Entretenimento_LIST.Add(_Detail);
                    //
                    if (_loop.SomaDesc != 0)
                    {
                        var _PercDesc = Math.Round(_loop.SomaDesc / _loop.SomaTotal * 100, 1);
                        _Detail = new Class_Service_Detail()
                        {
                            DESC = string.Format("({0} %) desc.", _PercDesc),
                            VALUE = _loop.SomaDesc.ToString("C2")
                        };
                        _Services.CheckOut_Entretenimento_LIST.Add(_Detail);
                        //
                        var _vlTotal = _loop.SomaTotal - _loop.SomaDesc;
                        _Detail = new Class_Service_Detail()
                        {
                            DESC = "Vl.Final",
                            VALUE = _loop.SomaDesc.ToString("C2")
                        };
                        _Services.CheckOut_Entretenimento_LIST.Add(_Detail);
                    }
                }
                var _PayTotal = from s in _context.Class_BookItem
                                where s.BookId == _BookID
                                group s by s.BookId into BookGroup
                                select new { SomaTotal = BookGroup.Sum(f => f.SeasonValue * f.SeasonUnit), SomaDesc = BookGroup.Sum(f => f.SeasonDiscountValue), SomaAdiant = BookGroup.Sum(f => f.SeasonAdvance) };
                foreach (var _loop in _PayTotal)
                {
                    var _final = _loop.SomaTotal - _loop.SomaDesc - _loop.SomaAdiant;
                    _Services.CheckOut_APagar_VIEW = _final.ToString("C2");
                    _Services.CheckOut_APagar = _final;
                }
                ViewBag.CheckOut_PayForm_LIST = new SelectList(Enum.GetValues(typeof(Services_Checkout_PayForm)).Cast<Services_Checkout_PayForm>().ToList());
                _Services.CheckOut_PayForm = "";
                _Services.CheckOut_OBS = "";
            }
            //EXTRATO
            var _Extrato = from s in _context.Class_BookItem
                           where s.BookId == _BookID
                           orderby s.DateConsume, s.GroupLevelItemId
                           select s;
            var _VlPagar = 0.0;
            var _Date = "";
            var _ItemExtrato = new Class_ServiceExtract();
            foreach (var _Item in _Extrato)
            {
                if (_Date != _Item.DateConsume.ToString("dd/MM"))
                {
                    _ItemExtrato = new Class_ServiceExtract()
                    {
                        ExtractTEXT = "",
                        ExtractVALUE = ""
                    };
                    _Services.ExtractLIST.Add(_ItemExtrato);
                    //
                    _ItemExtrato = new Class_ServiceExtract()
                    {
                        ExtractTEXT = string.Format("==( {0} )==", _Item.DateConsume.ToString("dd/MM")),
                        ExtractVALUE = ""
                    };
                    _Services.ExtractLIST.Add(_ItemExtrato);
                    //
                    _Date = _Item.DateConsume.ToString("dd/MM");
                }
                //
                var _Total = _Item.SeasonValue * _Item.SeasonUnit;
                _ItemExtrato = new Class_ServiceExtract()
                {
                    ExtractTEXT = _Item.SeasonDesciption,
                    ExtractVALUE = _Total.ToString("C2")
                };
                if (_Item.StatusId == BookItemStatus.Reservado) { _ItemExtrato.ExtractTEXT = "(RESERVADO) " + _ItemExtrato.ExtractTEXT; }
                _Services.ExtractLIST.Add(_ItemExtrato);
                _VlPagar += _Total;
                //
                var _Discount = _Item.SeasonDiscountValue;
                if (_Discount != 0)
                {
                    _ItemExtrato = new Class_ServiceExtract()
                    {
                        ExtractTEXT = string.Format("( {0} % ) desc.", _Item.SeasonDiscountPercent),
                        ExtractVALUE = _Discount.ToString("C2")
                    };
                    _Services.ExtractLIST.Add(_ItemExtrato);
                    _VlPagar -= _Discount;
                }
                var _Advance = _Item.SeasonAdvance;
                if (_Advance != 0)
                {
                    _ItemExtrato = new Class_ServiceExtract()
                    {
                        ExtractTEXT = "Adiant.",
                        ExtractVALUE = _Advance.ToString("C2")
                    };
                    _Services.ExtractLIST.Add(_ItemExtrato);
                    _VlPagar -= _Advance;
                }
            }
            //
            _ItemExtrato = new Class_ServiceExtract()
            {
                ExtractTEXT = "",
                ExtractVALUE = ""
            };
            _Services.ExtractLIST.Add(_ItemExtrato);
            //
            _ItemExtrato = new Class_ServiceExtract()
            {
                ExtractTEXT = "",
                ExtractVALUE = "=========="
            };
            _Services.ExtractLIST.Add(_ItemExtrato);
            //
            _ItemExtrato = new Class_ServiceExtract()
            {
                ExtractTEXT = "Valor A PAGAR ->",
                ExtractVALUE = _VlPagar.ToString("C2")
            };
            _Services.ExtractLIST.Add(_ItemExtrato);
            //
            return View(_Services);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SERVICES(Class_Services _Services)
        {
            var _BookItem = new Class_BookItem();
            if (_Services.Consumo_TabInput == 1)
            {
                var _BookItemDate = _Services.Consumo_Date.Split("/");
                _BookItem = new Class_BookItem()
                {
                    StatusId = BookItemStatus.Consumido,
                    BookId = _Services.Reserve_ID,

                    Id = _Services.Consumo_ExtractID,
                    DateConsume = DateTime.Parse(string.Format("{0}/{1}/{2} 00:01", _BookItemDate[1], _BookItemDate[0], _BookItemDate[2])),
                    GroupLevelItemId = _Services.Consumo_GroupLevelItemID,
                    SeasonUnit = _Services.Consumo_QtUnit,
                    SeasonDesciption = _Services.Consumo_NAME,
                    SeasonValue = _Services.Consumo_VlUnit,
                    SeasonDiscountValue = _Services.Consumo_VlDiscount,
                    SeasonDiscountPercent = _Services.Consumo_PercDiscount,
                    SeasonAdvance = 0,
                    OBS = _Services.Consumo_OBS
                };
            }
            else if (_Services.Entretenimento_TabInput == 1)
            {
                var _BookItemDate = _Services.Entretenimento_Date.Split("/");
                if (_Services.Entretenimento_Time == "") { _Services.Entretenimento_Time = "00:01"; }
                _BookItem = new Class_BookItem()
                {
                    StatusId = BookItemStatus.Consumido,
                    BookId = _Services.Reserve_ID,

                    Id = _Services.Entretenimento_ExtractID,
                    DateConsume = DateTime.Parse(string.Format("{0}/{1}/{2} {3}", _BookItemDate[1], _BookItemDate[0], _BookItemDate[2], _Services.Entretenimento_Time)),
                    GroupLevelItemId = _Services.Entretenimento_GroupLevelItemID,
                    SeasonUnit = _Services.Entretenimento_QtUnit,
                    SeasonDesciption = _Services.Entretenimento_NAME,
                    SeasonValue = _Services.Entretenimento_VlUnit,
                    SeasonDiscountValue = _Services.Entretenimento_VlDiscount,
                    SeasonDiscountPercent = _Services.Entretenimento_PercDiscount,
                    SeasonAdvance = 0,
                    OBS = _Services.Entretenimento_OBS
                };
                if (_BookItem.DateConsume >= DateTime.Today.ToLocalTime())
                {
                    _BookItem.StatusId = BookItemStatus.Reservado;
                }
            }
            else if (_Services.Ajuste_TabInput == 1)
            {
                _BookItem = _context.Class_BookItem.FirstOrDefault(m => m.Id == _Services.Ajuste_ExtractID);

                _BookItem.SeasonUnit = _Services.Ajuste_SeasonDays;
                _BookItem.SeasonDiscountPercent = _Services.Ajuste_PercDiscount;

                var _VlTotal = _BookItem.SeasonValue * _Services.Ajuste_SeasonDays;
                var _VlDiscount = _VlTotal * ((double)_Services.Ajuste_PercDiscount / 100);
                _BookItem.SeasonDiscountValue = _VlDiscount;

                var _OBS = _BookItem.OBS + string.Format("{0}=={1}, {2}==", Environment.NewLine, DateTime.Today.ToString("dd/MM"), _Services.Ajuste_OBS);
                while (_OBS.Length > 200)
                {
                    try
                    {
                        _OBS = _OBS.Substring(0, _OBS.IndexOf("==")) + _OBS.Substring(_OBS.IndexOf("==", _OBS.IndexOf("==") + 2) + 3);
                    }
                    catch
                    {
                        _OBS = _OBS.Substring(0, 199);
                    }
                }
                _BookItem.OBS = _OBS;
            }
            else if (_Services.CheckOut_TabInput == 1)
            {
                var _BookItemDate = DateTime.Today.ToString("dd/MM/yyyy").Split("/");
                _BookItem = new Class_BookItem()
                {
                    StatusId = BookItemStatus.Consumido,
                    BookId = _Services.Reserve_ID,

                    Id = 0,
                    DateConsume = DateTime.Parse(string.Format("{0}/{1}/{2} 00:01", _BookItemDate[1], _BookItemDate[0], _BookItemDate[2])),
                    GroupLevelItemId = 0,
                    SeasonUnit = 1,
                    SeasonDesciption = string.Format("Check-Out, Forma de Pagto: {0}", _Services.CheckOut_PayForm),
                    SeasonValue = _Services.CheckOut_APagar * -1,
                    SeasonDiscountValue = 0,
                    SeasonDiscountPercent = 0,
                    SeasonAdvance = 0,
                    OBS = _Services.CheckOut_OBS
                };
            }
            else
            {
                return RedirectToAction(nameof(Error), new { _Message = "Não houve mudança significativa!" });
            }

            if (_BookItem.Id == 0)
            {
                _context.Add(_BookItem);
            }
            else
            {
                _context.Update(_BookItem);
            }
            _context.SaveChanges();

            //AJUSTE - Checkin
            if (_Services.Ajuste_TabInput == 1)
            {
                var _BookItemDate = _Services.Ajuste_DateOut.Split("/");
                var _Book = _context.Class_Book.FirstOrDefault(m => m.Id == _BookItem.BookId);

                _Book.Book_DateOut = DateTime.Parse(string.Format("{0}/{1}/{2} 00:01", _BookItemDate[1], _BookItemDate[0], _BookItemDate[2]));

                _context.Update(_BookItem);
                _context.SaveChanges();
            }
            else if (_Services.CheckOut_TabInput == 1)
            {
                //Ajusta Status Historico de CONSUMO
                var _Consumo = from s in _context.Class_BookItem
                               where s.BookId == _Services.Reserve_ID && s.StatusId == BookItemStatus.Reservado
                               select s;
                foreach (var _loop in _Consumo)
                {
                    _loop.StatusId = BookItemStatus.Consumido;
                    _context.Update(_loop);
                }
                _context.SaveChanges();
                // ajusta Status da Reserva
                var _Book = _context.Class_Book.Where(m => m.Id == _Services.Reserve_ID).FirstOrDefault();
                _Book.StatusId = BookStatus.Finalizado;
                _context.Update(_Book);
                _context.SaveChanges();

                //return View(_Services);
                return RedirectToAction("SERVICES", "Index", new { _BookID = _Services.Reserve_ID, _ExtratoID = 0 });
            }
            //return View(_Services);
            return RedirectToAction("SERVICES", "Reception", new { _BookID = _Services.Reserve_ID, _ExtratoID = 0 });
        }

        public IActionResult SERVICES_History(int _ReserveID, GroupType _GroupId)
        {
            var _Item = from s in _context.Class_BookItem
                        join s2 in _context.Class_GroupLevelItem on s.GroupLevelItemId equals s2.Id
                        join s3 in _context.Class_GroupLevel on s2.GroupLevelId equals s3.Id
                        where s.BookId == _ReserveID && s3.GroupId == _GroupId
                        orderby s.StatusId, s.DateConsume, s2.Name
                        select s;

            ViewBag.BookId = _ReserveID;
            ViewBag.GroupTypeName = _GroupId.ToString();
            return View(_Item);
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



        // Function AJAX
        public JsonResult FIND_SEASON(int _SeasonId, int _GroupLevelItemId)
        {
            var _GroupLevelItem = _context.Class_GroupLevelItem.FirstOrDefault(m => m.Id == _GroupLevelItemId);
            var _GroupLevel = _context.Class_GroupLevel.FirstOrDefault(m => m.Id == _GroupLevelItem.GroupLevelId);
            var _SeasonFound = _context.Class_SeasonItem.FirstOrDefault(m => m.SeasonId == _SeasonId && m.GroupLevelItemId == _GroupLevelItemId);

            var _Return = new Class_Service_LIST()
            {
                Name = string.Format("{0} - {1}", _GroupLevel.Name, _GroupLevelItem.Name),
                Tax = _SeasonFound.Tax
            };

            return Json(_Return);
        }
        public JsonResult FIND_CHANNEL(int _ChannelId)
        {
            var _ChannelFound = _context.Class_Channel.FirstOrDefault(m => m.Id == _ChannelId);
            return Json(_ChannelFound);
        }
        public JsonResult FIND_CPF(string _CPF)
        {
            var _UserFound = _context.Class_Guest.FirstOrDefault(m => m.CPF == _CPF);
            return Json(_UserFound);
        }
        public JsonResult FIND_GroupLevelItem(int _GroupLevelID)
        {
            var _GroupLevelItem = (from s in _context.Class_GroupLevelItem
                                   where s.GroupLevelId == _GroupLevelID
                                   orderby s.Name
                                   select new { s.Id, s.Name });
            return Json(_GroupLevelItem);
        }
    }
}
