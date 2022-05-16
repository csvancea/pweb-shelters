using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;
using System.Net;

namespace ShelterHelper.Api.Features.Profiles.RegisterProfile
{
    public class RegisterProfileCommandHandler : IRegisterProfileCommandHandler
    {
        private readonly IUsersBookingsRepository usersBookingsRepository;

        public RegisterProfileCommandHandler(IUsersBookingsRepository usersBookingsRepository)
        {
            this.usersBookingsRepository = usersBookingsRepository;
        }
        public async Task HandleAsync(RegisterProfileCommand command, string identityId, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetByIdentityAsync(identityId, cancellationToken);

            if (user != null)
            {
                throw new ApiException(HttpStatusCode.Conflict, $"User with identity {identityId} is already registered!");
            }

            await usersBookingsRepository.AddAsync(
                new RegisterUserProfileCommand(identityId, command.Email, command.Name, command.PhoneNumber, command.Address, command.BirthDate),
                cancellationToken);
        }
    }
}
