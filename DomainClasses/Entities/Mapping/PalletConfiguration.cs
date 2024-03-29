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
    public class PalletConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Pallet>
    {
        public PalletConfiguration()
            : this("dbo")
        {
        }

        public PalletConfiguration(string schema)
        {
            ToTable("QC_Pallet", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Sn).HasColumnName(@"SN").HasColumnType("int").IsRequired();
            Property(x => x.Opf).HasColumnName(@"OPF").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(x => x.GrossWeight).HasColumnName(@"GrossWeight").HasColumnType("float").IsOptional();
            Property(x => x.NetWeight).HasColumnName(@"NetWeight").HasColumnType("float").IsOptional();
            Property(x => x.CreationDate).HasColumnName(@"CreationDate").HasColumnType("datetime").IsRequired();
            Property(x => x.ClosureDate).HasColumnName(@"ClosureDate").HasColumnType("datetime").IsOptional();
            Property(x => x.IsClosed).HasColumnName(@"IsClosed").HasColumnType("bit").IsRequired();
            Property(x => x.PalletTypeId).HasColumnName(@"PalletTypeId").HasColumnType("int").IsRequired();
            Property(x => x.QStatus).HasColumnName(@"QStatus").HasColumnType("nvarchar").IsOptional().HasMaxLength(20);
            Property(x => x.H).HasColumnName(@"H").HasColumnType("int").IsOptional();
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
        }
    }

}
// </auto-generated>
