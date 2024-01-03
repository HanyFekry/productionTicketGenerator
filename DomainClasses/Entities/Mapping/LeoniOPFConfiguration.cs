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

namespace DomainClasses.Mapping
{
    using DomainClasses;

    // QC_Pallet
    public class LeoniOPFConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<LeoniOPF>
    {
        public LeoniOPFConfiguration()
            : this("dbo")
        {
        }

        public LeoniOPFConfiguration(string schema)
        {
            ToTable("V_PR_LeoniOPF", schema);
            HasKey(x => new { x.OPF});

            Property(x => x.OPF).HasColumnName(@"OPF").HasColumnType("nvarchar").IsRequired().HasMaxLength(10);
            //HasOptional(x => x.SalesOrderOPF).WithRequired(x => x.LeoniOPF);
        }
    }

}
// </auto-generated>