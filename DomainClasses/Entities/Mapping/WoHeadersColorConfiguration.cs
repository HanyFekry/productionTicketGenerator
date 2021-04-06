using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities.Mapping
{

    public class WoHeadersColorConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<WoHeadersColor>
    {
        public WoHeadersColorConfiguration()
            : this("dbo")
        {
        }

        public WoHeadersColorConfiguration(string schema)
        {
            ToTable("V_QC_WO_Colors", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"ID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Wo).HasColumnName(@"WO").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(x => x.Quantity).HasColumnName(@"QtyKm").HasColumnType("real").IsOptional();
            Property(x => x.ColorId).HasColumnName(@"ColorId").HasColumnType("nvarchar").IsOptional().HasMaxLength(24);

            HasRequired(a => a.WoHeader).WithMany(b => b.WoHeadersColors).HasForeignKey(c => c.Wo).WillCascadeOnDelete(false); // FK_Ma_Process_Ma_Section
            HasRequired(a => a.Color).WithMany(b => b.WoHeadersColors).HasForeignKey(c => c.ColorId).WillCascadeOnDelete(false);
        }
    }
}

