using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Infrastucture.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables

            modelBuilder.Entity<Loan>().ToTable("Loans");
            modelBuilder.Entity<Beneficiary>().ToTable("Beneficiaries");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<PaymentLog>().ToTable("PaymentLogs");
            modelBuilder.Entity<SavingAccount>().ToTable("SavingAccounts");
            modelBuilder.Entity<TransactionLog>().ToTable("TransactionLogs");

            #endregion

            #region Primary Keys
            modelBuilder.Entity<Loan>().HasKey(x => x.Id);
            modelBuilder.Entity<CreditCard>().HasKey(x => x.Id);
            modelBuilder.Entity<PaymentLog>().HasKey(x => x.Id);
            modelBuilder.Entity<SavingAccount>().HasKey(x => x.Id);
            modelBuilder.Entity<TransactionLog>().HasKey(x => x.Id);
            modelBuilder.Entity<Beneficiary>().HasKey(x => new { x.IdUser, x.IdBeneficiary});




            #endregion

            #region Relationships


            #endregion

        }


    }
}
