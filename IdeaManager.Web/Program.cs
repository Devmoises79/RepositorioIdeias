using Microsoft.EntityFrameworkCore;
using IdeaManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Adicionar DbContext do PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// ==========================================
// REDIRECTS
// ==========================================

// Redirect da rota /Ideas para /Ideas/Index
app.MapGet("/Ideas", context =>
{
    context.Response.Redirect("/Ideas/Index");
    return Task.CompletedTask;
});

// Redirect da raiz para /Ideas/Index (opcional - descomente se quiser)
// app.MapGet("/", context =>
// {
//     context.Response.Redirect("/Ideas/Index");
//     return Task.CompletedTask;
// });

// ==========================================

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();