namespace Lx.Identity.Contracts.DTOs
{
    public class ResetPasswordResponse : ResponseBase
    {
        public string TemporaryPassword { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}