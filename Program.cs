
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using tarefasbackend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = "AGreatTokenAndThatsIT";
var keyToByte = Encoding.ASCII.GetBytes(key);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true, 
        IssuerSigningKey = new SymmetricSecurityKey(keyToByte),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});



builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BDTasks"));
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddMvc();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI();
app.Run();
