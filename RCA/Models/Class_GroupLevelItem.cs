using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Models
{
    public enum GroupLevelItemStatus : int
    {
        Ativo = 1,
        Inativo = 2,
        Suspenso = 3,
        EmManutencao = 4
    }

    [Table("GroupLevelItem")]
    public class Class_GroupLevelItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public GroupLevelItemStatus StatusId { get; set; }

        [ForeignKey("GroupLevel")]
        [DisplayName("Grupo")]
        public int GroupLevelId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [DisplayName("No.Ocupantes")]
        public int OccupantsNum { get; set; }

        [Required]
        [DisplayName("PCD?")]
        public bool PCD { get; set; }

        public ICollection<Class_GroupLevelItemTax> GroupLevelItemTax_LIST { get; set; } = new List<Class_GroupLevelItemTax>();

        //Constructors
        public Class_GroupLevelItem() { }
        public Class_GroupLevelItem(int _Id, GroupLevelItemStatus _StatusId, int _GroupLevelId, string _Name, int _OccupantsNum, bool _PCD)
        {
            Id = _Id;
            StatusId = _StatusId;
            GroupLevelId = _GroupLevelId;
            Name = _Name;
            OccupantsNum = _OccupantsNum;
            PCD = _PCD;
        }

        //Methods
        public void GroupLevelItemTax_Add(Class_GroupLevelItemTax _Item)
        {
            GroupLevelItemTax_LIST.Add(_Item);
        }

        public void GroupLevelItemTax_Remove(Class_GroupLevelItemTax _Item)
        {
            GroupLevelItemTax_LIST.Remove(_Item);
        }
    }
}
