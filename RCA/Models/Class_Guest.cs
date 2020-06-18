using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    [Table("Guest")]
    public class Class_Guest
    {
        [Key]
        [Required]
        [StringLength(20)]
        [DisplayName("CPF")]
        public string CPF { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Celular")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [StringLength(20)]
        [DisplayName("Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("CEP")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Endereço")]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("No - Complemento - Bairro")]
        public string Complement { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Estado")]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("País")]
        public string Country { get; set; }
    }
}
