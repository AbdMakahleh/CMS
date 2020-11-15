using Business.CommandParams;
using DataBase.Locater;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Business.Commands.AuthCommands
{
    public class LoginCommand : Command
    {
        private DBMangerLocator _dBMangerLocator;
        public string Email { get; set; }
        public string Password { get; set; }
        private CommandParam _commandParam;
        public override IResponseResult Execute(ICommandParam param)
        {
            this._commandParam = ((CommandParam)param);
            this._dBMangerLocator = ((DBMangerLocator)this._commandParam.DBManger.Value);
            User user = this._dBMangerLocator.User.Value.GetUserByEmail(this.Email);
            if (user == null)
                return new ResponseResult<object>()
                {
                    Status = false,
                    ErrorMessage = "Invalid Email or password",
                    Data = null,
                    Code = HttpStatusCode.BadRequest
                };

            if (!_validatePassword(user.Password))
                return new ResponseResult<object>()
                {
                    Status = false,
                    ErrorMessage = "Invalid Email or password",
                    Data = null,
                    Code = HttpStatusCode.BadRequest
                };


            UserIdentity userIdentity = new UserIdentity();
            userIdentity.Id = user.Id;

            return new ResponseResult<object>()
            {
                Status = true,
                ErrorMessage = "",
                Data =new { 
                Token= _generateJsonWebToken(userIdentity, this._commandParam.Config),
                User= user  
                } ,
                Code = HttpStatusCode.OK

            };
        }



        private string _generateJsonWebToken(UserIdentity userIdentity, IConfiguration _config)
        {
            List<Claim> claims = new List<Claim>()
             {
                new Claim(ClaimTypes.NameIdentifier, userIdentity.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(this._commandParam.Config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = creds,
                Issuer = _config.GetSection("AppSettings:Issuer").Value,
                Audience = _config.GetSection("AppSettings:Audience").Value
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodeToken = tokenHandler.WriteToken(token);
            return encodeToken;

        }

        private bool _validatePassword(string password) => BCrypt.Net.BCrypt.Verify(this.Password, password);

    }
}