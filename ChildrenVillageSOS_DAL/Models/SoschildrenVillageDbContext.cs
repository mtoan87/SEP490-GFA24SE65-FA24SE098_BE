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

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<DonationDetail> DonationDetails { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); uid=sa; pwd=12345; database=SOSChildrenVillageDB; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__5DE3A5B134BEAA53");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Endtime)
                .HasColumnType("datetime")
                .HasColumnName("endtime");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Starttime)
                .HasColumnType("datetime")
                .HasColumnName("starttime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");
            entity.Property(e => e.Visitday)
                .HasColumnType("datetime")
                .HasColumnName("visitday");

            entity.HasOne(d => d.House).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Booking__house_i__6E01572D");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__userAcc__6EF57B66");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Child__015ADC05080A6A23");

            entity.ToTable("Child");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("child_id");
            entity.Property(e => e.AcademicLevel)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("academic_level");
            entity.Property(e => e.Certificate)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("certificate");
            entity.Property(e => e.ChildName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("child_name");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.HealthStatus)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("health_status");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__house_id__5535A963");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__296B91DCB8AFE3CB");

            entity.ToTable("Donation");

            entity.Property(e => e.DonationId).HasColumnName("donation_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__userAc__5812160E");
        });

        modelBuilder.Entity<DonationDetail>(entity =>
        {
            entity.HasKey(e => e.DonationdetailId).HasName("PK__Donation__87CAA1677434C5C5");

            entity.ToTable("DonationDetail");

            entity.Property(e => e.DonationdetailId).HasColumnName("donationdetail_id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Donation).HasColumnName("donation");
            entity.Property(e => e.DonationId).HasColumnName("donation_id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("village_id");

            entity.HasOne(d => d.DonationNavigation).WithMany(p => p.DonationDetails)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__DonationD__donat__5AEE82B9");

            entity.HasOne(d => d.House).WithMany(p => p.DonationDetails)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__DonationD__house__5CD6CB2B");

            entity.HasOne(d => d.Village).WithMany(p => p.DonationDetails)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__DonationD__villa__5BE2A6F2");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Expense__404B6A6BB76F030E");

            entity.ToTable("Expense");

            entity.Property(e => e.ExpenseId).HasColumnName("expense_id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.ExpenseAmount).HasColumnName("expense_amount");
            entity.Property(e => e.Expenseday)
                .HasColumnType("datetime")
                .HasColumnName("expenseday");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__house_i__6B24EA82");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HouseId).HasName("PK__House__E246264134BD35F4");

            entity.ToTable("House");

            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.HouseMember).HasColumnName("house_member");
            entity.Property(e => e.HouseName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("house_name");
            entity.Property(e => e.HouseNumber).HasColumnName("house_number");
            entity.Property(e => e.HouseOwner)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("house_owner");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("location");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("village_id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Houses)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__House__userAccou__5165187F");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__village_i__52593CB8");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PK__Income__8DC777A6C385197E");

            entity.ToTable("Income");

            entity.Property(e => e.IncomeId).HasColumnName("income_id");
            entity.Property(e => e.DonationId).HasColumnName("donation_id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("house_id");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Receiveday)
                .HasColumnType("datetime")
                .HasColumnName("receiveday");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");

            entity.HasOne(d => d.Donation).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Income__donation__66603565");

            entity.HasOne(d => d.House).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Income__house_id__68487DD7");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__userAcco__6754599E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA6874CDCB");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.PaymentMenthod)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("payment_menthod");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Payment__userAcc__6383C8BA");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC097C5731");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__E2C600EE3509A16C");

            entity.ToTable("UserAccount");

            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserEmail)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserAccou__role___4BAC3F29");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.VillageId).HasName("PK__Village__71031D95935E23CC");

            entity.ToTable("Village");

            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("village_id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("location");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userAccount_id");
            entity.Property(e => e.VillageName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("village_name");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Villages)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Village__userAcc__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
