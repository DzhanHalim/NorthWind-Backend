using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        // making me able to read the data in appsetings.json(WebApi)
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            // get the section of TokenOptions from appsettings.json and bind them together with the TokenOptions class entities(json to class)
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            // expiration time of the token 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
             
            // creating security key with the given security key in appsetings.json
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            // creating the jwt token
            var jwt = new JwtSecurityToken(
                // needed informations for creating a token
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, 
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
                
            );
            return jwt;
        }

        // OperationClaims = Roles
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            // Extension = creating methods for already existing classes like Claim, JwtSecurityToken(NetCore classes).
            var claims = new List<Claim>();
            // instead of doing like this, we are creating a new class Extensions in Extensions folder
            //var email = "";
            //claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
             
            return claims;
        }
    }
}
