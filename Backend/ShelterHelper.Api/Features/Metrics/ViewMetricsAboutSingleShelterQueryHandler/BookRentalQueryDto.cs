namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    internal class BookRentalQueryDto
    {
        internal BookRentalQueryDto(Core.DataModel.Shelters book, Core.DataModel.Bookings? rental)
        {
            BookName = book.Name;
            AuthorName = book.Author;

            ReaderEmail = rental?.Reader.Name;
            ReaderPhone = rental?.Reader.PhoneNumber;
            RentalStartDate = rental?.RentalStartDate;
            RentalEndDate = rental?.ActualRentalEndDate;
            ExpectedRentalEndDate = rental?.ExpectedRentalEndDate;
        }

        internal string BookName { get; init; }
        internal string AuthorName { get; init; }
        internal string? ReaderEmail { get; init; }
        internal string? ReaderPhone { get; init; }
        internal DateTime? RentalStartDate { get; init; }
        internal DateTime? RentalEndDate { get; init; }
        internal DateTime? ExpectedRentalEndDate { get; init; }

        internal RentalsDto? ValidateAndParseRental()
        {
            if (!string.IsNullOrWhiteSpace(ReaderEmail) &&
                !string.IsNullOrWhiteSpace(ReaderPhone) &&
                ExpectedRentalEndDate.HasValue &&
                RentalStartDate.HasValue)
            {
                return new RentalsDto(ReaderEmail, ReaderPhone, ExpectedRentalEndDate.Value, RentalStartDate.Value, RentalEndDate);
            }

            return null;
        }
    }
}
