﻿using Application.Services.HttpRequestService;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAuthService
{
    public class UserAuthManager : IUserAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextService _httpContextService;

        public UserAuthManager(IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper, IHttpContextService httpContextService)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenHelper = tokenHelper;
            _httpContextService = httpContextService;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));
            IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            var ipAddress = _httpContextService.GetIpAddress();
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            return await Task.FromResult(refreshToken);
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }
    }
}
