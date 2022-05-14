using ShelterHelper.Core.DataModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelterHelper.Infrastructure.Data.EntityConfigurations
{
    internal class SheltersConfiguration : EntityConfiguration<Shelters>
    {
        public override void Configure(EntityTypeBuilder<Shelters> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .IsRequired();

            builder
                .Property(x => x.MapsLink)
                .IsRequired();

            builder
                .Property(x => x.Capacity)
                .IsRequired();

            builder
                .Property(x => x.MaximumDaysForRental)
                .IsRequired();

            builder
                .Property(x => x.NumberOfUsers)
                .IsRequired();

            builder
                .Property(x => x.Accessibility)
                .IsRequired();

            builder
                .Property(x => x.Pets)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
