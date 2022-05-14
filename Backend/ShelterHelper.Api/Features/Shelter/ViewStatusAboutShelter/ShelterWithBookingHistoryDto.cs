﻿using ShelterHelper.Api.Features.Shelter.ViewAllShelters;
using ShelterHelper.Core.DataModel;

namespace ShelterHelper.Api.Features.Shelter.ViewStatusAboutShelter
{
    public record ShelterWithBookingHistoryDto : ShelterDto
    {
        public ShelterWithBookingHistoryDto(int id, string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets) 
            : base(id, name, address, mapsLink, capacity, maximumDaysForRental, numberOfUsers, accessibility, pets)
        {
        }

        public IEnumerable<BookingStatusDto> BookingHistory { get; init; } = new List<BookingStatusDto>();
    }

    public record BookingStatusDto
    {
        public BookingStatusDto(string bookingName, string bookingEmail, string bookingPhone, DateTime expectedCheckOutDate)
        {
            BookingName = bookingName;
            BookingEmail = bookingEmail;
            BookingPhone = bookingPhone;
            ExpectedCheckOutDate = expectedCheckOutDate;
        }

        public string BookingName { get; init; }
        public string BookingEmail { get; init; }
        public string BookingPhone { get; init; }
        public DateTime ExpectedCheckOutDate { get; init; }
    }
}
