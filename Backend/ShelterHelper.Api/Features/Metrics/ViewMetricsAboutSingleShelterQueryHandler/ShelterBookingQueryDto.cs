namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    internal class ShelterBookingQueryDto
    {
        internal ShelterBookingQueryDto(Core.DataModel.Shelters shelter, Core.DataModel.Bookings? booking)
        {
            ShelterId = shelter.Id;
            ShelterName = shelter.Name;
            ShelterTotalNumberOfRefugees = shelter.NumberOfUsers;

            UserId = booking?.UserId;
            UserName = booking?.User.Name;
            UserEmail = booking?.User.Email;
            UserPhone = booking?.User.PhoneNumber;
            UserBirthDate = booking?.User.BirthDate;
            UserCheckInDate = booking?.CheckInDate;
            UserCheckedOut = booking?.ActualCheckOutDate != null;

            if (UserCheckedOut)
                UserCheckOutDate = booking?.ActualCheckOutDate;
            else
                UserCheckOutDate = booking?.ExpectedCheckOutDate;
        }

        internal int ShelterId { get; set; }
        internal string ShelterName { get; init; }
        internal int ShelterTotalNumberOfRefugees { get; init; }
        internal int? UserId { get; init; }
        internal string? UserName { get; init; }
        internal string? UserEmail { get; init; }
        internal string? UserPhone { get; init; }
        internal DateTime? UserBirthDate { get; init; }
        internal DateTime? UserCheckInDate { get; init; }
        internal DateTime? UserCheckOutDate { get; init; }
        internal bool UserCheckedOut { get; init; }

        internal RefugeeDto? ValidateAndParseRefugee()
        {
            if (UserId.HasValue &&
                !string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(UserEmail) &&
                !string.IsNullOrWhiteSpace(UserPhone) &&
                UserCheckInDate.HasValue &&
                UserCheckOutDate.HasValue/* &&
                RentalStartDate.HasValue*/)
            {
                return new RefugeeDto(UserId.Value, UserName, UserEmail, UserPhone, UserBirthDate, UserCheckInDate.Value, UserCheckOutDate.Value, UserCheckedOut);
            }

            return null;
        }
    }
}
