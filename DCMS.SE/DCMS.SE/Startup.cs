using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DCMS.SE.Data;
using DCMS.SE.Data.Apimodel;
using DCMS.SE.Data.Connection;
using DCMS.SE.Handlers;
using DCMS.SE.Services;
using DCMS.SE.Services.CartService;
using DCMS.SE.Services.Interface;
using DCMS.SE.Services.IService;
using DCMS.SE.Services.Repository;
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


namespace DCMS.SE
{
    /// <summary>
    /// DCMS Simplified Edition(¼ò³ÆDCMS.SE)
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddMudServices();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            //services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //Newtonsoft.Json.JsonReaderException: Error parsing NaN value
            services.AddControllers()
                .AddNewtonsoftJson(opt => 
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

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
            services.AddScoped<ICatagory, CatagoryRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<IStore, StoreRepository>();
            services.AddScoped<IFinancialYear, FinancialYearRepository>();
            services.AddScoped<ITerminal, TerminalRepository>();
            services.AddScoped<IManufacturer, ManufacturerRepository>();
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
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DCMS.SE Account API", Version = "v1.0" });
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
                ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "DCMS.SE Account API Endpoint");
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
