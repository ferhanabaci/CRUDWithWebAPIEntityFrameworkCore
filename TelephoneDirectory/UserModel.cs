using TelephoneDirectory.Enums;

namespace TelephoneDirectory
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public enmTokenTypes UserType { get; set; }
    }
}
