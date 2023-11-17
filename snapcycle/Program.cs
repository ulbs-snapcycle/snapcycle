using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.Users.Repository;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services;
using polyclinic_service.Users.Services.Interfaces;
using snapcycle.Images.Repository;
using snapcycle.Images.Repository.Interfaces;
using snapcycle.Images.Services;
using snapcycle.Images.Services.Interfaces;
using snapcycle.UserImages.Repository;
using snapcycle.UserImages.Repository.Interfaces;
using snapcycle.UserImages.Services;
using snapcycle.UserImages.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BASE

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddMySql5()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region REPOSITORIES

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IUserImageRepository, UserImageRepository>();

#endregion

#region SERVICES

builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IImageQueryService, ImageQueryService>();
builder.Services.AddScoped<IImageCommandService, ImageCommandService>();
builder.Services.AddScoped<IUserImageCommandService, UserImageCommandService>();
builder.Services.AddScoped<IUserImageQueryService, UserImageQueryService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/Home/Error");
app.UseDeveloperExceptionPage();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
