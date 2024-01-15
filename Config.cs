// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Loggerdinates.API.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_api1")
                {
                    Scopes = {"api1.read","api1.write","api1.update"}
                },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1.read","API1 için okuma izni"),
                new ApiScope("api1.write","API1 için yazma izni"),
                new ApiScope("api1.update","API1 için update izni"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile",IdentityServerConstants.LocalApi.ScopeName ,"api1.read","api1.write","api1.update"}
                },
                new Client
            {
                ClientId = "angular-client",
                ClientName = "Angular Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200/" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowedScopes = { "openid", "profile",IdentityServerConstants.LocalApi.ScopeName ,"api1.read","api1.write","api1.update"}
            }
    };
    }
}