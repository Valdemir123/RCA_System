using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum SeasonStatus : int
    {
        Ativo = 1,
        Suspenso = 2
    }

    [Table("Season")]
    public class Class_Season
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public SeasonStatus StatusId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }
    }
}
