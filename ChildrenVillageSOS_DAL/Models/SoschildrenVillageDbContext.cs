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

    public virtual DbSet<AcademicReport> AcademicReports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingSlot> BookingSlots { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<FacilitiesWallet> FacilitiesWallets { get; set; }

    public virtual DbSet<FoodStuffWallet> FoodStuffWallets { get; set; }

    public virtual DbSet<HealthReport> HealthReports { get; set; }

    public virtual DbSet<HealthWallet> HealthWallets { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<NecessitiesWallet> NecessitiesWallets { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemWallet> SystemWallets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=SOSChildrenVillageDB;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicReport>(entity =>
        {
            entity.ToTable("AcademicReport");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");

            entity.HasOne(d => d.Child).WithMany(p => p.AcademicReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_AcademicReport_Child1");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC07FC9D6831");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingSlotId).HasColumnName("BookingSlot_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.Visitday).HasColumnType("datetime");

            entity.HasOne(d => d.House).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Booking__House_i__6A30C649");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__UserAcc__6B24EA82");
        });

        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC07B627058A");

            entity.ToTable("BookingSlot");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("End_time");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SlotTime).HasColumnName("Slot_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("Start_time");
            entity.Property(e => e.Status).HasMaxLength(100);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC07CB8F6BE8");

            entity.ToTable("Child");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildName)
                .HasMaxLength(100)
                .HasColumnName("Child_Name");
            entity.Property(e => e.CitizenIdentification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.Gender).HasMaxLength(100);
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(100)
                .HasColumnName("Health_Status");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.Event).WithMany(p => p.Children)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Child_Event");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Child__Facilitie__0B91BA14");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Child__FoodStuff__09A971A2");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Child__HealthWal__08B54D69");

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__House_Id__6C190EBB");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Child__Necessiti__0A9D95DB");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Child__SystemWal__0C85DE4D");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donation__3214EC078ADC5F2D");

            entity.ToTable("Donation");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DonationType)
                .HasMaxLength(200)
                .HasColumnName("Donation_Type");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__UserAc__6E01572D");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Event__Facilitie__10566F31");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Event__FoodStuff__0E6E26BF");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Event__HealthWal__0D7A0286");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Event__Necessiti__0F624AF8");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Event__SystemWal__114A936A");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC072B3C9270");

            entity.ToTable("Expense");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ExpenseAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Expense_Amount");
            entity.Property(e => e.Expenseday).HasColumnType("datetime");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Expense__Facilit__151B244E");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Expense__FoodStu__1332DBDC");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Expense__HealthW__123EB7A3");

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__House_I__6FE99F9F");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Expense__Necessi__14270015");
        });

        modelBuilder.Entity<FacilitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3214EC07CADA00D3");

            entity.ToTable("FacilitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FacilitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Facilitie__UserA__7C4F7684");
        });

        modelBuilder.Entity<FoodStuffWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodStuf__3214EC070FAA2C57");

            entity.ToTable("FoodStuffWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FoodStuffWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__FoodStuff__UserA__7E37BEF6");
        });

        modelBuilder.Entity<HealthReport>(entity =>
        {
            entity.ToTable("HealthReport");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.Height)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Child).WithMany(p => p.HealthReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_HealthReport_Child");
        });

        modelBuilder.Entity<HealthWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthWa__3214EC0724D03DD7");

            entity.ToTable("HealthWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.HealthWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__HealthWal__UserA__7D439ABD");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__House__3214EC0760A35E72");

            entity.ToTable("House");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.HouseMember).HasColumnName("House_Member");
            entity.Property(e => e.HouseName)
                .HasMaxLength(100)
                .HasColumnName("House_Name");
            entity.Property(e => e.HouseNumber).HasColumnName("House_Number");
            entity.Property(e => e.HouseOwner)
                .HasMaxLength(100)
                .HasColumnName("House_Owner");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
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
                .HasConstraintName("FK__House__UserAccou__71D1E811");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__Village_I__72C60C4A");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Village_Id");

            entity.HasOne(d => d.Child).WithMany(p => p.Images)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_Image_Child");

            entity.HasOne(d => d.Event).WithMany(p => p.Images)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Image_Event");

            entity.HasOne(d => d.House).WithMany(p => p.Images)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK_Image_House");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Images)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK_Image_UserAccount");

            entity.HasOne(d => d.Village).WithMany(p => p.Images)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK_Image_Village");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC07932250B9");

            entity.ToTable("Income");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Receiveday).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.Donation).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Income__Donation__787EE5A0");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Income__Faciliti__19DFD96B");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Income__FoodStuf__17F790F9");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Income__HealthWa__17036CC0");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Income__Necessit__18EBB532");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Income__SystemWa__160F4887");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__UserAcco__797309D9");
        });

        modelBuilder.Entity<NecessitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Necessit__3214EC07C0136BC1");

            entity.ToTable("NecessitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.NecessitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Necessiti__UserA__7F2BE32F");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07A09223C8");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.Donation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Payment__Donatio__7A672E12");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07E999D2FF");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<SystemWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemWa__3214EC073EB8F3E6");

            entity.ToTable("SystemWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.SystemWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__SystemWal__UserA__7B5B524B");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07C02621F3");

            entity.ToTable("Transaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IncomeId).HasColumnName("Income_Id");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(200);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.Donation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Transacti__Donat__00200768");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Transacti__Facil__04E4BC85");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Transacti__FoodS__02FC7413");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Transacti__Healt__02084FDA");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__Transacti__Incom__01142BA1");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Transacti__Neces__03F0984C");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Transacti__Syste__05D8E0BE");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Transacti__UserA__6EF57B66");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC07495D81F3");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserAccou__Role___06CD04F7");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Village__3214EC0784533E5B");

            entity.ToTable("Village");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.VillageName)
                .HasMaxLength(200)
                .HasColumnName("Village_Name");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Villages)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Village__UserAcc__07C12930");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
