﻿using System;
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

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingSlot> BookingSlots { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ChildNeed> ChildNeeds { get; set; }

    public virtual DbSet<ChildProgress> ChildProgresses { get; set; }

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

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<NecessitiesWallet> NecessitiesWallets { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SubjectDetail> SubjectDetails { get; set; }

    public virtual DbSet<SystemWallet> SystemWallets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransferHistory> TransferHistories { get; set; }

    public virtual DbSet<TransferRequest> TransferRequests { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); uid=sa; pwd=12345; database=SOSChildrenVillageDB; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicReport>(entity =>
        {
            entity.ToTable("AcademicReport");

            entity.Property(e => e.AcademicYear)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Achievement).HasMaxLength(255);
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Diploma).HasMaxLength(255);
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GPA");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SchoolId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School_Id");
            entity.Property(e => e.SchoolLevel).HasMaxLength(255);
            entity.Property(e => e.Semester).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Child).WithMany(p => p.AcademicReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_AcademicReport_Child");

            entity.HasOne(d => d.School).WithMany(p => p.AcademicReports)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_AcademicReport_School");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC07DF0D4423");

            entity.ToTable("Activity");

            entity.Property(e => e.ActivityName)
                .HasMaxLength(255)
                .HasColumnName("Activity_Name");
            entity.Property(e => e.ActivityType).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.LocationId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Location_Id");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Organizer).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Planned");
            entity.Property(e => e.TargetAudience).HasMaxLength(255);

            entity.HasOne(d => d.Event).WithMany(p => p.Activities)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Activity__Event___634EBE90");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC0714994C3B");

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
                .HasConstraintName("FK__Booking__House_i__2A164134");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__UserAcc__2B0A656D");
        });

        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC0703186617");

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
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC079749A86E");

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
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
            entity.Property(e => e.SchoolId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Child__Facilitie__4D5F7D71");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Child__FoodStuff__4B7734FF");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Child__HealthWal__4A8310C6");

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__House_Id__2BFE89A6");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Child__Necessiti__4C6B5938");

            entity.HasOne(d => d.School).WithMany(p => p.Children)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK__Child__School_Id__2CF2ADDF");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Child__SystemWal__4E53A1AA");
        });

        modelBuilder.Entity<ChildNeed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChildNee__3214EC070CBB16B3");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FulfilledDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NeedType).HasMaxLength(255);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Child).WithMany(p => p.ChildNeeds)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChildNeed__Child__671F4F74");
        });

        modelBuilder.Entity<ChildProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChildPro__3214EC07BC060B0B");

            entity.ToTable("ChildProgress");

            entity.Property(e => e.ActivityId).HasColumnName("Activity_Id");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.ChildProgresses)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__ChildProg__Activ__662B2B3B");

            entity.HasOne(d => d.Child).WithMany(p => p.ChildProgresses)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChildProg__Child__6442E2C9");

            entity.HasOne(d => d.Event).WithMany(p => p.ChildProgresses)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__ChildProg__Event__65370702");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donation__3214EC07258E0C48");

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
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__Donation__Child___2EDAF651");

            entity.HasOne(d => d.Event).WithMany(p => p.Donations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Donation__Event___2FCF1A8A");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Donation__Facili__6166761E");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Donation__FoodSt__5F7E2DAC");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Donation__Health__5E8A0973");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Donation__Necess__607251E5");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Donation__System__625A9A57");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__UserAc__2DE6D218");
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
                .HasConstraintName("FK__Event__Facilitie__5224328E");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Event__FoodStuff__503BEA1C");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Event__HealthWal__4F47C5E3");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Event__Necessiti__51300E55");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Event__SystemWal__531856C7");

            entity.HasOne(d => d.Village).WithMany(p => p.Events)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK_Event_Village");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC07DE1F0454");

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
                .HasConstraintName("FK__Expense__Facilit__57DD0BE4");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Expense__FoodStu__55F4C372");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Expense__HealthW__55009F39");

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__House_I__31B762FC");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Expense__Necessi__56E8E7AB");
        });

        modelBuilder.Entity<FacilitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3214EC07C23AF6C0");

            entity.ToTable("FacilitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FacilitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Facilitie__UserA__3E1D39E1");
        });

        modelBuilder.Entity<FoodStuffWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodStuf__3214EC07296D9E61");

            entity.ToTable("FoodStuffWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FoodStuffWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__FoodStuff__UserA__40058253");
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
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
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
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NutritionalStatus)
                .HasMaxLength(100)
                .HasColumnName("Nutritional_Status");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.VaccinationStatus)
                .HasMaxLength(100)
                .HasColumnName("Vaccination_Status");

            entity.HasOne(d => d.Child).WithMany(p => p.HealthReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_HealthReport_Child");
        });

        modelBuilder.Entity<HealthWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthWa__3214EC079F954EFE");

            entity.ToTable("HealthWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.HealthWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__HealthWal__UserA__3F115E1A");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__House__3214EC075ED8FE94");

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
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__House__UserAccou__339FAB6E");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__Village_I__3493CFA7");
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
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
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
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC078C7BDD0F");

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
                .HasConstraintName("FK__Income__Donation__3A4CA8FD");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Income__Faciliti__5CA1C101");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Income__FoodStuf__5AB9788F");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Income__HealthWa__59C55456");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Income__Necessit__5BAD9CC8");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Income__SystemWa__58D1301D");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__UserAcco__3B40CD36");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC079547B2F8");

            entity.ToTable("Inventory", tb => tb.HasTrigger("trg_CheckBelongsTo"));

            entity.Property(e => e.BelongsTo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BelongsToId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .HasColumnName("Item_Name");
            entity.Property(e => e.LastInspectionDate).HasColumnType("datetime");
            entity.Property(e => e.MaintenanceStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Good");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NecessitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Necessit__3214EC07798BAB6E");

            entity.ToTable("NecessitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.NecessitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Necessiti__UserA__40F9A68C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07D8E8AB2E");

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
                .HasConstraintName("FK__Payment__Donatio__3C34F16F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07FD9A3D88");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.ToTable("School");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.SchoolName).HasMaxLength(255);
            entity.Property(e => e.SchoolType).HasMaxLength(50);
        });

        modelBuilder.Entity<SubjectDetail>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SubjectName).HasMaxLength(255);

            entity.HasOne(d => d.AcademicReport).WithMany(p => p.SubjectDetails)
                .HasForeignKey(d => d.AcademicReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectDetails_AcademicReport");
        });

        modelBuilder.Entity<SystemWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemWa__3214EC071E0784D9");

            entity.ToTable("SystemWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.SystemWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__SystemWal__UserA__3D2915A8");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC0736DBD669");

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
                .HasConstraintName("FK__Transacti__Donat__41EDCAC5");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Transacti__Facil__46B27FE2");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Transacti__FoodS__44CA3770");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Transacti__Healt__43D61337");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__Transacti__Incom__42E1EEFE");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Transacti__Neces__45BE5BA9");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Transacti__Syste__47A6A41B");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Transacti__UserA__30C33EC3");
        });

        modelBuilder.Entity<TransferHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transfer__3214EC07C574D42A");

            entity.ToTable("TransferHistory");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromHouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FromHouseID");
            entity.Property(e => e.HandledBy).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Completed");
            entity.Property(e => e.ToHouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ToHouseID");
            entity.Property(e => e.TransferDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Child).WithMany(p => p.TransferHistories)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TransferH__Child__6AEFE058");

            entity.HasOne(d => d.FromHouse).WithMany(p => p.TransferHistoryFromHouses)
                .HasForeignKey(d => d.FromHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TransferH__FromH__6BE40491");

            entity.HasOne(d => d.ToHouse).WithMany(p => p.TransferHistoryToHouses)
                .HasForeignKey(d => d.ToHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TransferH__ToHou__6CD828CA");
        });

        modelBuilder.Entity<TransferRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transfer__3214EC0746E6BB55");

            entity.ToTable("TransferRequest");

            entity.Property(e => e.ApprovedBy).HasMaxLength(100);
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromHouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FromHouseID");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.ToHouseId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ToHouseID");

            entity.HasOne(d => d.Child).WithMany(p => p.TransferRequests)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TransferR__Child__681373AD");

            entity.HasOne(d => d.FromHouse).WithMany(p => p.TransferRequestFromHouses)
                .HasForeignKey(d => d.FromHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TransferR__FromH__690797E6");

            entity.HasOne(d => d.ToHouse).WithMany(p => p.TransferRequestToHouses)
                .HasForeignKey(d => d.ToHouseId)
                .HasConstraintName("FK__TransferR__ToHou__69FBBC1F");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC07E467A610");

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
                .HasConstraintName("FK__UserAccou__Role___489AC854");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Village__3214EC0789DE6975");

            entity.ToTable("Village");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EstablishedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__Village__UserAcc__498EEC8D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
