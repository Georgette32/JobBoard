using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace JobBoard.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }
        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists");
            }
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role
            };
            user.Password = _passwordHasher.HashPassword(
                user,
                request.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
