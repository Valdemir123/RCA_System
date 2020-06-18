using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    public enum CompanyStatus : int
    {
        Ativo = 1,
        Suspenso = 2
    }
    public enum CompanyCountry : short
    {
        Brasil
    }

    [Table("Company")]
    public class Class_Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public CompanyStatus StatusId { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(20)]
        [DisplayName("No. CNPJ")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage ="{0} Preenchimento Obrigatório")]
        [StringLength(100)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(100)]
        [DisplayName("Site (www)")]
        [DataType(DataType.Url)]
        [Url(ErrorMessage ="{0} Formato Obrigatório, http://wwww.site.com.br")]
        public string Site { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Nome Contato")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} Formato Obrigatório, user@provedor.com.br")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(20)]
        [DisplayName("Celular")]
        public string Phone1 { get; set; }

        [StringLength(20)]
        [DisplayName("Telefone")]
        public string Phone2 { get; set; }

        

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(10)]
        [DisplayName("CEP")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Endereço")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("No - Compl. - Bairro")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Estado")]
        public string State { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("País")]
        public string Country { get; set; }
    }
}
