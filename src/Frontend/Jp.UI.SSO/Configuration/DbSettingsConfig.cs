﻿using Jp.Infra.CrossCutting.IdentityServer.Configuration;
using Jp.Infra.Migrations.MySql.Configuration;
using Jp.Infra.Migrations.Sql.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.UI.SSO.Configuration
{
    public static class DbSettingsConfig
    {
        public static void ConfigureIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<string>("ApplicationSettings:DatabaseType") == "MySql")
                services.AddIdentityMySql(configuration);
            else
                services.AddIdentitySqlServer(configuration);
        }

        public static void ConfigureIdentityServerDatabase(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment, ILogger logger)
        {
            var identityServerBuilder = services.AddIdentityServer(configuration, environment, logger);
            if (configuration.GetValue<string>("ApplicationSettings:DatabaseType") == "MySql")
                identityServerBuilder.UseIdentityServerMySqlDatabase(services, configuration, logger);
            else
                identityServerBuilder.UseIdentityServerSqlDatabase(services, configuration, logger);
        }
    }
}
