using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            // Configura la clave primaria
            builder.HasKey(c => c.Id);

            // Configura la conversión para Id
            builder.Property(c => c.Id).IsRequired();
            // Configura propiedades adicionales
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);

            // Ignora la propiedad FullName
            builder.Ignore(c => c.FullName);

            // Configura la propiedad Email
            builder.Property(c => c.Email).HasMaxLength(255);
            builder.HasIndex(c => c.Email).IsUnique();

            // Configura la propiedad PhoneNumber
            builder.Property(c => c.PhoneNumber)
                .HasConversion(
                    phoneNumber => phoneNumber.Value,
                    value => PhoneNumber.Create(value)!
                )
                .HasMaxLength(9);

            // Configura la propiedad Address como un objeto de valor
            builder.OwnsOne(c => c.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.Line1).HasMaxLength(50);
                addressBuilder.Property(a => a.Line2).HasMaxLength(50);
                addressBuilder.Property(a => a.City).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(false);
            });

            builder.Property(c => c.Active);
        }
    }
}
