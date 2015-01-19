namespace rampsnamp.Core
{
    public interface IUserQuery
    {
        UserDto GetUserByEmail(string email);
    }
}