using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

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

        [Required]
        [StringLength(100)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("No. CNPJ")]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Site (www)")]
        [DataType(DataType.Url)]
        public string Site { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome Contato")]
        public string ContactName { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Celular.")]
        [DataType(DataType.PhoneNumber)]

        public string Phone1 { get; set; }

        [StringLength(20)]
        [DisplayName("Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        [Required]
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
        [DisplayName("No - Compl. - Bairro")]
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



        //Constructors
        public Class_Company() { }
        public Class_Company(int _Id, CompanyStatus _StatusId, string _Name, string _CNPJ, string _Site,
                            string _ContactName, string _Phone1, string _Phone2, string _Email, 
                            string _PostalCode, string _Address, string _Complement, string _City, string _State, string _Country)
        {
            Id = _Id;
            StatusId = _StatusId;
            Name = _Name;
            CNPJ = _CNPJ;
            Site = _Site;

            ContactName = _ContactName;
            Phone1 = _Phone1;
            Phone2 = _Phone2;
            Email = _Email;

            PostalCode = _PostalCode;
            Address = _Address;
            Complement = _Complement;
            City = _City;
            State = _State;
            Country = _Country;
        }
    }

    



}
