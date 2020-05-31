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
        Suspenso = 2,
        EmManutencao = 3
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
    }
}
