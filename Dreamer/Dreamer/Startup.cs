using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Dreamer.Data;
using Dreamer.Data.Apimodel;
using Dreamer.Data.Connection;
using Dreamer.Handlers;
using Dreamer.Services;
using Dreamer.Services.CartService;
using Dreamer.Services.Interface;
using Dreamer.Services.IService;
using Dreamer.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Dreamer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddMudServices();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
   Configuration.GetConnectionString("DefaultConnection")),
ServiceLifetime.Transient);

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddTransient<ValidateHeaderHandler>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddSingleton<WeatherForecastService>();
            services.AddHttpClient<IUserService, UserService>();


            services.AddSingleton<HttpClient>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SeniorEmployee", policy =>
                    policy.RequireClaim("IsUserEmployedBefore1990", "true"));
            });



            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<MailService>();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddScoped<IBrand, BrandRepository>();
            services.AddScoped<IUnit, UnitRepository>();
            services.AddScoped<ITax, TaxRepository>();
            services.AddScoped<ICurrency, CurrencyRepository>();
            services.AddScoped<IProductGroup, ProductGroupRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<ICompany, CompanyRepository>();
            services.AddScoped<IFinancialYear, FinancialYearRepository>();
            services.AddScoped<IAccountLedger, AccountLedgerRepository>();
            services.AddScoped<IWarehouse, WarehouseRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartSalesService, SalesCartService>();
            services.AddScoped<IPurchaseReturnService, PurchaseReturnCartService>();
            services.AddScoped<ISalesReturnService, SalesReturnCartService>();
            services.AddScoped<IPurchaseInvoice, PurchaseInvoiceRepository>();
            services.AddScoped<IVoucherType, VoucherTypeRepository>();
            services.AddScoped<IPaymentSupplier, PaymentSupplierRepository>();
            services.AddScoped<IReceiveCustomer, ReceiveCustomerRepository>();
            services.AddScoped<ISalesInvoice, SalesInvoiceRepository>();
            services.AddScoped<IExpensesMaster, ExpensesMasterRepository>();
            services.AddScoped<IPurchaseReturnInvoice, PurchaseReturnInvoiceRepository>();
            services.AddScoped<ISalesReturnInvoice, SalesReturnInvoiceRepository>();
            services.AddScoped<IInventoryReport, InventoryReportRepository>();
            services.AddScoped<IRole, RolesRepository>();
            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<DataAccess>();
            services.AddScoped<DatabaseConnection>();

            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            //to validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Dreamer Account API", Version = "v1.0" });
            });
            services.AddSingleton<WeatherForecastService>();
        }
        private RequestLocalizationOptions GetLocalizationOptions()
        {
            var cultures = Configuration.GetSection("Cultures")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
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
                app.UseExceptionHandler("/Error");
            }
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRequestLocalization(GetLocalizationOptions());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Dreamer Account API Endpoint");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
