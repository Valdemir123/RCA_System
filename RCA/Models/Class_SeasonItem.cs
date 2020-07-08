using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCA.Models
{
    [Table("SeasonItem")]
    public class Class_SeasonItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Season")]
        public int SeasonId { get; set; }

        [ForeignKey("GroupLevelItem")]
        public int GroupLevelItemId { get; set; }

        [Required]
        [DisplayName("Taxa em (R$)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Tax { get; set; }
    }

    public class Class_SeasonREL
    {
        public string TypeLine { get; set; }

        [DisplayName("Grupo")]
        public string GroupLevelNAME { get; set; }

        [DisplayName("Item de Preço")]
        public string GroupLevelItemNAME { get; set; }
        public int GroupLevelItemID { get; set; }

        [DisplayName("Temporada")]
        public string SeasonNAME { get; set; }
        public int SeasonID { get; set; }

        public int SeasonItemID { get; set; }
        [DisplayName("Preço (diária/unitário)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double SeasonItemTAX { get; set; }

    }
}
