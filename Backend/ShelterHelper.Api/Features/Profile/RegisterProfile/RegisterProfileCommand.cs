namespace ShelterHelper.Api.Features.Profile.RegisterProfile
{
    public record RegisterProfileCommand
    {
        public RegisterProfileCommand(string email, string name, string phoneNumber, string address, DateTime birthDate)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            BirthDate = birthDate;
        }

        public string Email { get; init; }
        public string Name { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public DateTime BirthDate { get; init; }
    }
}
