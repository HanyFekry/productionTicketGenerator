using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    [Table("Le_SalesOrderOPF")]
    public class SalesOrderOPF
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OPF { get; set; }
        [Required]
        public string SalesOrderNo { get; set; }
        //[ForeignKey("OPF")]
        //public LeoniOPF LeoniOPF { get; set; }

    }
}
