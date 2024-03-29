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

namespace DomainClasses
{

    // Ma_Module
    public class Module : ISoftDelete
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; } // ID (Primary key)

        [Required(AllowEmptyStrings = true)]
        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "En name")]
        public string EnName { get; set; } // EN_Name (length: 25)

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; } // Description (length: 200)

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Icon")]
        public string FaIcon { get; set; } // EN_Name (length: 25)

        [Required]
        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; } // ID (Primary key)

        [Required]
        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Functions where [Ma_Function].[ModuleID] point to this entity (FK_Ma_Function_Ma_Module)
        /// </summary>
        public System.Collections.Generic.ICollection<Function> Functions { get; set; } // Ma_Function.FK_Ma_Function_Ma_Module

        public Module()
        {
            IsDeleted = false;
            Functions = new System.Collections.Generic.List<Function>();
        }
    }

}
// </auto-generated>
