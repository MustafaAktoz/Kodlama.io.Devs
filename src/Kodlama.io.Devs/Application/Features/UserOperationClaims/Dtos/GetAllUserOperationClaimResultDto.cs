namespace Application.Features.UserOperationClaims.Dtos
{
    public class GetAllUserOperationClaimResultDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string ClaimName { get; set; }
    }
}
