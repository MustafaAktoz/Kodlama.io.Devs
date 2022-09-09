namespace Application.Features.ApplicantAuths.Dto
{
    public class LoginApplicantAuthResultDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
