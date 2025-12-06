using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SwitchCMS.API.Services;
using SwitchCMS.API.Services.Authentication;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.APIServices.Authentication;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.DataBaseContext;
using SwitchCMS.Repository;
using SwitchCMS.Repository.Interface;
using SwitchCMS.Utility;

namespace SwitchCMS.API.AppService
{
    public class DIService
    {
        private static IEnumerable<string> JoinRoles(params Roles[] roles)
        {
            List<string> roleNames = new() { nameof(Roles.SuperAdmin) };
            roleNames.AddRange(roles.Select(x => Enum.GetName(x))!);
            return roleNames;
        }

        private static AuthorizationPolicyBuilder AddRoles(AuthorizationPolicyBuilder policy, params Roles[] roles)
        {
            IEnumerable<string> roleNames = JoinRoles(roles);
            policy.RequireAuthenticatedUser().RequireRole(roleNames);
            return policy;
        }

        public static void AddAuthorizationPolicies(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyType.SuperAdmin, policy => AddRoles(policy));
                 

                options.AddPolicy(PolicyType.SuperAdmin, policy => AddRoles(policy, Roles.SuperAdmin));
                options.AddPolicy(PolicyType.CompanyAdmin, policy => AddRoles(policy, Roles.CompanyAdmin));
                //options.AddPolicy(PolicyType.Student, policy => AddRoles(policy, Roles.School, Roles.Teacher, Roles.Student));
                options.AddPolicy(PolicyType.AppAll, policy => AddRoles(policy, Roles.Application));

                options.AddPolicy(PolicyType.AppExternal, policy => AddRoles(policy, Roles.Application)
                    .RequireClaim(UserClaimTypes.Name, ApplicationTokenKey.Application));
            });
        }


        public static void AddSwaggerConfig(WebApplicationBuilder builder)
        {
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Switch API", Version = "v1" });
                option.AddSecurityDefinition(JWTAuthenticationHandler.AuthScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = JWTAuthenticationHandler.AuthHeader,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JWTAuthenticationHandler.AuthScheme
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JWTAuthenticationHandler.AuthScheme
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            

            builder.Services.AddScoped<IDapperContext,DapperContext>();
            
            //user
            builder.Services.AddScoped<IOUSRRepository, OUSRRepository>();
            builder.Services.AddScoped<IOUSRService, OUSRService>();
            builder.Services.AddScoped<IUserLoginService, UserLoginService>();

            //Login
            builder.Services.AddScoped<IUserLoginService, UserLoginService>();   
            builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();
            builder.Services.AddScoped<IJWTService, JWTService>();

            // Company List
            builder.Services.AddScoped<IOCMPRepository, OCMPRepository>();
            builder.Services.AddScoped<IOCMPService, OCMPService>();

            //Country
            builder.Services.AddScoped<IOCRYRepository,OCRYRepository>();
            builder.Services.AddScoped<IOCRYService, OCRYService>();

            // Menus
            builder.Services.AddScoped<IOMENRepository, OMENRepository>();
            builder.Services.AddScoped<IOMENService, OMENService>();

            // User Menu
            builder.Services.AddScoped<IOUMNRepository, OUMNRepository>();

            //Access Company Manager
            builder.Services.AddScoped<IOACARepository, OACARepository>();
            builder.Services.AddScoped<IOACAService, OACAService>();

            //Employee
            builder.Services.AddScoped<IOHEMRepository, OHEMRepository>();
            builder.Services.AddScoped<IOHEMService, OHEMService>();

            // Advatisement Service
            builder.Services.AddScoped<IOADVRepository, OADVRepository>();
            builder.Services.AddScoped<IADV1Repository, ADV1Repository>();
            builder.Services.AddScoped<IOADVService, OADVService>();

            // vehicle
            builder.Services.AddScoped<IOVHLRepository, OVHLRepository>();
            builder.Services.AddScoped<IOVHLService, OVHLService>();

            // Document 
            builder.Services.AddScoped<IOATCRepository, OATCRepository>();
            builder.Services.AddScoped<IOATCService, OATCService>();
            //Employee Resignation
            builder.Services.AddScoped<IHEM1Repository, HEM1Repository>();
            builder.Services.AddScoped<IHEM1Service, HEM1Service>();

            // vehicle Handover 
            builder.Services.AddScoped<IVHL1Repository, VHL1Repository>();
            builder.Services.AddScoped<IVHL1Service, VHL1Service>();
            //Employee Absentee
            builder.Services.AddScoped<IHEM3Repository, HEM3Repository>();
            builder.Services.AddScoped<IHEM3Service, HEM3Service>();
            //Offer Letter
            builder.Services.AddScoped<IHEM4Repository, HEM4Repository>();
            builder.Services.AddScoped<IHEM4Service, HEM4Service>();

            // Employee Direct Notification
            builder.Services.AddScoped<IHEM2Repository, HEM2Repository>();
            builder.Services.AddScoped<IHEM2Service, HEM2Service>();


        }

    }
}
