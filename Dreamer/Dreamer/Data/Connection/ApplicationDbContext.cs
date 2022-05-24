using Dreamer.Data.Apimodel;
using Dreamer.Data.Inventory;
using Dreamer.Data.Setting;
using Microsoft.EntityFrameworkCore;

namespace Dreamer.Data.Connection
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<AccountGroup> AccountGroup { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<AccountLedger> AccountLedger { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<ExpenseMaster> ExpenseMaster { get; set; }
        public DbSet<FinancialYear> FinancialYear { get; set; }
        public DbSet<IncomeMaster> IncomeMaster { get; set; }
        public DbSet<LedgerPosting> LedgerPosting { get; set; }
        public DbSet<PaymentMaster> PaymentMaster { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<PurchaseMaster> PurchaseMaster { get; set; }
        public DbSet<PurchaseReturnDetails> PurchaseReturnDetails { get; set; }
        public DbSet<PurchaseReturnMaster> PurchaseReturnMaster { get; set; }
        public DbSet<ReceiptMaster> ReceiptMaster { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }
        public DbSet<SalesMaster> SalesMaster { get; set; }
        public DbSet<SalesReturnDetails> SalesReturnDetails { get; set; }
        public DbSet<SalesReturnMaster> SalesReturnMaster { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<StockPosting> StockPosting { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DreamerElectricity");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("TokenId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("ExpiryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("Token")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__user___60FC61CA");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleId");

                entity.Property(e => e.RoleDesc)
                    .IsRequired()
                    .HasColumnName("RoleDesc")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('New Position - title not formalized yet')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserId_2")
                    .IsClustered(false);

                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("EmailAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HireDate)
                    .HasColumnName("HireDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MiddleName")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("Password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasColumnName("Source")
                    .HasMaxLength(100)
                    .IsUnicode(false);


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__RoleId__6E565CE8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}