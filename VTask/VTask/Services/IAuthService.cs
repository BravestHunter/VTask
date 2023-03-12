﻿using System.Threading.Tasks;
using VTask.Model.DAO;
using VTask.Model.DTO.Auth;

namespace VTask.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<RegisterResponseDto> Register(RegisterRequestDto request);
    }
}
