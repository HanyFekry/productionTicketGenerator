using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    [Table("WOInsColor")]
    public class WoHeadersColor
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; } // ID (Primary key)
        [Required()]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Wo")]
        public string Wo { get; set; } // WO (Primary key) (length: 50)
        [StringLength(20)]
        [Display(Name = "ColorId")]
        public string ColorId { get; set; } // WO (Primary key) (length: 50)
        [Display(Name = "Quantity")]
        public float? Quantity { get; set; } // Quantity}
        public WoHeader WoHeader { get; set; }
        public Color Color { get; set; }

        public WoHeadersColor()
        {

        }
    }
}
