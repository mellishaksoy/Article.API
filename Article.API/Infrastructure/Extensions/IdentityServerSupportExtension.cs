using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Article.API.Infrastructure.Extensions
{
    public static class IdentityServerSupportExtension
    {
        public static void AddAuthorizationAndConfigureIdentityServerSupport(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration.GetSection("IdentityServer")["IdentityServerUrl"];
                    options.ApiName = configuration["ApiName"];
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = SupportedTokens.Both;
                });
        }
    }
}
