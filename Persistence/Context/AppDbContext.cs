using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABCMoneyTransfer.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        //---------------[ Set Here ]-----------------
        public DbSet<Transaction> Transactions { get; set; }
        //--------------------------------------------
    }
}
