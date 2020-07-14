using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RCA.Models
{
    public class Class_Checkin
    {
        public int Id { get; set; }
        public BookStatus StatusId { get; set; }
        public int GroupLevelItem_Id { get; set; }
        public string GroupLevelItem_Name { get; set; }


        [Required]
        [DisplayName("Temporada")]
        public int Season_Id { get; set; }


        [Required]
        [DisplayName("Canal")]
        public int Channel_Id { get; set; }

        [DisplayName("Taxa (R$)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double Channel_Tax { get; set; }

        [DisplayName("Taxa (%)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public double Channel_Percent { get; set; }

        [DisplayName("Cód.Reserva")]
        [StringLength(50)]
        public string Channel_Code { get; set; }



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



        [Required]
        [StringLength(20)]
        [DisplayName("CPF responsável")]
        public string Guest_CPF { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nome")]
        public string Guest_Name { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Celular")]
        [DataType(DataType.PhoneNumber)]
        public string Guest_Phone1 { get; set; }

        [StringLength(20)]
        [DisplayName("Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Guest_Phone2 { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Guest_Email { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("CEP")]
        [DataType(DataType.PostalCode)]
        public string Guest_PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Endereço")]
        public string Guest_Address { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("No - Complemento - Bairro")]
        public string Guest_Complement { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Cidade")]
        public string Guest_City { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Estado")]
        public string Guest_State { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("País")]
        public string Guest_Country { get; set; }
    }

    public class Class_Checkin_LIST
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
