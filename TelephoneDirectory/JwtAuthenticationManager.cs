using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace TelephoneDirectory
{
    public  class JwtAuthenticationManager
    {
        private readonly string key;
        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            

            //ilk önce  bir jwt güvenlik belirteci işleyicisi olusturuyoruz
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //Belirtec anahtarı olusturuyoruz
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // Güvenlik belirteci tanımlayıcısı oluşturmamız gerekecek 
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Bir özneye ihtiyacımız var konunun ne oldugunuz burda belirtmeliyiz 

                Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username )
                    }),

                // bir süre ayarı yapmamız gerekiyor, bu süre ayarı yaparken aynı zamanda kimlik bilgileri algoritmayı da tanımlamam gerekiyor.
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            //Yukardaki tanımlamalar bittikten sonra  bi belirtec değişkeni tanımlamız gerekiyor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
