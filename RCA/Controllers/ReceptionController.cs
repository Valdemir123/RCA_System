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
    public class ReceptionController : Controller
    {
        private readonly RCAContext _context;
        public ReceptionController(RCAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);
            var _Reception = new List<Class_ReceptionBOOK>();

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
                        _GLIi.Reserve_StatusName = _Book.StatusId.ToString();
                        _GLIi.Reserve_Format = string.Format("{0} Adultos, {1} Crianças, PCD?{2}, PET?{3}", _Book.Book_AdultsNum, _Book.Book_KidsNum, _Book.Book_PCD, _Book.Book_PET);
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
                                DayTime = string.Format("", _Tour.DateConsume.ToString("dd"), _Tour.DateConsume.ToString("HH:mm")),
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
            return View(_Reception);
        }

        public IActionResult CHECKin(int _RoomID)
        {
            var _Room = _context.Class_GroupLevelItem.FirstOrDefault(s => s.Id == _RoomID);
            var _Group = _context.Class_GroupLevel.FirstOrDefault(s => s.Id == _Room.GroupLevelId);
            var _Checkin = new Class_Checkin()
            {
                Id = 0,
                StatusId = BookStatus.EmUso,

                GroupLevelItem_Id = _RoomID,
                GroupLevelItem_Name = string.Format("{0} - {1}", _Group.Name, _Room.Name),

                Season_Id = 0,

                Channel_Id = 0,
                Channel_Code = "",
                Channel_Percent = 0,
                Channel_Tax = 0,

                Book_DateIn = DateTime.Today,
                Book_DateOut = DateTime.Today.AddDays(5),
                Book_AdultsNum = _Room.OccupantsNum,
                Book_KidsNum = 0,
                Book_PCD = _Room.PCD,
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
            var _CompanyId = int.Parse(User.FindFirst("CompanyId").Value);
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
            var _SeasonFound = _context.Class_SeasonItem.FirstOrDefault(m => m.SeasonId == _SeasonId && m.GroupLevelItemId == _GroupLevelItemId);
            return Json(_SeasonFound);
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
    }
}
