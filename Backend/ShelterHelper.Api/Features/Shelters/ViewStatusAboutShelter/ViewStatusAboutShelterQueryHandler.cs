using ShelterHelper.Api.Web;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Shelters.ViewStatusAboutShelter
{
    public class ViewStatusAboutShelterQueryHandler : IViewStatusAboutShelterQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewStatusAboutShelterQueryHandler(ShelterHelperContext ShelterHelperContext)
        {
            dbContext = ShelterHelperContext;
        }

        public async Task<ShelterWithBookingHistoryDto> HandleAsync(int shelterId, CancellationToken cancellationToken)
        {
            var shelter = await dbContext.Shelters
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == shelterId, cancellationToken);

            if (shelter == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, $"Shelter with id {shelterId} was not found.");
            }

            var shelterWithHistory = new ShelterWithBookingHistoryDto(shelter.Id, shelter.Name, shelter.Address, shelter.MapsLink, shelter.Capacity, shelter.MaximumDaysForRental, shelter.NumberOfUsers, shelter.Accessibility, shelter.Pets, shelter.CreatedAt);

            // TODO
            // var shelterHistory = await dbContext
            //     .Bookings
            //     .AsNoTracking()
            //     .Include(x => x.Refugee)
            //     .SingleOrDefaultAsync(x => x.ShelterId == shelterId && x.ActualCheckOutDate == null, cancellationToken);
            // 
            // bookWithStatus.ExpectedRentalEndDate = rentalOfBook.ExpectedRentalEndDate;
            // bookWithStatus.ReaderEmail = rentalOfBook.Reader.Name;
            // bookWithStatus.ReaderPhone = rentalOfBook.Reader.PhoneNumber;
            // bookWithStatus.RentalDate = rentalOfBook.RentalStartDate;

            return shelterWithHistory;
        }
    }
}
