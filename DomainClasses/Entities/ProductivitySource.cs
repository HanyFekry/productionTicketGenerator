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

using System.ComponentModel.DataAnnotations;

namespace DomainClasses
{

    // QC_ProductivitySource
    public class ProductivitySource
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; } // Id (Primary key)

        [Required(AllowEmptyStrings = true)]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "En name")]
        public string EnName { get; set; } // EnName (length: 50)

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Ar name")]
        public string ArName { get; set; } // ArName (length: 50)

        [Required]
        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child CoileProductivities where [QC_CoileProductivity].[ProductivitySourceId] point to this entity (FK_QC_CoileProductivity_QC_ProductivitySource)
        /// </summary>
        public System.Collections.Generic.ICollection<CoileProductivity> CoileProductivities { get; set; } // QC_CoileProductivity.FK_QC_CoileProductivity_QC_ProductivitySource

        public ProductivitySource()
        {
            IsDeleted = false;
            CoileProductivities = new System.Collections.Generic.List<CoileProductivity>();
        }
    }

}
// </auto-generated>
