using System.Collections.Generic;
using System.ComponentModel;

namespace RCA.Models
{
    public class Class_Cadaster
    {
        public GroupType HOSPEDAGEMid { get; set; } = GroupType.HOSPEDAGEM;
        public GroupType ENTRETENIMENTOid { get; set; } = GroupType.ENTRETENIMENTO;
        public GroupType CONSUMOid { get; set; } = GroupType.CONSUMO;

        public int CompanyId { get; set; }
        public GroupType GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Class_CadasterItem> CadasterLIST { get; set; } = new List<Class_CadasterItem>();
    }
    public class Class_CadasterItem
    {
        public string TypeLine { get; set; }
        public int GroupLevelId { get; set; }
        public string GroupLevelName { get; set; }
        public int GroupLevelItemId { get; set; }
        public string GroupLevelItemName { get; set; }
        public int GroupLevelItemTaxId { get; set; }
        public string GroupLevelItemTaxName { get; set; }

        [DisplayName("Item")]
        public string ItemDesc { get; set; }
    }
}
