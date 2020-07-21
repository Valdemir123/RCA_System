using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum BookStatus : int
    {
        EmUso = 1,
        Reservado = 2,
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

        [ForeignKey("GroupLevelItem")]
        [Required]
        [DisplayName("Quarto")]
        public int GroupLevelItemId { get; set; }

        [ForeignKey("Season")]
        [Required]
        [DisplayName("Temporada")]
        public int SeasonId { get; set; }



        [ForeignKey("Channel")]
        [Required]
        [DisplayName("Canal")]
        public int ChannelId { get; set; }

        [DisplayName("Cód.Reserva")]
        [StringLength(50)]
        public string Channel_Code { get; set; }

        [DisplayName("Taxa (R$)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Channel_Tax { get; set; }

        [DisplayName("Taxa (%)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public double Channel_Percent { get; set; }



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
        [DisplayName("Adulto(s)")]
        public int Book_AdultsNum { get; set; }

        [Required]
        [DisplayName("Criança(s)")]
        public int Book_KidsNum { get; set; }

        [Required]
        [DisplayName("PCD ?")]
        public GroupLevelItem_YN Book_PCD { get; set; }

        [Required]
        [DisplayName("PET ?")]
        public GroupLevelItem_YN Book_PET { get; set; }

        

        [ForeignKey("Guest")]
        [Required]
        [DisplayName("CPF responsável")]
        public string GuestCPF { get; set; }
    }
}
