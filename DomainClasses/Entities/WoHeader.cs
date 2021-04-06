// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using DomainClasses.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DomainClasses
{

    // V_PL_WOHeader
    public class WoHeader
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; } // ID (Primary key)
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Wo")]
        public string Wo { get; set; } // WO (Primary key) (length: 50)
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "OPF")]
        public string OPF { get; set; } // OPF (Primary key) (length: 50)

        [MaxLength(120)]
        [StringLength(120)]
        [Display(Name = "Tdsn No")]
        public string TdsNo { get; set; } // TDSNo (length: 120)

        [Required(AllowEmptyStrings = true)]
        [MaxLength(160)]
        [StringLength(160)]
        [Display(Name = "Cable code")]
        public string CableCode { get; set; } // CableCode (Primary key) (length: 160)

        [MaxLength(80)]
        [StringLength(80)]
        [Display(Name = "Conductor type")]
        public string ConductorType { get; set; } // ConductorType (length: 80)

        [MaxLength(240)]
        [StringLength(240)]
        [Display(Name = "Cable type")]
        public string CableType { get; set; } // CableType (length: 240)

        [MaxLength(240)]
        [StringLength(240)]
        [Display(Name = "Standard")]
        public string Standard { get; set; } // Standard (length: 240)

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Size")]
        public string Size { get; set; } // Size (length: 255)

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Size ID")]
        public string SizeId { get; set; } // SizeID (length: 10)

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string Description { get; set; } // Description (length: 255)

        [MaxLength(120)]
        [StringLength(120)]
        [Display(Name = "Volt")]
        public string Volt { get; set; } // Volt (length: 120)

        [Display(Name = "Quantity")]
        public float? Quantity { get; set; } // Quantity

        [Required]
        [Display(Name = "Is finished")]
        public bool IsFinished { get; set; } // IsFinished (Primary key)

        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery date")]
        public System.DateTime? DeliveryDate { get; set; } // DeliveryDate

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Customer")]
        public string Customer { get; set; } // Customer (length: 255)
        public string CustomerType { get; set; } // CustomerType (length: 255)
        public float? StandardLength { get; set; }
        /// <summary>
        /// Child MachineProgramDetails where [PL_MachineProgramDetail].[MachineProgramID] point to this entity (FK_PL_MachineProgramDetail_PL_MachineProgram)
        /// </summary>
        public System.Collections.Generic.ICollection<WoHeadersColor> WoHeadersColors { get; set; }
        //public System.Collections.Generic.ICollection<Color> Colors { get { return WoHeadersColors.Select(w => w.Color).ToList(); } set { } }
        public ICollection<CoileProductivity> CoileProductivities { get; set; }
        public ICollection<DrumProductivity> DrumProductivities { get; set; }

        public WoHeader()
        {
            WoHeadersColors = new List< WoHeadersColor>();
            CoileProductivities = new List<CoileProductivity>();
            DrumProductivities = new List<DrumProductivity>();
            //Colors = new List<Color>();

        }
    }

}
// </auto-generated>