using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


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


        //Constructs
        public Class_Guest() { }
        public Class_Guest(string _CPF, string _Name, string _Phone1, string _Phone2, string _Email, string _PostalCode, string _Address, string _Complement, string _City, string _State, string _Country)
        {
            CPF = _CPF;
            Name = _Name;
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
