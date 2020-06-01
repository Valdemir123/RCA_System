using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Models
{
    public enum ChannelStatus : int
    {
        Ativo = 1,
        Suspenso = 2
    }
    public enum ChannelType : int 
    { 
        BALCAO = 1,
        SITE = 2,
        RESERVA =3
    }

    [Table("Channel")]
    public class Class_Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public ChannelStatus StatusId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public ChannelType TypeId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Taxa em (R$)")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        [DataType(DataType.Currency)]
        public double Tax { get; set; }

        [Required]
        [DisplayName("Taxa em (%)")]
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double Percent { get; set; }
    }
}
