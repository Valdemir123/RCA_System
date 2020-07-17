using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum BookItemStatus : int
    {
        Reservado = 1,
        Consumido = 2
    }

    [Table("BookItem")]
    public class Class_BookItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public BookItemStatus StatusId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateConsume { get; set; }

        [Required]
        [ForeignKey("GroupLevelItem")]
        public int GroupLevelItemId { get; set; }

        [Required]
        [DisplayName("Quantidade")]
        public int SeasonUnit { get; set; }

        [Required]
        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double SeasonValue { get; set; }

        [Required]
        [DisplayName("Desconto ($)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double SeasonDiscountValue { get; set; }

        [Required]
        [DisplayName("Desconto (%)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public int SeasonDiscountPercent { get; set; }

        [Required]
        [DisplayName("Adiantamento")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double SeasonAdvance { get; set; }

        [DisplayName("Obs.")]
        [StringLength(200)]
        public string OBS { get; set; }
    }
}
