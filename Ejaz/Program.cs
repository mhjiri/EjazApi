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
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;


var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
    options.InstanceName = "SampleInstance";
});

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));

});

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{

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
builder.Services.AddMemoryCache();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default604800",
        new CacheProfile()
        {
            Duration = 604800
        });
});


//added output caching policy


var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

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
app.UseResponseCaching();

// app.UseOutputCache();

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
    // await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
