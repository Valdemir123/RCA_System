using System.Collections.Generic;

namespace RCA.Models
{
    public class Class_ReceptionBOOK
    {
        public int LevelID { get; set; }
        public string LevelNAME { get; set; }

        public List<Class_ReceptionBOOKItem> BookItem_LIST { get; set; } = new List<Class_ReceptionBOOKItem>();
    }

    public class Class_ReceptionBOOKItem
    {
        public int Item_Id { get; set; }
        public string Item_Name { get; set; }

        public int Reserve_Id { get; set; }
        public int Reserve_StatusId { get; set; }
        public string Reserve_StatusName { get; set; }
        public string Reserve_Format { get; set; }
        public string Reserve_DateOut { get; set; }

        public string Reserve_GuestName { get; set; }
        public string Reserve_GuestPhone { get; set; }

        public List<Class_ReceptionENTRETENIMENT> Entreteniment_LIST { get; set; } = new List<Class_ReceptionENTRETENIMENT>();
    }

    public class Class_ReceptionENTRETENIMENT
    {
        public string DayTime { get; set; }
        public string Name { get; set; }
    }
}
