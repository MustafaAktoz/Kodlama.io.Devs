namespace Application.Features.Applicants.Dtos
{
    public class UpdateGitHubAddressApplicantResultDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? GitHubAddress { get; set; }
    }
}
