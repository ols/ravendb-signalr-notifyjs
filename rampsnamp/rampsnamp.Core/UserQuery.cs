using Raven.Client;
using Raven.Client.UniqueConstraints;

namespace rampsnamp.Core
{
    public class UserQuery : IUserQuery
    {
        private readonly IDocumentSession _session;

        public UserQuery(IDocumentSession session)
        {
            _session = session;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _session.LoadByUniqueConstraint<User>(x => x.Email, email);
            return AutoMapper.Mapper.Map<User, UserDto>(user);
        }
    }
}