﻿using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace EntityFrameworkStorage.IdentityServer4.EF
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiScope> GetApiScopes =>
            new ApiScope[]
            {
                 new ApiScope("client_scope1"),
                 new ApiScope("password_scope1"),
                 new ApiScope("Implicit_scope1"),
                 new ApiScope("code_scope1"),
                 new ApiScope("hybrid_scope1")
            };

        public static IEnumerable<ApiResource> GetApiResources =>
            new ApiResource[]
            {
                 new ApiResource("client.api1","client.api1")
                {
                    Scopes={"client_scope1" },
                },
                 new ApiResource("password.api1","password.api1")
                {
                    Scopes={ "password_scope1" },
                    UserClaims={JwtClaimTypes.Role},  //添加Cliam 角色类型
                    ApiSecrets={new Secret("apipwd".Sha256())}
                },
                 new ApiResource("implicit.api1","implicit.api1")
                {
                    Scopes={ "Implicit_scope1" },
                    UserClaims={JwtClaimTypes.Role},  //添加Cliam 角色类型
                    ApiSecrets={new Secret("apipwd".Sha256())}
                },
                  new ApiResource("code.api1","code.api1")
                {
                    Scopes={ "code_scope1" },
                    UserClaims={JwtClaimTypes.Role},  //添加Cliam 角色类型
                    ApiSecrets={new Secret("apipwd".Sha256())}
                },
                new ApiResource("hybrid.api1","hybrid.api1")
                {
                    Scopes={ "hybrid_scope1" },
                    UserClaims={JwtClaimTypes.Role},  //添加Cliam 角色类型
                    ApiSecrets={new Secret("apipwd".Sha256())}
                }
            };

        public static IEnumerable<Client> GetClients =>
            new Client[]
            {
                //客户端模式
                new Client
                {
                    ClientId = "credentials_client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "client_scope1" }
                },
                // 密码模式
                new Client
                {
                    ClientId = "password_client",
                    ClientName = "Resource Owner Password",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "password_scope1" }
                },
                //简化模式
                new Client
                {
                    ClientId = "Implicit_client",
                    ClientName = "Implicit Auth",

                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris ={
                    "http://localhost:5002/signin-oidc", //跳转登录到的客户端的地址
                    },
                    // RedirectUris = {"http://localhost:5002/auth.html" }, //跳转登出到的客户端的地址
                    PostLogoutRedirectUris ={
                        "http://localhost:5002/signout-callback-oidc",
                    },
                    //ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = {
                           IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                         "Implicit_scope1"
                     },
                      //允许将token通过浏览器传递
                     AllowAccessTokensViaBrowser=true,
                     // 是否需要同意授权 （默认是false）
                      RequireConsent=true
                 },
                //授权码模式
                new Client
                {
                    ClientId = "code_client",
                    ClientName = "code Auth",

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris ={
                    "http://localhost:5002/signin-oidc", //跳转登录到的客户端的地址
                    },
                    // RedirectUris = {"http://localhost:5002/auth.html" }, //跳转登出到的客户端的地址
                    PostLogoutRedirectUris ={
                        "http://localhost:5002/signout-callback-oidc",
                    },
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = {
                           IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                         "code_scope1"
                     },
                      //允许将token通过浏览器传递
                     AllowAccessTokensViaBrowser=true,
                     // 是否需要同意授权 （默认是false）
                      RequireConsent=true
                 },
                //混合模式
                new Client
                {
                    ClientId = "hybrid_client",
                    ClientName = "hybrid Auth",
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RequirePkce = false,//v4.x需要配置这个

                    RedirectUris ={
                    "http://localhost:5002/signin-oidc", //跳转登录到的客户端的地址
                    },
                    // RedirectUris = {"http://localhost:5002/auth.html" }, //跳转登出到的客户端的地址
                    PostLogoutRedirectUris ={
                        "http://localhost:5002/signout-callback-oidc",
                    },

                    AllowedScopes = {
                           IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                         "hybrid_scope1"
                     },
                      //允许将token通过浏览器传递
                     AllowAccessTokensViaBrowser=true,
                    // AllowOfflineAccess=true,
                     // 是否需要同意授权 （默认是false）
                      RequireConsent=true
                 }
            };
    }
}
