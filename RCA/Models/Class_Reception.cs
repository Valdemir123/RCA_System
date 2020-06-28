using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum ReceptionStatus : int
    {
        Reservado = 1,
        EmUso = 2,
        Finalizado = 3,
        Cancelado = 4
    }

    [Table("Book")]
    public class Class_Reception
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public ReceptionStatus StatusId { get; set; }



        [ForeignKey("GroupLevel")]
        [Required]
        [DisplayName("Canal")]
        public int Channel_GroupLevelId { get; set; }

        [ForeignKey("GroupLevelItem")]
        [Required]
        [DisplayName("Local")]
        public int Channel_GroupLevelItemId { get; set; }

        [DisplayName("Cód.Reserva")]
        [StringLength(50)]
        public string Channel_Code { get; set; }

        [DisplayName("Taxa (R$)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Channel_Tax { get; set; }

        [DisplayName("Taxa (%)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public double Channel_Percent { get; set; }



        [ForeignKey("GroupLevelItem")]
        [Required]
        [DisplayName("Quarto")]
        public int Book_GroupLevelItemId { get; set; }

        [ForeignKey("GroupLevelItemTax")]
        [Required]
        [DisplayName("Temporada")]
        public int Book_GroupLevelItemTaxId { get; set; }

        [Required]
        [DisplayName("Início")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Book_DateIn { get; set; }

        [Required]
        [DisplayName("Término")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Book_DateOut { get; set; }

        [Required]
        [DisplayName("No.Adulto(s)")]
        public int Book_AdultsNum { get; set; }

        [Required]
        [DisplayName("No.Criança(s)")]
        public int Book_KidsNum { get; set; }

        [Required]
        [DisplayName("PCD ?")]
        public bool Book_PCD { get; set; }

        [Required]
        [DisplayName("PET ?")]
        public bool Book_PET { get; set; }

        [Required]
        [DisplayName("Valor Diária")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Book_DayValue { get; set; }

        [Required]
        [DisplayName("(%) Desconto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public int Book_DiscountPercent { get; set; }

        [Required]
        [DisplayName("Adiantamento")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Book_InputValue { get; set; }



        [ForeignKey("Guest")]
        [Required]
        [DisplayName("Hóspede responsável")]
        public string GuestCPF { get; set; }
    }
}
