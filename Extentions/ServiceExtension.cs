using ABCMoneyTransfer.Helper.Implementation;
using ABCMoneyTransfer.Helper.Interface;
using ABCMoneyTransfer.Persistence.Context;
using ABCMoneyTransfer.Persistence.Identity;
using ABCMoneyTransfer.Persistence.Repository.Implementation;
using ABCMoneyTransfer.Persistence.Repository.Interface;
using ABCMoneyTransfer.Persistence.UnitOfWork.Implementation;
using ABCMoneyTransfer.Persistence.UnitOfWork.Interface;
using ABCMoneyTransfer.Service.Implementation;
using ABCMoneyTransfer.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace ABCMoneyTransfer.Extentions
{
    public static class ServiceExtension
    {
        //---------------------------[ Inject Repository Here ]---------------------------------
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
        //--------------------------------------------------------------------------------------

        //--------------------------[ Inject Unit Of Work Here ]--------------------------------
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        //--------------------------------------------------------------------------------------

        //-----------------------------[ Inject Service Here ]----------------------------------
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICurrentExchangeService, CurrentExchangeService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransferService, TransferService>();
        }
        //--------------------------------------------------------------------------------------

        //-----------------------------[ Inject Helper Here ]-----------------------------------
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IToastrHelper, ToastrHelper>();
            services.AddTransient<IConsoleHelper, ConsoleHelper>();
        }
        //--------------------------------------------------------------------------------------

        //-------------------------------[ Add Identity ]---------------------------------------
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                //-----------------------------------------[ Password Setting ]------------------------------------------------
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                //-------------------------------------------------------------------------------------------------------------
                //------------------------------------------[ Lockout Setting ]------------------------------------------------
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                //-------------------------------------------------------------------------------------------------------------
                //-------------------------------------------[ User Setting ]--------------------------------------------------
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                //-------------------------------------------------------------------------------------------------------------
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }
        //--------------------------------------------------------------------------------------

        //----------------------------[ Add Authentication ]------------------------------------
        public static void AddAuthenticationConfig(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.ConfigureApplicationCookie(options =>
            {
                //----------------------[ Cookie Setting ]---------------------------
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.LogoutPath = "/account/logout";
                options.SlidingExpiration = true;
                //-------------------------------------------------------------------
            });
        }
        //--------------------------------------------------------------------------------------

    }
}
