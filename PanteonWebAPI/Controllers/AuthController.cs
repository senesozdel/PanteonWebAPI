﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.Entities;
using PanteonWebAPI.Models.JwtModel;
using PanteonWebAPI.Models.RequestModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PanteonWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


        public class AuthController : ControllerBase
        {
        private IUser _userService;
            private AppDbContext db;
            private readonly JwtConfigs jwtConfigs;
            public AuthController(AppDbContext db,IOptions<JwtConfigs> jwtConfigs,IUser userService)
            {
                this.jwtConfigs = jwtConfigs.Value;
                this.db = db;
                _userService = userService;
            }

            [HttpPost("Login")]
            public IActionResult Login([FromBody] UserRequest userRequest) 
            {
                var user = CheckId(userRequest);
                if (user == null)    return NotFound();
                var token = CreateToken(userRequest);
                return Ok(token);
            }

            private string CreateToken(UserRequest userRequest)
            {
                var user = CheckId(userRequest);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Key));
                var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                var claimsData = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserName),
                    new Claim(ClaimTypes.Email,user.Email),
                };
                var token = new JwtSecurityToken(

                    issuer: jwtConfigs.Issuer,
                    audience: jwtConfigs.Audience,
                    signingCredentials: credentials,
                    claims: claimsData,
                    expires: DateTime.Now.AddHours(1)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            private User CheckId(UserRequest userRequest)
            {
                return db.Users.FirstOrDefault(x => x.UserName == userRequest.UserName && x.Password == userRequest.Password); 
            }
        }
    }


