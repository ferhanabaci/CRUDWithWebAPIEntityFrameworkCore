namespace TelephoneDirectory
{
    public class TokenModel
    {
        public int Id { get; set; } 
        public int UserId { get; set; }

        public string Token { get; set; }
        public  DateTime ExpireDate { get; set; }
    }
}
