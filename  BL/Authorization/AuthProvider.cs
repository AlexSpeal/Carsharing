﻿using AutoMapper;
using BL.Authorization.Entities;
using BL.Authorization.Exceptions;
using BL.Users.Entities;
using DataAccess.Entities;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace BL.Authorization;


public class AuthProvider(
    SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    string identityServerUri,
    string clientId,
    string clientSecret,  IMapper mapper) : IAuthProvider
{
    public async Task<TokensResponse> AuthorizeUser(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new Exception();
        }

        var verificationPasswordResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!verificationPasswordResult.Succeeded)
        {
            throw new Exception();
        }

        var client = httpClientFactory.CreateClient();
        var discoveryDoc = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (discoveryDoc.IsError)
        {
            throw new Exception();
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDoc.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = clientId,
            ClientSecret = clientSecret,
            UserName = user.UserName,
            Password = password,
            Scope = "api offline_access"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception();
        }

        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
        };
    }
    
    public async Task<UserModel> RegisterUser(string fullName,string email, string password)
    {
        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            throw new AlreadyExistException();
        }
        
        var user = new UserEntity
        {
            FullName = fullName,
            Email = email,
            UserName = email,
        };
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new UserCreationException();
        }
        
        var createUserResult = await userManager.CreateAsync(user, password);
        return mapper.Map<UserModel>(createUserResult);
    }
}