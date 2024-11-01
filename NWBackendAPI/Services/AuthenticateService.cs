using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NWBackendAPI.Models;
using NWBackendAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NWBackendAPI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        //key ja tietokantayhteys välittyy dependency injectionin kautta
        private readonly NorthwindOriginalContext db;
        private readonly Appsettings _appSettings;


        public AuthenticateService(IOptions<Appsettings> appSettings, NorthwindOriginalContext nwc)
        {
            _appSettings = appSettings.Value;
            db = nwc;
        }


        public LoggedInUser Authenticate(string username, string password)
        {
            var foundUser = db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // Jos ei käyttäjää löydy palautetaan null authenticatio kontrollerille
            if (foundUser == null)
            {
                return null!;
            }

            // Jos käyttäjä löytyy:
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);

            string accessLevel = "Basic";

            if (foundUser.AccessLevelId == 2)
            {
                accessLevel = "Admin";
            }


            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
    {
                    new Claim(ClaimTypes.Name, foundUser.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1")
    }),
                Expires = DateTime.UtcNow.AddHours(3), // Montako tuntia token on voimassa

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //LoggedUser luokkaan tallennetaan käyttäjän tiedot ja token
            LoggedInUser loggedUser = new LoggedInUser();

            loggedUser.Username = foundUser.Username;
            loggedUser.AccesslevelId = foundUser.AccessLevelId;
            loggedUser.Token = tokenHandler.WriteToken(token);

            return loggedUser; // Palautetaan kutsuvalle controllerimetodille user ilman salasanaa
        }
    }
}
