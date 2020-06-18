using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    [Table("GroupLevelItemTax")]
    public class Class_GroupLevelItemTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("GroupLevelItem")]
        public int GroupLevelItemId { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Tax { get; set; }
    }
}
