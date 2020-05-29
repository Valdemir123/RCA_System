using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Models
{
    public enum GroupLevelStatus : int
    {
        Ativo = 1,
        Inativo = 2,
        Suspenso = 3
    }

    public enum Group : int
    {
        HOSPEDAGEM = 1,
        CANAL = 2,
        ENTRETENIMENTO = 3,
        CONSUMO = 4
    }

    [Table("GroupLevel")]
    public class Class_GroupLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public GroupLevelStatus StatusId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [DisplayName("Grupo")]
        public Group GroupId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        public ICollection<Class_GroupLevelItem> GroupLevelItem_LIST { get; set; } = new List<Class_GroupLevelItem>();


        //Constructors
        public Class_GroupLevel() { }
        public Class_GroupLevel(int _Id, GroupLevelStatus _StatusId, int _CompanyId, Group _GroupId, string _Name)
        {
            Id = _Id;
            StatusId = _StatusId;
            CompanyId = _CompanyId;
            GroupId = _GroupId;
            Name = _Name;
        }

        //Methods
        public void GroupLevelItem_Add (Class_GroupLevelItem _Item)
        {
            GroupLevelItem_LIST.Add(_Item);
        }

        public void GroupLevelItem_Remove(Class_GroupLevelItem _Item)
        {
            GroupLevelItem_LIST.Remove(_Item);
        }
    }
}
