using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace RCA.Models
{
    public class Class_Company
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("CNPJ")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="###.###.###/####-##")]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome do Contato")]
        public string ContactName { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("1o. Tel.")]
        public string Phone1 { get; set; }

        [StringLength(20)]
        [DisplayName("2o. Tel.")]
        public string Phone2 { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("CEP")]
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
