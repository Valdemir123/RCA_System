using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RCA.Models
{
    public class Class_Services
    {
       
        public int TabDefault { get; set; }

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
        public List<Class_Service_LIST> Consumo_GroupLevel_LIST { get; set; } = new List<Class_Service_LIST>();

        [Required]
        [Display(Name = "Item")]
        public int Consumo_GroupLevelItemID { get; set; }
        public List<Class_Service_LIST> Consumo_GroupLevelItem_LIST { get; set; } = new List<Class_Service_LIST>();
        public string Consumo_NAME { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Consumo_Date { get; set; }

        [Required]
        [Display(Name = "Unid")]
        [Range(1, 100,ErrorMessage ="{0}: minimo {1}, Máximo {2}")]
        public int Consumo_QtUnit { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Consumo_VlUnit { get; set; }

        [Display(Name = "Valor Total")]
        public string Consumo_VlTotal_VIEW { get; set; }

        [Required]
        [Display(Name = "(-) Desconto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Range(0, double.PositiveInfinity)]
        public double Consumo_VlDiscount { get; set; }

        [Display(Name = "Valor Final")]
        public string Consumo_VlFinal_VIEW { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Consumo_OBS { get; set; }



        //ENTRETENIMENTO
        public int Entreteniment_ExtractID { get; set; }
        public BookItemStatus Entreteniment_StatusId { get; set; }

        [Required]
        [Display(Name = "Grupo")]
        public int Entreteniment_GroupLevelID { get; set; }
        public List<Class_Service_LIST> Entreteniment_GroupLevel_LIST { get; set; } = new List<Class_Service_LIST>();

        [Required]
        [Display(Name = "Item")]
        public int Entreteniment_GroupLevelItemID { get; set; }
        public List<Class_Service_LIST> Entreteniment_GroupLevelItem_LIST { get; set; } = new List<Class_Service_LIST>();
        public string Entreteniment_NAME { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Entreteniment_Date { get; set; }
        public string Entreteniment_Time { get; set; }

        [Required]
        [Display(Name = "Unid")]
        [Range(1, 100, ErrorMessage = "{0}: minimo {1}, Máximo {2}")]
        public int Entreteniment_QtUnit { get; set; }
        public double Entreteniment_VlUnit { get; set; }

        [Display(Name = "Valor Total")]
        public string Entreteniment_VlTotal_VIEW { get; set; }

        [Required]
        [Display(Name = "(-) Desconto")]
        [Range(0, double.PositiveInfinity)]
        public double Entreteniment_VlDiscount { get; set; }

        [Display(Name = "Valor Final")]
        public string Entreteniment_VlFinal_VIEW { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Entreteniment_OBS { get; set; }



        //Check-out
        [Display(Name = "HOSPEDAGEM")]
        public List<Class_Service_Detail> CheckOut_Hospedagem_LIST { get; set; } = new List<Class_Service_Detail>();
        [Display(Name = "CONSUMO")]
        public List<Class_Service_Detail> CheckOut_Consumo_LIST { get; set; } = new List<Class_Service_Detail>();
        [Display(Name = "ENTRETENIMENTO")]
        public List<Class_Service_Detail> CheckOut_Entreten_LIST { get; set; } = new List<Class_Service_Detail>();

        [Display(Name = "Total")]
        public string CheckOut_Total_VIEW { get; set; }
        [Display(Name = "Desconto")]
        public string CheckOut_Discount_VIEW { get; set; }
        [Display(Name = "TOTAL A PAGAR")]
        public string CheckOut_APagar_VIEW { get; set; }

        [Required]
        [Display(Name = "Forma de Pagamento")]
        public string CheckOut_PayForm { get; set; }
        public List<string> CheckOut_PayForm_LIST { get; set; }

        [Required]
        [Display(Name = "Complemento")]
        [StringLength(30)]
        public string CheckOut_PayText { get; set; }

        [Required]
        [Display(Name = "Observação")]
        [StringLength(100)]
        public string CheckOut_OBS { get; set; }



        //Ajuste Hospedagem
        [Required]
        public string Ajuste_AJU_DateOUT { get; set; }
        public int Ajuste_AJU_DateOUT_Days { get; set; }
        public string Ajuste_TOT_DateOUT_Days_VIEW { get; set; }

        public string Ajuste_ATU_Diaria_VIEW { get; set; }
        public string Ajuste_TOT_Diaria_VIEW { get; set; }

        public string Ajuste_ATU_Discount_VIEW { get; set; }
        public string Ajuste_TOT_Discount_VIEW { get; set; }

        [Required]
        public double Ajuste_AJU_Input { get; set; }
        public string Ajuste_ATU_Input_VIEW { get; set; }
        public string Ajuste_TOT_Input_VIEW { get; set; }

        public string Ajuste_ATU_Payment_VIEW { get; set; }
        public string Ajuste_TOT_Payment_VIEW { get; set; }

        [Required]
        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Ajuste_OBS { get; set; }

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
    }

    public class Class_Service_Detail
    {
        public string DESC { get; set; }
        public string UNID { get; set; }
        public string VALOR { get; set; }
        public string DESCONTO { get; set; }
        public string ENTRADA { get; set; }
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
