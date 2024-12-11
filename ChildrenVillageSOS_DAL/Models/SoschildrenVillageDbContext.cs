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

            entity.Property(e => e.Achievement).HasMaxLength(255);
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GPA");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Semester).HasMaxLength(50);

            entity.HasOne(d => d.Child).WithMany(p => p.AcademicReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_AcademicReport_Child");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC0743C0FFFD");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingSlotId).HasColumnName("BookingSlot_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("House_id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.House).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Booking__House_i__6C190EBB");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__UserAcc__6D0D32F4");
        });

        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC07833EF046");

            entity.ToTable("BookingSlot");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnName("End_time");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SlotTime).HasColumnName("Slot_time");
            entity.Property(e => e.StartTime).HasColumnName("Start_time");
            entity.Property(e => e.Status).HasMaxLength(100);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC07E8C76F40");

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
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Dob).HasColumnType("datetime");
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
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Child__Facilitie__0E6E26BF");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Child__FoodStuff__0C85DE4D");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Child__HealthWal__0B91BA14");

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__House_Id__6E01572D");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Child__Necessiti__0D7A0286");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Child__SystemWal__0F624AF8");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donation__3214EC07C02870E8");

            entity.ToTable("Donation");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DonationType)
                .HasMaxLength(200)
                .HasColumnName("Donation_Type");
            entity.Property(e => e.EventCode).HasMaxLength(200);
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Child).WithMany(p => p.Donations)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Donation__Child___6FE99F9F");

            entity.HasOne(d => d.Event).WithMany(p => p.Donations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Donation__Event___70DDC3D8");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Donation__Facili__22751F6C");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Donation__FoodSt__208CD6FA");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Donation__Health__1F98B2C1");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Donation__Necess__2180FB33");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Donation__System__236943A5");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__UserAc__6EF57B66");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.EventCode).HasMaxLength(200);
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .HasColumnName("User_Name");
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Village_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Event__Facilitie__1332DBDC");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Event__FoodStuff__114A936A");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Event__HealthWal__10566F31");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Event__Necessiti__123EB7A3");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Event__SystemWal__14270015");

            entity.HasOne(d => d.Village).WithMany(p => p.Events)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK_Event_Village");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC07863D5318");

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
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Expense__Facilit__18EBB532");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Expense__FoodStu__17036CC0");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Expense__HealthW__160F4887");

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__House_I__72C60C4A");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Expense__Necessi__17F790F9");
        });

        modelBuilder.Entity<FacilitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3214EC074E82C04D");

            entity.ToTable("FacilitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FacilitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Facilitie__UserA__7F2BE32F");
        });

        modelBuilder.Entity<FoodStuffWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodStuf__3214EC078555FF71");

            entity.ToTable("FoodStuffWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FoodStuffWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__FoodStuff__UserA__01142BA1");
        });

        modelBuilder.Entity<HealthReport>(entity =>
        {
            entity.ToTable("HealthReport");

            entity.Property(e => e.CheckupDate)
                .HasColumnType("datetime")
                .HasColumnName("Checkup_Date");
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(255)
                .HasColumnName("Doctor_Name");
            entity.Property(e => e.FollowUpDate)
                .HasColumnType("datetime")
                .HasColumnName("Follow_Up_Date");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(50)
                .HasColumnName("Health_Status");
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(100)
                .HasColumnName("Medical_History");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NutritionalStatus)
                .HasMaxLength(100)
                .HasColumnName("Nutritional_Status");
            entity.Property(e => e.VaccinationStatus)
                .HasMaxLength(100)
                .HasColumnName("Vaccination_Status");

            entity.HasOne(d => d.Child).WithMany(p => p.HealthReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_HealthReport_Child");
        });

        modelBuilder.Entity<HealthWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthWa__3214EC074721F54D");

            entity.ToTable("HealthWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.HealthWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__HealthWal__UserA__00200768");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__House__3214EC070503E5EF");

            entity.ToTable("House");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentMembers).HasDefaultValue(0);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FoundationDate).HasColumnType("datetime");
            entity.Property(e => e.HouseMember).HasColumnName("House_Member");
            entity.Property(e => e.HouseName)
                .HasMaxLength(100)
                .HasColumnName("House_Name");
            entity.Property(e => e.HouseNumber).HasColumnName("House_Number");
            entity.Property(e => e.HouseOwner)
                .HasMaxLength(100)
                .HasColumnName("House_Owner");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.LastInspectionDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.MaintenanceStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Good");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
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
                .HasConstraintName("FK__House__UserAccou__74AE54BC");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__Village_I__75A278F5");
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
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC07159C30E1");

            entity.ToTable("Income");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__Income__Donation__7B5B524B");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Income__Faciliti__1DB06A4F");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Income__FoodStuf__1BC821DD");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Income__HealthWa__1AD3FDA4");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Income__Necessit__1CBC4616");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Income__SystemWa__19DFD96B");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__UserAcco__7C4F7684");
        });

        modelBuilder.Entity<NecessitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Necessit__3214EC074DF4CF7F");

            entity.ToTable("NecessitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.NecessitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Necessiti__UserA__02084FDA");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07C6127601");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.DonationId).HasColumnName("Donation_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.Donation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__Payment__Donatio__7D439ABD");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0743DF598A");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<SystemWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemWa__3214EC078C668129");

            entity.ToTable("SystemWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.SystemWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__SystemWal__UserA__7E37BEF6");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC075C2FAA77");

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
                .HasConstraintName("FK__Transacti__Donat__02FC7413");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Transacti__Facil__07C12930");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Transacti__FoodS__05D8E0BE");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Transacti__Healt__04E4BC85");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__Transacti__Incom__03F0984C");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Transacti__Neces__06CD04F7");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Transacti__Syste__08B54D69");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Transacti__UserA__71D1E811");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC072E215EC7");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__UserAccou__Role___09A971A2");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Village__3214EC07FAEA22FD");

            entity.ToTable("Village");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EstablishedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
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
                .HasConstraintName("FK__Village__UserAcc__0A9D95DB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
