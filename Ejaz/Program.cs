using Ejaz.Extensions;
using Ejaz.Middleware;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Application.Emails;
using Application.Emails.Interfaces;
using React;
using Microsoft.AspNetCore.Mvc;
using Firebase.Storage;
using StackExchange.Redis;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Specify log file path here
    .CreateLogger();

try
{
    Log.Information("Starting host");

    builder.Services.AddControllers(opt =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        opt.Filters.Add(new AuthorizeFilter(policy));
    });

    builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
    {
        //Credential = GoogleCredential.FromJson("{\n  \"type\": \"service_account\",\n  \"project_id\": \"ejaz-ca748\",\n  \"private_key_id\": \"0111eaf94dd433dedc502cdcc6c7b300b1310a00\",\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCsEXZTDZPQa70n\\nqFQ4Xr1V6GODniufnq0mRY4Yt6jNL5j/b2CG5go0KLPZ7OCEul51aM9rXz7PJqWp\\nflrnjxUr7rns58EdcPFX084dCrsyCTDRW3zG7KxecfS9niSkApn1c8NCtq+4bdPH\\n5Fzb7uWwm4F0cglhBOAMiArWi6lTrbsT1lfGeL1LK7M7kJxjipTg92dGJJMsu3BO\\ndlBbGpuf2Yq+u6laHrXkB/fL94RGqvmJiNk4C8qVmTL0dxrl0XNZMKfOpsGKHing\\nkTuUPhRqTCNUdnMbeETtfwf7wP9ZlVOOhcIlFjALBSQPEvANrqgZzP3a8hvVtJZj\\nbA0ViAg9AgMBAAECggEAH6PaYS1G9/VHxAoU6oSpRinEWshz9xcDed0iYvMqrAhL\\nlvTRCB021R6C69i99Eoc4nQSaAkMkKTno4Ijjx7lYr20HJcFELfa+S5uYTK+91Vv\\nT6AGEsIkc/16Zn+09x+vVc4ioprbNwui/MfGN373Z4FAzbvGc7ukw0kkzQzDZypm\\nWsIyssbWYsCx/DlSagdFcF3N9FrZy04QVSH2MDibd09MXZhdKRE5lpw1P5JHmlxz\\nkgK0zPuuX2Oyu/8uunwxoWRiFsJ9xP+8jStkuaM+9cfvi6HBRT6Nrz+Ec8Phgm3I\\nYj06t2xAyGThOw8zCF+ngmf8yA6XCr9c4F32+kyhtQKBgQDsC11AWf78LpJAkiyk\\nfZMiCqfrWA4t/uaWhvv9XOCc7M2Zu3U/MjjrPAWdKhUO/YmeUtxPPKGe08VhY0QW\\nWl5hbPhA4HYXGu28H63/xuo0PMYC5K78JwDpccIoqwmKSEyiRubR/r5njdtOPyZQ\\nFp870FtqDH3I3QgjORqoOi7onwKBgQC6nXs0wsvFMvd0NtH4+Wkhp2aRh+Qsag76\\nuRcMcOafYjNarZCqESjMrYlmLAnGe/a5E6jYwgbqdFo99Kk4TMNNdF3ixj2nv06l\\nWTkD6TL6j6XV7NoTXhcmKuHnw5eLXbtCn88Y1m1cmlvJHDLv+bjnDJfPlV3N60K7\\nL1sdr981owKBgG5nHlWkAlR3f+Z9uC2zombQrxuvkupGiBmtxx0JHPGo/L1D5aIK\\nXn8AEuwgq7rYMU8BGJRjGoEMMgzL/iOqXuyYqEDH+9Pwv3M3Kg4xPofySPc/eOzt\\nXUrS72pQHWU1Tcq0+O8NhIcw3XtqpAPfoNi8KpWehBK39z9qTSKUHC6nAoGBAJkn\\niehBPq83JvNP0n+9YdTm6DzkBBWXbXfD0/C37+46z97Jt4J71ro0aiFaXyNwtYor\\nYlWS184vc6iJVSAJj/fPWwY0oIE9drQpR6u4Bcixf+7UFh3zCJigdQqa8ZLFeFwz\\nf+nogZSQcmfZJszgbCnjoewRsAzB2eB5+xOgNVZ1AoGBAIkMR1djbsugFl91twu+\\nVCXVB9lhImGQ8YDdrIUbYIbtYT8cql9ybesD3ygToqGo7Iu9gGLujQLYQqMnY8S/\\nlLnZuKLOTljDk+7y1h7LdesD0Rho4sfyD/5pLqYFxiDymnMnv6BMdAls2YihiR4K\\nd8jNhlNFeC7XdWWfYV1oXDc9\\n-----END PRIVATE KEY-----\\n\",\n  \"client_email\": \"firebase-adminsdk-keo3z@ejaz-ca748.iam.gserviceaccount.com\",\n  \"client_id\": \"109885726526989325792\",\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-keo3z%40ejaz-ca748.iam.gserviceaccount.com\",\n  \"universe_domain\": \"googleapis.com\"\n}")

        //Credential = GoogleCredential.FromJson("{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"ejaz-290e6\",\r\n  \"private_key_id\": \"62b6a07c943e6f69f22d8fc05e4216aad6625273\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCr3rpYMLTU4An4\\n38u/IU889qWvsoS7fvdHXXNrEWHJIbq9Y/Uuhk5XcrFZCDcZBmAzGKvExTPMwm38\\neT7tBtqgFQZECbVm+3wY4atzuxDz+PsX/zuMy6TpdAtsKkC5/5xi8l6SUpZfDdTj\\ngfSGASs03jKO6lW2NNmaz0hhV3vFfz+KgYdJ73eKwLYqMnOefiilLhsfx92wvRuU\\nogDg83sDdeV2FCM3XTjXpXKsIfWC5t1Tb+TcZPXiBBDaCnf4oIPAo1LTSbyXgldD\\nQckQu36qB1zPxlfJ7BJYYJ0BZltuMQ3C6vMmqsCaRL5781eob6RI8weAXIOpIyBz\\nTmDdLRmJAgMBAAECggEAD9E0ylS6MayoMkmrWzzOKWzEGRA3d7dyu7RBoOEFxDBI\\n2rDyMazN1S2ynWBcjL6p2A39UeO5tY6Pt3hzlnYG5QxmShjwI0kB9dq/8AvPXrqj\\nZiUYxod/1vXVbsgRMqTv8CnA0xvLZ7ZSyTjOp44AO1J3KfCuQHFq9Ny+eynlU8qw\\nDRTqUmJBTCDehKcTq4UH8T4nct0JwIOTHoEGNfKZv8oOtZy1TGBmgHZVwwHMqaj8\\n3SJ/qnXi04RyJ8fnLebbEqEs2gkwhlZ56nMfax02MhkfeHBTDIbIPHlgK/4Gpa1c\\nGZZ5zOR90e35Az0eXPEf7xK2E3axIRWuu/LxVMZF0QKBgQDyJCXxhmYuMTaEn2wY\\nLA21KpsRFnrwxDfsbrNCBCDCuYjxtuu74qlPx8NRHXuYVdMzB4s3zsF1lDpMTUwP\\nHOrncFvIJxyAzsT2/TKsrRu1JXacb0cO3jV+rwAngEqPeZuSdPmAy8UZNfQmp3Ch\\nu1U5kRMKcDdvI2wfvE3rJ6DGkQKBgQC1tPd/yTXCHCRLH+1FSA9vR/R33dkobd9g\\n/EyB5w4UwQMwG0QwACZXWZBMfHRTNsIo5KsSMK4NbRBMWkZIOf5Z75yYrPiFGdXs\\nxNGLjWvf78yRr2UMmj9rA+a8vibPfFEHHUF/Mz8juJj8KRN8Oa9BKWtFFDq5GGP7\\nujGNF6DPeQKBgEnvZbKQ0iRqnHX0Y8IhuXHG96BJF/LqRxUnXA7dSc+LbVg4/VPa\\nSs50dZwP/4wtMJbVR9obhJDNWNuxgnbe4o8WjL/ZyWc/O38bItz84of2T3hGthDB\\nIt5yuv5Uuu+CN1GJ7CE6lw0yn0EqQUbw933jYUf/qb2CuGIcUkJqUDoxAoGAIErT\\n/WrF2gRkUtGOulRPIei4wqCzmiLEoxjTg/aLIxcWOPNvj9RG/BkXsvAfQDaoFNpm\\nqKnsFMjAL8GPYgCkellCDEQZOpZX1Wc5EkME29xjD7ULEU1MXflohTyJ6y/dupTx\\nceiqnO+OiKnII7igs8TnonQw53o1MqRpb/i2BIkCgYAlIEbqF7Zjpk09aXmVmTHB\\na0fKZhxli1g+7riPn51VTmADmXfuUyc4wqeYz8VF9d/WVOUhA+BLKxxBQ/vq6Rx0\\nnLRCLMIo8BU224+PeXk9+Ek7nRqW0/J2ldHb8Kuzo51Eex7pxQHv2bNnmW80wgZx\\n5BWPK0M+Dj2XVs5wsY9vyw==\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-hzd1m@ejaz-290e6.iam.gserviceaccount.com\",\r\n  \"client_id\": \"104960540607983573300\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-hzd1m%40ejaz-290e6.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}")

        Credential = GoogleCredential.FromJson("{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"ejaz-290e6\",\r\n  \"private_key_id\": \"1a48581dfb4dc74655462748e9f188cfb3fbf34e\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCvcDSWKn/czqfl\\nD6tSrq1xrGO/D+bfJ9gQoEjSMjXVee1zCBFN/NYbdGewJYr7sJTkqYcpjdrPxzI1\\nTwd4lEXaHU2aOOrrMyGIGpAV5omzXrCpS4YmIZiWqSZgfrdBVXjG3zAWrxXJ9Kxd\\n1M2layru6unBgCJFigUs/spCHQuvtbJuVAeZ1wDOexcY4BDxXxVxKdCEXJ8wzBb0\\n0NfztL3ioa3cUOc1VQot8HJKN8ml4I/vJ1OlxM9bhx1cRSX70F2SDWMrbN26e4tq\\nuAyn9iZeXtjCfrQfqEz2ynrXErL2f+v2jJkP30RA85eIBVOPqHuxH6KutMEXz+jJ\\nqOR8rRHNAgMBAAECggEAOfyGzRmIAJYnpUzsDhyhRGC76VNatmingDtSRnOFGqW9\\nx5cjjRjkNdfgS7bk/LLNS9UC0UfLSoDnBfq0l/S+L80esLpeX1Ybn65T8IEuA2Tp\\nVFObp+/TzPxkrGFFtekHzgfID79YHtPpwuvK+wF0g9qOX8A3068+bbRbasXJ7o29\\nmi8iJrouVHN6GJb3H4xLlISYUImBrEzjFwt4BmzfyY231Fch7cPCC1+3LkG0+YkO\\nKx8n6uXN1eJFf6NA9eQ6T0WYJBqqIlAGUK9CWIUiKgVn59xnjMia46Td3lvDIYA7\\nXIIjF9cdPoLSaKUCYqc8HDhxmxooGbEb03YUf56EWwKBgQDoK7qe8knWlwFvDRvz\\n7crkGBTvhnLTIAU411lX7Ak0BgGnO4d+/AzeB2PLGSaL22bpmHca9idrBW55omUf\\n8klJFqrTrDccglzhd9c35kn02QQaxj/aQZTqBXh/I61rn3WUBteQLjWEaKQWeu8N\\nqD5P2UWqnIgth+M25goyct/2wwKBgQDBcdV1g6xgSI4Hy9c3BpNkvdv1XutQM4QG\\ncg4NJwHZqkMDGYKYXc3Kk/QPzQKGZKe1c7dIR4wIvjaeKSiA4r52sPVCy1/rVu2O\\nxrZ2ZAsM0VIvxhQ60N33V0RWxOidTvPU2hd5JJNGrWdfNcixLokGhZWUQoNtDewA\\n2IGI1nHsLwKBgHkPnD8XPZNMqC282y+Fdf8b44bGR4d5Md/iwq6K4H2lCCWob82y\\nJg98MPgNREE3BWGW47xGUGWIroN0P2C6GKao0CiRqycNftr+f1Whmjy3EGHZsB/h\\nhIHGmn9JHjt5KXknXC3NOpCxc6ZZMd+gM+W0+JOvDX8YVU0iBH0r00a7AoGATqsY\\nf/3YcB+RgyIml74Y2vNLLHI5iBgrLOPdSwP+AKL5NZ1+OrCLLEMXEgXxbO4qNeSl\\nOMO/8x5H3/IlAqUzh9lXJJ7Il1B2s4WkVmlBBSlrHvqS54hhfGEE7bSOVMry5jD7\\nMszpB+klDNc2kre2Cezxc6XJBOSQgZeKtjdfhQkCgYAZK/4Dvbb2ZuazR1GcRi/8\\nmESC7ekRG/IofBwo1Au9YXndFYL/qCDqCWTqR/LxCP4OH0q3k5PDS+R6yTiggpMR\\n/y24LUFmLND6fB+qyxsvyOYQ700g0q8SLT+AUPzAHQXKg8LyRRlBCFWGBG6yF8tJ\\nzn4ja1tiX6QjMYH+CaKybQ==\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-hzd1m@ejaz-290e6.iam.gserviceaccount.com\",\r\n  \"client_id\": \"104960540607983573300\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-hzd1m%40ejaz-290e6.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}")
    }));

    ReactSiteConfiguration.Configuration = new ReactSiteConfiguration()
                    .AddScript("C:\\Users\\faisa\\OneDrive\\Desktop\\Mannawar\\web\\Frontend\\newweb\\src\\index.tsx");

    builder.Services.AddSingleton<FirebaseStorage>(provider =>
    {
        var firebaseStorageBucket = "ejaz-backend.appspot.com";
        return new FirebaseStorage(firebaseStorageBucket);
    });


    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);
    builder.Services.Configure<EmailSettings>
    (options => builder.Configuration.GetSection("EmailSettings").Bind(options));
    builder.Services.AddSingleton<IEmailService, EmailNotifier>();
    builder.Services.AddControllers(options =>
    {
        options.CacheProfiles.Add("Default604800",
            new CacheProfile()
            {
                Duration = 604800
            });
    });


    //added output caching policy
    builder.Services.AddOutputCache(options =>
    {
        options.AddBasePolicy(builder => builder.Cache());
    });

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
        options.InstanceName = "SampleInstance";
    });
    // builder.Services.AddResponseCaching();


    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseXContentTypeOptions();
    app.UseReferrerPolicy(opt => opt.NoReferrer());
    app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
    app.UseXfo(opt => opt.Deny());
    app.UseCsp(opt => opt
    .BlockAllMixedContent()
        .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com"))
        .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com", "data:"))
        .FormActions(s => s.Self())
        .FrameAncestors(s => s.Self())
        .ScriptSources(s => s.Self())
    );

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            await next.Invoke();
        });
    }

    app.UseCors("CorsPolicy");
    
    app.UseOutputCache();
    // app.UseResponseCaching();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseDefaultFiles();
    app.UseStaticFiles();


    app.MapControllers();
    app.MapFallbackToController("Index", "Fallback");

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DataContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration");
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

