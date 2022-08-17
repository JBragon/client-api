using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Business;

namespace DataAccess.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Client");

            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.State)
                .HasColumnName("State")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(t => t.CPF)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();
        }
    }
}
