using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum GroupLevelItemStatus : int
    {
        [Display(Name = "Ativo")]
        Ativo = 1,

        [Display(Name = "Suspenso")]
        Suspenso = 2,

        [Display(Name = "Em Manuteção")]
        EmManutencao = 3
    }

    public enum GroupLevelItemPCD : int
    {
        [Display(Name = "Sim")]
        Sim = 1,

        [Display(Name = "Não")]
        Nao = 0
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
        [DisplayName("Preparado para PCD?")]
        public GroupLevelItemPCD PCD { get; set; }



        //Constructors
        public Class_GroupLevelItem() { }
        public Class_GroupLevelItem(int _Id, GroupLevelItemStatus _StatusId, int _GroupLevelId, string _Name, int _OccupantsNum, GroupLevelItemPCD _PCD)
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
