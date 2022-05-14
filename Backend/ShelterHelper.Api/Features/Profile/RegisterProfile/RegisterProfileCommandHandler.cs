using ShelterHelper.Core.Domain.UserBookings;

namespace ShelterHelper.Api.Features.Profile.RegisterProfile
{
    public class RegisterProfileCommandHandler : IRegisterProfileCommandHandler
    {
        private readonly IUsersBookingsRepository usersBookingsRepository;

        public RegisterProfileCommandHandler(IUsersBookingsRepository usersBookingsRepository)
        {
            this.usersBookingsRepository = usersBookingsRepository;
        }
        public Task HandleAsync(RegisterProfileCommand command, string identityId, CancellationToken cancellationToken) 
            => usersBookingsRepository.AddAsync(
                new RegisterUserProfileCommand(identityId, command.Email, command.Name, command.PhoneNumber, command.Address, command.BirthDate),
                cancellationToken);
    }
}
