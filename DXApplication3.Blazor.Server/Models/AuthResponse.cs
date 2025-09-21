namespace DXApplication3.Blazor.Server.Models
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string[] Roles { get; set; } = Array.Empty<string>();
        public DateTime ExpiresAt { get; set; }
        public long ExpiresIn { get; set; } // Thời gian còn lại tính bằng mili giây
    }
}
