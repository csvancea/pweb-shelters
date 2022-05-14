using ShelterHelper.Core.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelterHelper.Infrastructure.Data.EntityConfigurations
{
    internal class UsersConfiguration : EntityConfiguration<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            builder
                .Property(x => x.IdentityId)
                .IsRequired();

            builder
                .HasIndex(x => x.IdentityId)
                .IsUnique();

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .IsRequired();

            builder
                .Property(x => x.BirthDate)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
