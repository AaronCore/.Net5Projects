using System.IdentityModel.Tokens.Jwt;
using HybridFlow.IdentityServer4.Mvc.Extendsions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HybridFlow.IdentityServer4.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //
            #region MVC client
            //�ر��� JWT �����Ϣ����ӳ��
            //���������� well-known �����Ϣ�����磬��sub�� �� ��idp�����޸��ŵ�������
            //��������Ϣ����ӳ��ġ����������ڵ��� AddAuthentication()֮ǰ���
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("Cookies")

              .AddOpenIdConnect("oidc", options =>
              {

                  options.Authority = "http://localhost:5001";
                  options.RequireHttpsMetadata = false;
                  options.ClientId = "D521A185-77D6-2959-7487-E3587BBE8D85";
                  options.ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
                  options.ResponseType = "code id_token";
                  options.SaveTokens = true;

                  options.GetClaimsFromUserInfoEndpoint = true;
                  //����������ȡ
                  options.Scope.Add("hybrid_scope1"); //�����Ȩ��Դ
                  // Ϊapi��ʹ��refresh_token��ʱ��,����offline_access������
                  //options.GetClaimsFromUserInfoEndpoint = true;
              });

            #endregion
            services.ConfigureNonBreakingSameSiteCookies();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
