using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChildrenVillageSOS_DAL.Models;

public partial class SoschildrenVillageDbContext : DbContext
{
    public SoschildrenVillageDbContext()
    {
    }

    public SoschildrenVillageDbContext(DbContextOptions<SoschildrenVillageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingSlot> BookingSlots { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemWallet> SystemWallets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); uid=sa; pwd=12345; database=SOSChildrenVillageDB; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC077F943F0D");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingSlotId).HasColumnName("BookingSlot_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.Visitday).HasColumnType("datetime");

            entity.HasOne(d => d.BookingSlot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingSlotId)
                .HasConstraintName("FK__Booking__Booking__5EBF139D");

            entity.HasOne(d => d.House).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Booking__House_i__5CD6CB2B");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__UserAcc__5DCAEF64");
        });

        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC0777B97DB3");

            entity.ToTable("BookingSlot");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("End_time");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SlotTime).HasColumnName("Slot_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("Start_time");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC0725FD79FE");

            entity.ToTable("Child");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AcademicLevel)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Academic_Level");
            entity.Property(e => e.Certificate)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ChildName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Child_Name");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.HealthStatus)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Health_Status");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__House_Id__5535A963");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donation__3214EC071DD15350");

            entity.ToTable("Donation");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.DonationType)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Donation_Type");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__UserAc__5812160E");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC0785B57306");

            entity.ToTable("Expense");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.ExpenseAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Expense_Amount");
            entity.Property(e => e.Expenseday).HasColumnType("datetime");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__House_I__693CA210");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__House__3214EC07E71AFAD2");

            entity.ToTable("House");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.HouseMember).HasColumnName("House_Member");
            entity.Property(e => e.HouseName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("House_Name");
            entity.Property(e => e.HouseNumber).HasColumnName("House_Number");
            entity.Property(e => e.HouseOwner)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("House_Owner");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Village_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Houses)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__House__UserAccou__5165187F");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__Village_I__52593CB8");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC0713B92F1E");

            entity.ToTable("Income");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Receiveday).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.Donation).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Income__Donation__6477ECF3");

            entity.HasOne(d => d.House).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Income__House_Id__66603565");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__UserAcco__656C112C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07DDCC4E5F");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Donation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Payment__Donatio__619B8048");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07BF3C5B7F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<SystemWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemWa__3214EC07FCD611E9");

            entity.ToTable("SystemWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.SystemWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__SystemWal__UserA__6C190EBB");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07C812BE91");

            entity.ToTable("Transaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.IncomeId).HasColumnName("Income_Id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.Donation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Transacti__Donat__6FE99F9F");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__Transacti__Incom__70DDC3D8");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Transacti__Syste__6EF57B66");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC073A8A5C66");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserEmail)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserAccou__Role___4BAC3F29");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Village__3214EC07048EBD00");

            entity.ToTable("Village");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.VillageName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Village_Name");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Villages)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Village__UserAcc__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
