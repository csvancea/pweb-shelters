using ShelterHelper.Api.Features.Profiles.RegisterProfile;

namespace ShelterHelper.Api.Features.Profiles.UpdateProfile
{
    public record UpdateProfileCommand : RegisterProfileCommand
    {
        public UpdateProfileCommand(string? name, string? phoneNumber, string? address, DateTime? birthDate) : base("x", name, phoneNumber, address, birthDate)
        {
        }

        // Intentional inheritance hiding: Do not allow email to be changed
        private new string? Email { get; set; }
    }
}
