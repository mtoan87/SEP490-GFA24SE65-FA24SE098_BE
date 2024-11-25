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

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

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

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemWallet> SystemWallets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(local); uid=sa; pwd=12345; database=SOSChildrenVillageDB; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicReport>(entity =>
        {
            entity.ToTable("AcademicReport");

            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GPA");

            entity.HasOne(d => d.Child).WithMany(p => p.AcademicReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_AcademicReport_Child1");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC0737DD5FE2");

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
                .HasConstraintName("FK__Booking__House_i__1BC821DD");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Booking__UserAcc__1CBC4616");
        });

        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingS__3214EC07A7813704");

            entity.ToTable("BookingSlot");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnName("End_time");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SlotTime).HasColumnName("Slot_time");
            entity.Property(e => e.StartTime).HasColumnName("Start_time");
            entity.Property(e => e.Status).HasMaxLength(100);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carts__3214EC0713CC3D79");

            entity.HasIndex(e => e.UserAccountId, "UQ__Carts__DA6C709B9E4F3891").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UserAccount).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.UserAccountId)
                .HasConstraintName("FK__Carts__UserAccou__00200768");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07EB1211BF");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItems__CartI__02FC7413");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CartItems__Produ__03F0984C");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0723BE3635");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC07FE587723");

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
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Child__Facilitie__3E1D39E1");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Child__FoodStuff__3C34F16F");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Child__HealthWal__3B40CD36");

            entity.HasOne(d => d.House).WithMany(p => p.Children)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Child__House_Id__1DB06A4F");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Child__Necessiti__3D2915A8");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Children)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Child__SystemWal__3F115E1A");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donation__3214EC0757008FFE");

            entity.ToTable("Donation");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Child_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DonationType)
                .HasMaxLength(200)
                .HasColumnName("Donation_Type");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.FacilitiesWalletId).HasColumnName("FacilitiesWallet_Id");
            entity.Property(e => e.FoodStuffWalletId).HasColumnName("FoodStuffWallet_Id");
            entity.Property(e => e.HealthWalletId).HasColumnName("HealthWallet_Id");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NecessitiesWalletId).HasColumnName("NecessitiesWallet_Id");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.SystemWalletId).HasColumnName("SystemWallet_Id");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.Child).WithMany(p => p.Donations)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Donation__Child___1F98B2C1");

            entity.HasOne(d => d.Event).WithMany(p => p.Donations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Donation__Event___208CD6FA");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Donation__Facili__5224328E");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Donation__FoodSt__503BEA1C");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Donation__Health__4F47C5E3");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Donation__Necess__51300E55");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Donations)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Donation__System__531856C7");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Donation__UserAc__1EA48E88");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentAmount).HasColumnType("decimal(18, 2)");
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
            entity.Property(e => e.VillageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Village_Id");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Event__Facilitie__42E1EEFE");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Event__FoodStuff__40F9A68C");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Event__HealthWal__40058253");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Event__Necessiti__41EDCAC5");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Events)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Event__SystemWal__43D61337");

            entity.HasOne(d => d.Village).WithMany(p => p.Events)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK_Event_Village");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC07EC3EA5DA");

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
                .HasConstraintName("FK__Expense__Facilit__489AC854");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Expense__FoodStu__46B27FE2");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Expense__HealthW__45BE5BA9");

            entity.HasOne(d => d.House).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Expense__House_I__22751F6C");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Expense__Necessi__47A6A41B");
        });

        modelBuilder.Entity<FacilitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3214EC070A23B2DC");

            entity.ToTable("FacilitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FacilitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Facilitie__UserA__2EDAF651");
        });

        modelBuilder.Entity<FoodStuffWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodStuf__3214EC07D71DB36A");

            entity.ToTable("FoodStuffWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.FoodStuffWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__FoodStuff__UserA__30C33EC3");
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
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(100)
                .HasColumnName("Medical_History");
            entity.Property(e => e.NutritionalStatus)
                .HasMaxLength(100)
                .HasColumnName("Nutritional_Status");
            entity.Property(e => e.VaccinationStatus)
                .HasMaxLength(100)
                .HasColumnName("Vaccination_Status");
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Child).WithMany(p => p.HealthReports)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_HealthReport_Child");
        });

        modelBuilder.Entity<HealthWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthWa__3214EC07D6DC1F1A");

            entity.ToTable("HealthWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.HealthWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__HealthWal__UserA__2FCF1A8A");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__House__3214EC0770BBCA30");

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
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__House__UserAccou__245D67DE");

            entity.HasOne(d => d.Village).WithMany(p => p.Houses)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK__House__Village_I__25518C17");
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
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
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

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Image__Product_I__74AE54BC");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Images)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK_Image_UserAccount");

            entity.HasOne(d => d.Village).WithMany(p => p.Images)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("FK_Image_Village");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC0714D52C0C");

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
                .HasConstraintName("FK__Income__Donation__2B0A656D");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Income__Faciliti__4D5F7D71");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Income__FoodStuf__4B7734FF");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Income__HealthWa__4A8310C6");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Income__Necessit__4C6B5938");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Income__SystemWa__498EEC8D");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Income__UserAcco__2BFE89A6");
        });

        modelBuilder.Entity<NecessitiesWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Necessit__3214EC0734599955");

            entity.ToTable("NecessitiesWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.NecessitiesWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Necessiti__UserA__31B762FC");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC075A56A05F");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Orders__PaymentI__787EE5A0");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Orders__UserAcco__778AC167");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07D19307CB");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__7B5B524B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__7C4F7684");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07ACBA7F99");

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
                .HasConstraintName("FK__Payment__Donatio__2CF2ADDF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07BEC3E76B");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__71D1E811");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0753B6C3DD");

            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK__Reviews__OrderDe__06CD04F7");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Reviews__UserAcc__07C12930");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0773F4D0D8");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<SystemWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemWa__3214EC07E13FF3ED");

            entity.ToTable("SystemWallet");

            entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserAccount_Id");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.SystemWallets)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__SystemWal__UserA__2DE6D218");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07CAD32173");

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
                .HasConstraintName("FK__Transacti__Donat__32AB8735");

            entity.HasOne(d => d.FacilitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FacilitiesWalletId)
                .HasConstraintName("FK__Transacti__Facil__37703C52");

            entity.HasOne(d => d.FoodStuffWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FoodStuffWalletId)
                .HasConstraintName("FK__Transacti__FoodS__3587F3E0");

            entity.HasOne(d => d.HealthWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.HealthWalletId)
                .HasConstraintName("FK__Transacti__Healt__3493CFA7");

            entity.HasOne(d => d.Income).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__Transacti__Incom__339FAB6E");

            entity.HasOne(d => d.NecessitiesWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.NecessitiesWalletId)
                .HasConstraintName("FK__Transacti__Neces__367C1819");

            entity.HasOne(d => d.SystemWallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SystemWalletId)
                .HasConstraintName("FK__Transacti__Syste__3864608B");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__Transacti__UserA__2180FB33");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC07B2D88078");

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
                .HasConstraintName("FK__UserAccou__Role___395884C4");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Village__3214EC0706C154B8");

            entity.ToTable("Village");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");
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
                .HasConstraintName("FK__Village__UserAcc__3A4CA8FD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
