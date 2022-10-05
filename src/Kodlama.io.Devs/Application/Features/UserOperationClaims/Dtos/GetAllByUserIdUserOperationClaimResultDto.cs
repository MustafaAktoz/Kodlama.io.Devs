namespace Application.Features.UserOperationClaims.Dtos
{
    public class GetAllByUserIdUserOperationClaimResultDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string ClaimName { get; set; }
    }
}
