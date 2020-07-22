using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace RCA.Models
{
    public enum Services_Checkout_PayForm : int
    {
        Credito = 1,
        Debito = 2,
        Cheque = 3,
        Dinheiro = 4,
        Outro = 0
    }
    [DefaultValue(0)]

    public class Class_Services
    {
        public int TabDefault { get; set; }
        public string TabInput { get; set; }

        public int Reserve_ID { get; set; }
        public string Reserve_BookName { get; set; }
        public string Reserve_Occupants { get; set; }
        public string Reserve_Date { get; set; }
        public string Reserve_GuestName { get; set; }

        public int Season_Id { get; set; }
        public string Season_NAME { get; set; }

        public List<Class_ServiceExtract> ExtractLIST { get; set; } = new List<Class_ServiceExtract>();



        //CONSUMO
        public int Consumo_ExtractID { get; set; }
        public BookItemStatus Consumo_StatusId { get; set; }

        [Required]
        [Display(Name = "Grupo")]
        public int Consumo_GroupLevelID { get; set; }

        [Required]
        [Display(Name = "Item")]
        public int Consumo_GroupLevelItemID { get; set; }

        public string Consumo_NAME { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Consumo_Date { get; set; }

        [Required]
        [Display(Name = "Unid")]
        [Range(1, 100, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Consumo_QtUnit { get; set; }

        [Display(Name = "Valor Unid")]
        public string Consumo_VlUnit_VIEW { get; set; }
        public double Consumo_VlUnit { get; set; }

        [Display(Name = "Valor Total")]
        public string Consumo_VlTotal_VIEW { get; set; }
        public double Consumo_VlTotal { get; set; }

        [Required]
        [Display(Name = "(%)")]
        [Range(0, 50, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Consumo_PercDiscount { get; set; }
        [Display(Name = "(-) Desconto")]
        public string Consumo_VlDiscount_VIEW { get; set; }
        public double Consumo_VlDiscount { get; set; }

        [Display(Name = "Valor Final")]
        public string Consumo_VlFinal_VIEW { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Consumo_OBS { get; set; }



        //ENTRETENIMENTO
        public int Entretenimento_ExtractID { get; set; }
        public BookItemStatus Entretenimento_StatusId { get; set; }

        [Required]
        [Display(Name = "Grupo")]
        public int Entretenimento_GroupLevelID { get; set; }

        [Required]
        [Display(Name = "Item")]
        public int Entretenimento_GroupLevelItemID { get; set; }

        public string Entretenimento_NAME { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Entretenimento_Date { get; set; }
        [Required]
        public string Entretenimento_Time { get; set; }

        [Required]
        [Display(Name = "Unid")]
        [Range(1, 100, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Entretenimento_QtUnit { get; set; }

        [Display(Name = "Valor Unid")]
        public string Entretenimento_VlUnit_VIEW { get; set; }
        public double Entretenimento_VlUnit { get; set; }

        [Display(Name = "Valor Total")]
        public string Entretenimento_VlTotal_VIEW { get; set; }
        public double Entretenimento_VlTotal { get; set; }

        [Required]
        [Display(Name = "(%)")]
        [Range(0, 50, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Entretenimento_PercDiscount { get; set; }
        [Display(Name = "(-) Desconto")]
        public string Entretenimento_VlDiscount_VIEW { get; set; }
        public double Entretenimento_VlDiscount { get; set; }

        [Display(Name = "Valor Final")]
        public string Entretenimento_VlFinal_VIEW { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Entretenimento_OBS { get; set; }




        //Ajuste Hospedagem
        public int Ajuste_ExtractID { get; set; }
        public int Ajuste_SeasonDays { get; set; }
        public double Ajuste_SeasonValue { get; set; }
        public double Ajuste_SeasonDiscountVALUE { get; set; }
        public double Ajuste_SeasonAdvance { get; set; }

        [Display(Name = "Entrada")]
        public string Ajuste_DateIn { get; set; }
        [Required]
        [Display(Name = "Nova Saída")]
        public string Ajuste_DateOut { get; set; }
        [Display(Name = "Dia(s)")]
        public string Ajuste_Days_VIEW { get; set; }
        [Display(Name = "Valor Total")]
        public string Ajuste_VlTotal_VIEW { get; set; }
        [Required]
        [Display(Name = "(%)")]
        [Range(0, 50, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Ajuste_PercDiscount { get; set; }
        [Display(Name = "Desconto (-)")]
        public string Ajuste_VlDiscount_VIEW { get; set; }
        [Display(Name = "Adiantamento")]
        public string Ajuste_VlAdvance_VIEW { get; set; }
        [Display(Name = "Valor Final")]
        public string Ajuste_VlFinal_VIEW { get; set; }

        [Required]
        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Ajuste_OBS { get; set; }



        //Check-out
        [Display(Name = "HOSPEDAGEM")]
        public List<Class_Service_Detail> CheckOut_Hospedagem_LIST { get; set; } = new List<Class_Service_Detail>();
        [Display(Name = "CONSUMO")]
        public List<Class_Service_Detail> CheckOut_Consumo_LIST { get; set; } = new List<Class_Service_Detail>();
        [Display(Name = "ENTRETENIMENTO")]
        public List<Class_Service_Detail> CheckOut_Entretenimento_LIST { get; set; } = new List<Class_Service_Detail>();

        [Display(Name = "TOTAL A PAGAR")]
        public string CheckOut_APagar_VIEW { get; set; }
        public double CheckOut_APagar { get; set; }

        [Required]
        [Display(Name = "Forma de Pagamento")]
        public string CheckOut_PayForm { get; set; }

        [Required]
        [Display(Name = "Informação do Pagamento")]
        [StringLength(100)]
        public string CheckOut_OBS { get; set; }
    }

    public class Class_ServiceExtract
    {
        public string ExtractTEXT { get; set; }
        public string ExtractVALUE { get; set; }
    }

    public class Class_Service_LIST
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Tax { get; set; }
    }

    public class Class_Service_Detail
    {
        public string DESC { get; set; }
        public string VALUE { get; set; }
    }

    public class Class_Service_History
    {
        public int ReserveID { get; set; }
        public int ExtractID { get; set; }
        public string ExtractTYPE { get; set; }
        public string ExtractDate { get; set; }

        [Display(Name = "Nome Item")]
        public string ExtractDESC { get; set; }

        [Display(Name = "Quantidade")]
        public string ExtractUNID { get; set; }

        [Display(Name = "Valor Total")]
        public string ExtractValue { get; set; }

        [Display(Name = "Status")]
        public string ExtractStatusNAME { get; set; }
    }
}
