namespace ZV.Application.Dtos.Response
{
    public class UserInfoResponseDto
    {
        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? UserStatus { get; set; }
    }
}
