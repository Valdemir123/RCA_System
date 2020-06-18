using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum GroupLevelStatus : int
    {
        Ativo = 1,
        Suspenso = 2
    }
    public enum GroupType : int
    {
        HOSPEDAGEM = 1,
        ENTRETENIMENTO = 2,
        CONSUMO = 3
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
        public GroupType GroupId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }
    }
}
