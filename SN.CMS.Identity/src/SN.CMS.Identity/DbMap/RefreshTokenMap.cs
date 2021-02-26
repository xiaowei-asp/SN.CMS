using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SN.CMS.Identity.Domain;


namespace SN.CMS.Identity.DbMap
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("sn_cms_refreshtoken");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).HasMaxLength(50);
            builder.Property(c => c.Token).HasMaxLength(20);

            builder.Property(c => c.CreateTime);
            builder.Property(c => c.DeleteTime);
            builder.Property(c => c.ModifyTime);
            builder.Property(c => c.IsDeleted);
        }
    }
}
