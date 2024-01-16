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
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using Application.Emails;
using Application.Emails.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson("{\n  \"type\": \"service_account\",\n  \"project_id\": \"ejaz-ca748\",\n  \"private_key_id\": \"0111eaf94dd433dedc502cdcc6c7b300b1310a00\",\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCsEXZTDZPQa70n\\nqFQ4Xr1V6GODniufnq0mRY4Yt6jNL5j/b2CG5go0KLPZ7OCEul51aM9rXz7PJqWp\\nflrnjxUr7rns58EdcPFX084dCrsyCTDRW3zG7KxecfS9niSkApn1c8NCtq+4bdPH\\n5Fzb7uWwm4F0cglhBOAMiArWi6lTrbsT1lfGeL1LK7M7kJxjipTg92dGJJMsu3BO\\ndlBbGpuf2Yq+u6laHrXkB/fL94RGqvmJiNk4C8qVmTL0dxrl0XNZMKfOpsGKHing\\nkTuUPhRqTCNUdnMbeETtfwf7wP9ZlVOOhcIlFjALBSQPEvANrqgZzP3a8hvVtJZj\\nbA0ViAg9AgMBAAECggEAH6PaYS1G9/VHxAoU6oSpRinEWshz9xcDed0iYvMqrAhL\\nlvTRCB021R6C69i99Eoc4nQSaAkMkKTno4Ijjx7lYr20HJcFELfa+S5uYTK+91Vv\\nT6AGEsIkc/16Zn+09x+vVc4ioprbNwui/MfGN373Z4FAzbvGc7ukw0kkzQzDZypm\\nWsIyssbWYsCx/DlSagdFcF3N9FrZy04QVSH2MDibd09MXZhdKRE5lpw1P5JHmlxz\\nkgK0zPuuX2Oyu/8uunwxoWRiFsJ9xP+8jStkuaM+9cfvi6HBRT6Nrz+Ec8Phgm3I\\nYj06t2xAyGThOw8zCF+ngmf8yA6XCr9c4F32+kyhtQKBgQDsC11AWf78LpJAkiyk\\nfZMiCqfrWA4t/uaWhvv9XOCc7M2Zu3U/MjjrPAWdKhUO/YmeUtxPPKGe08VhY0QW\\nWl5hbPhA4HYXGu28H63/xuo0PMYC5K78JwDpccIoqwmKSEyiRubR/r5njdtOPyZQ\\nFp870FtqDH3I3QgjORqoOi7onwKBgQC6nXs0wsvFMvd0NtH4+Wkhp2aRh+Qsag76\\nuRcMcOafYjNarZCqESjMrYlmLAnGe/a5E6jYwgbqdFo99Kk4TMNNdF3ixj2nv06l\\nWTkD6TL6j6XV7NoTXhcmKuHnw5eLXbtCn88Y1m1cmlvJHDLv+bjnDJfPlV3N60K7\\nL1sdr981owKBgG5nHlWkAlR3f+Z9uC2zombQrxuvkupGiBmtxx0JHPGo/L1D5aIK\\nXn8AEuwgq7rYMU8BGJRjGoEMMgzL/iOqXuyYqEDH+9Pwv3M3Kg4xPofySPc/eOzt\\nXUrS72pQHWU1Tcq0+O8NhIcw3XtqpAPfoNi8KpWehBK39z9qTSKUHC6nAoGBAJkn\\niehBPq83JvNP0n+9YdTm6DzkBBWXbXfD0/C37+46z97Jt4J71ro0aiFaXyNwtYor\\nYlWS184vc6iJVSAJj/fPWwY0oIE9drQpR6u4Bcixf+7UFh3zCJigdQqa8ZLFeFwz\\nf+nogZSQcmfZJszgbCnjoewRsAzB2eB5+xOgNVZ1AoGBAIkMR1djbsugFl91twu+\\nVCXVB9lhImGQ8YDdrIUbYIbtYT8cql9ybesD3ygToqGo7Iu9gGLujQLYQqMnY8S/\\nlLnZuKLOTljDk+7y1h7LdesD0Rho4sfyD/5pLqYFxiDymnMnv6BMdAls2YihiR4K\\nd8jNhlNFeC7XdWWfYV1oXDc9\\n-----END PRIVATE KEY-----\\n\",\n  \"client_email\": \"firebase-adminsdk-keo3z@ejaz-ca748.iam.gserviceaccount.com\",\n  \"client_id\": \"109885726526989325792\",\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-keo3z%40ejaz-ca748.iam.gserviceaccount.com\",\n  \"universe_domain\": \"googleapis.com\"\n}")
}));



builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<EmailSettings>
   (options => builder.Configuration.GetSection("EmailSettings").Bind(options));
builder.Services.AddSingleton<IEmailService, EmailNotifier>();

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
