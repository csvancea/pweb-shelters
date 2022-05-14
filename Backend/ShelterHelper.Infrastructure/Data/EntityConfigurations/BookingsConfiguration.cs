using ShelterHelper.Core.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelterHelper.Infrastructure.Data.EntityConfigurations
{
    internal class BookingsConfiguration : EntityConfiguration<Bookings>
    {
        public override void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder
                .Property(x => x.ShelterId)
                .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.CheckInDate)
                .IsRequired();

            builder
                .Property(x => x.ExpectedCheckOutDate)
                .IsRequired();

            builder
                .HasOne<Shelters>()
                .WithMany()
                .HasForeignKey(x => x.ShelterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.Configure(builder);
        }
    }
}
