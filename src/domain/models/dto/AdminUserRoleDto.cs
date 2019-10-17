namespace domain.models.dto
{
    public class AdminUserRoleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}