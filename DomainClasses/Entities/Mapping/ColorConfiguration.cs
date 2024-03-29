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

    // V_QC_Color
    public class ColorConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Color>
    {
        public ColorConfiguration()
            : this("dbo")
        {
        }

        public ColorConfiguration(string schema)
        {
            ToTable("V_QC_Color", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("nvarchar").IsRequired().HasMaxLength(24).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ArName).HasColumnName(@"ArName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.EnName).HasColumnName(@"EnName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
        }
    }

}
// </auto-generated>
