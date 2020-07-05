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
        public int SeasonItemID { get; set; }

        public double SeasonItemTAX { get; set; }

        public int SeasonItem_GroupLevelItemID { get; set; }
        public string SeasonItem_GroupLevelItemNAME { get; set; }
    }
}
