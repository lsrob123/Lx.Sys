using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Email
{
    public class EmailParticipant : IDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}