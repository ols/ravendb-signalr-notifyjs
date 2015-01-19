using Raven.Client;

namespace rampsnamp.Core
{
    public class UserService : IUserService
    {
        private readonly IDocumentSession _session;

        public UserService(IDocumentSession session)
        {
            _session = session;
        }

        public void CreateUser(CreateUserCommand command)
        {
            _session.Store(new User
            {
                Email = command.Email,
                Firstname = command.Firstname
            });
            _session.SaveChanges();
        }
    }
}