using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Models
{
    public enum BookStatus : int
    {
        Reservado = 1,
        EmUso = 2,
        Finalizado = 3,
        Cancelado = 4
    }

    [Table("Book")]
    public class Class_Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public BookStatus StatusId { get; set; }



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
        public double Channel_Tax { get; set; }

        [DisplayName("Taxa (%)")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yy")]
        public DateTime Book_DateIn { get; set; }

        [Required]
        [DisplayName("Término")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yy")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:C")]
        public double Book_DayValue { get; set; }

        [Required]
        [DisplayName("(%) Desconto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:###")]
        public int Book_DiscountPercent { get; set; }

        [Required]
        [DisplayName("Adiantamento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:C")]
        public double Book_InputValue { get; set; }



        [ForeignKey("Guest")]
        [Required]
        [DisplayName("Hóspede responsável")]
        public string GuestCPF { get; set; }


        public Class_Book() { }

        public Class_Book(BookStatus _StatusId, 
                        int _Channel_GroupLevelId, int _Channel_GroupLevelItemId, string _Channel_Code, double _Channel_Tax, double _Channel_Percent,
                        int _Book_GroupLevelItemId, int _Book_GroupLevelItemTaxId, DateTime _Book_DateIn, DateTime _Book_DateOut, int _Book_AdultsNum,
                        int _Book_KidsNum, bool _Book_PCD, bool _Book_PET, double _Book_DayValue, int _Book_DiscountPercent, double _Book_InputValue,
                        string _GuestCPF)
        {
            StatusId = _StatusId;
            Channel_GroupLevelId = _Channel_GroupLevelId;
            Channel_GroupLevelItemId = _Channel_GroupLevelItemId;
            Channel_Code = _Channel_Code;
            Channel_Tax = _Channel_Tax;
            Channel_Percent = _Channel_Percent;
            Book_GroupLevelItemId = _Book_GroupLevelItemId;
            Book_GroupLevelItemTaxId = _Book_GroupLevelItemTaxId;
            Book_DateIn = _Book_DateIn;
            Book_DateOut = _Book_DateOut;
            Book_AdultsNum = _Book_AdultsNum;
            Book_KidsNum = _Book_KidsNum;
            Book_PCD = _Book_PCD;
            Book_PET = _Book_PET;
            Book_DayValue = _Book_DayValue;
            Book_DiscountPercent = _Book_DiscountPercent;
            Book_InputValue = _Book_InputValue;
            GuestCPF = _GuestCPF;
        }
    }
}
