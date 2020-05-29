using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Models
{
    [Table("GroupLevelItemTax")]
    public class Class_GroupLevelItemTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("GroupLevelItem")]
        [DisplayName("Item Grupo")]
        public int GroupLevelItemId { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Valor")]
        public double Tax { get; set; }

        [Required]
        [DisplayName("(%)")]
        public int Percent { get; set; }


        //Constructors
        public Class_GroupLevelItemTax() { }
        public Class_GroupLevelItemTax(int _Id, int _GroupLevelItemId, string _Name, double _Tax, int _Percent)
        {
            Id = _Id;
            GroupLevelItemId = _GroupLevelItemId;
            Name = _Name;
            Tax = _Tax;
            Percent = _Percent;
        }
    }
}
