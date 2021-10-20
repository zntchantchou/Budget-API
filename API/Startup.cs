using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Extensions;
using API.Middleware;

namespace API
{
  public class Startup
  {
    public Startup(IConfiguration config)
    {
      _config = config;
    }

    public IConfiguration _config { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddApplicationServices(_config);
      services.AddControllers();
      services.AddCors();
      services.AddIdentityServices(_config);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseMiddleware<ExceptionMiddleware>();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseCors(policy => policy.AllowAnyHeader().AllowAnyHeader().WithOrigins("https://localhost:4200"));
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
