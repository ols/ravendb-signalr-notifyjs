using Raven.Client.UniqueConstraints;

namespace rampsnamp.Core
{
    public class User
    {
        [UniqueConstraint]
        public string Email { get; set; }
        public string Firstname { get; set; } 
    }
}