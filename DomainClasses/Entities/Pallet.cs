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

using DomainClasses.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses
{

    // QC_Pallet
    public class Pallet : ISoftDelete
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; } // Id (Primary key)

        [Required]
        [Display(Name = "Sn")]
        public int Sn { get; set; } // SN

        [Required(AllowEmptyStrings = true)]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Opf")]
        public string Opf { get; set; } // OPF (length: 20)

        [Display(Name = "Gross weight")]
        public double? GrossWeight { get; set; } // GrossWeight

        [Display(Name = "Net weight")]
        public double? NetWeight { get; set; } // NetWeight

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation date")]
        public System.DateTime CreationDate { get; set; } // CreationDate

        [Display(Name = "Is closed")]
        public bool IsClosed { get; set; } // IsClosed

        [DataType(DataType.DateTime)]
        [Display(Name = "Closure date")]
        public System.DateTime? ClosureDate { get; set; } // ClosureDate
        [ForeignKey("PalletType")]
        public int PalletTypeId { get; set; }

        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Quality status")]
        public string QStatus { get; set; } // QualityStatus (length: 20)
        public int H { get; set; }
        public PalletType PalletType { get; set; }

        [Required]
        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child CoileProductivities where [QC_CoileProductivity].[PalletId] point to this entity (FK_QC_CoileProductivity_QC_Pallet)
        /// </summary>
        public System.Collections.Generic.ICollection<CoileProductivity> CoileProductivities { get; set; } // QC_CoileProductivity.FK_QC_CoileProductivity_QC_Pallet
        /// <summary>
        /// Child PalletsQualityStatus where [QC_Pallets_QualityStatuses].[PalletId] point to this entity (FK_QC_Pallets_QualityStatuses_QC_Pallet)
        /// </summary>
        public System.Collections.Generic.ICollection<PalletsQualityStatus> PalletsQualityStatus { get; set; } // QC_Pallets_QualityStatuses.FK_QC_Pallets_QualityStatuses_QC_Pallet

        public Pallet()
        {
            IsDeleted = false;
            CoileProductivities = new System.Collections.Generic.List<CoileProductivity>();
            PalletsQualityStatus = new System.Collections.Generic.List<PalletsQualityStatus>();
        }
    }

}
// </auto-generated>