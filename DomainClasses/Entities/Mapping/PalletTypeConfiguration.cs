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
    public class PalletTypeConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PalletType>
    {
        public PalletTypeConfiguration()
            : this("dbo")
        {
        }

        public PalletTypeConfiguration(string schema)
        {
            ToTable("QC_PalletType", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(x => x.W).HasColumnName(@"W").HasColumnType("int").IsRequired();
            Property(x => x.L).HasColumnName(@"L").HasColumnType("int").IsRequired();
            Property(x => x.H).HasColumnName(@"H").HasColumnType("nvarchar").IsOptional().HasMaxLength(50);
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
        }
    }

}
// </auto-generated>