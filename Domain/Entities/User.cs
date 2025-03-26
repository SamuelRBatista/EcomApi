namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;  // Senha armazenada de forma segura
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";  // Papel do usuário (pode ser Admin, User, etc.)
        
    }
}
