using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public record RegisterUserProfileCommand : ICreateAggregateCommand
    {
        public RegisterUserProfileCommand(string identityId, string email, string name, string phoneNumber, string address, DateTime birthDate)
        {
            IdentityId = identityId;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            BirthDate = birthDate;
        }

        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
