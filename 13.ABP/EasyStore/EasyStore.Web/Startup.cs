using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace EasyStore.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ע�� abp ��ط���
            services.AddApplication<EasyStoreWebModule>(options =>
            {
                options.UseAutofac();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // ���� asp.net core mvc ��ز���
            app.InitializeApplication();
        }
    }
}
