using ShelterHelper.Api.Features.Profile.RegisterProfile;

namespace ShelterHelper.Api.Features.Profile.ViewProfile
{
    public record ProfileDto : RegisterProfileCommand
    {
        public ProfileDto(string email, string name, string phoneNumber, string address, DateTime birthDate) : base(email, name, phoneNumber, address, birthDate)
        {
        }
    }
}
