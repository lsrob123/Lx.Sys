namespace Lx.Membership.Contracts.DTOs
{
    public class MemberUpdateDto : MemberDto
    {
        public string PlainTextPassword { get; set; }
    }
}