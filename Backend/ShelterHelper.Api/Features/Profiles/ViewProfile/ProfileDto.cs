using ShelterHelper.Api.Features.Bookings.ViewBookings;
using ShelterHelper.Api.Features.Profiles.RegisterProfile;

namespace ShelterHelper.Api.Features.Profiles.ViewProfile
{
    public record ProfileDto : RegisterProfileCommand
    {
        public ProfileDto(int id, string email, string? name, string? phoneNumber, string? address, DateTime? birthDate) : base(email, name, phoneNumber, address, birthDate)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
    public record ProfileAndCurrentShelterBookingDto
    {
        public ProfileAndCurrentShelterBookingDto(Core.DataModel.Users user, Core.DataModel.Shelters? shelter = null, Core.DataModel.Bookings? booking = null)
        {
            Profile = new ProfileDto(user.Id, user.Email, user.Name, user.PhoneNumber, user.Address, user.BirthDate);

            if (shelter != null && booking != null)
                CurrentShelter = new RentalHistoryDto(shelter, booking);
        }

        public ProfileDto Profile { get; init; }
        public RentalHistoryDto? CurrentShelter { get; init; }
    }
}
