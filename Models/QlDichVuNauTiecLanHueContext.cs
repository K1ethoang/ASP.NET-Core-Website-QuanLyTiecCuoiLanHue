using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class QlDichVuNauTiecLanHueContext : DbContext
{
    public QlDichVuNauTiecLanHueContext()
    {
    }

    public QlDichVuNauTiecLanHueContext(DbContextOptions<QlDichVuNauTiecLanHueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DetailInvoice> DetailInvoices { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishType> DishTypes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<PartyType> PartyTypes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffType> StaffTypes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Address)
                .HasColumnType("ntext")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CitizenNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CITIZEN_NUMBER");
            entity.Property(e => e.CusName)
                .HasMaxLength(100)
                .HasColumnName("CUS_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Sex).HasColumnName("SEX");
        });

        modelBuilder.Entity<DetailInvoice>(entity =>
        {
            entity.ToTable("DETAIL_INVOICE");

            entity.Property(e => e.DetailInvoiceId).HasColumnName("DETAIL_INVOICE_ID");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.DishId).HasColumnName("DISH_ID");
            entity.Property(e => e.InvoiceId).HasColumnName("INVOICE_ID");
            entity.Property(e => e.Number).HasColumnName("NUMBER");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("PRICE");

            entity.HasOne(d => d.Dish).WithMany(p => p.DetailInvoices)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETAIL_INVOICE_DISH_ID");

            entity.HasOne(d => d.Invoice).WithMany(p => p.DetailInvoices)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETAIL_INVOICE_INVOICE_ID");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("DISH");

            entity.Property(e => e.DishId).HasColumnName("DISH_ID");
            entity.Property(e => e.DishName)
                .HasMaxLength(255)
                .HasColumnName("DISH_NAME");
            entity.Property(e => e.DishTypeId).HasColumnName("DISH_TYPE_ID");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("PRICE");
            entity.Property(e => e.UnitId).HasColumnName("UNIT_ID");

            entity.HasOne(d => d.DishType).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DishTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_DISH_DISH_TYPE_ID");

            entity.HasOne(d => d.Unit).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_DISH_UNIT_ID");
        });

        modelBuilder.Entity<DishType>(entity =>
        {
            entity.ToTable("DISH_TYPE");

            entity.Property(e => e.DishTypeId).HasColumnName("DISH_TYPE_ID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("TYPE_NAME");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("INVOICE");

            entity.Property(e => e.InvoiceId).HasColumnName("INVOICE_ID");
            entity.Property(e => e.Deposit)
                .HasColumnType("money")
                .HasColumnName("DEPOSIT");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INVOICE_DATE");
            entity.Property(e => e.PartyId).HasColumnName("PARTY_ID");
            entity.Property(e => e.PaymentTime)
                .HasColumnType("datetime")
                .HasColumnName("PAYMENT_TIME");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("TOTAL");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("TOTAL_PRICE");

            entity.HasOne(d => d.Party).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVOICE_PARTY_ID");
        });

        modelBuilder.Entity<Party>(entity =>
        {
            entity.ToTable("PARTY");

            entity.Property(e => e.PartyId).HasColumnName("PARTY_ID");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("DATE");
            entity.Property(e => e.HasMenu).HasColumnName("HAS_MENU");
            entity.Property(e => e.Location)
                .HasColumnType("ntext")
                .HasColumnName("LOCATION");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("NOTE");
            entity.Property(e => e.PartyName)
                .HasColumnType("ntext")
                .HasColumnName("PARTY_NAME");
            entity.Property(e => e.PartyTypeId).HasColumnName("PARTY_TYPE_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("STATUS");
            entity.Property(e => e.Time).HasColumnName("TIME");

            entity.HasOne(d => d.Customer).WithMany(p => p.Parties)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARTY_CUSTOMER_ID");

            entity.HasOne(d => d.PartyType).WithMany(p => p.Parties)
                .HasForeignKey(d => d.PartyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARTY_PARTY_TYPE_ID");
        });

        modelBuilder.Entity<PartyType>(entity =>
        {
            entity.ToTable("PARTY_TYPE");

            entity.Property(e => e.PartyTypeId).HasColumnName("PARTY_TYPE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("STAFF");

            entity.Property(e => e.StaffId).HasColumnName("STAFF_ID");
            entity.Property(e => e.Address)
                .HasColumnType("ntext")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CitizenNumber)
                .HasMaxLength(12)
                .HasColumnName("CITIZEN_NUMBER");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Sex).HasColumnName("SEX");
            entity.Property(e => e.StaffName)
                .HasMaxLength(100)
                .HasColumnName("STAFF_NAME");
            entity.Property(e => e.StaffTypeId).HasColumnName("STAFF_TYPE_ID");
            entity.Property(e => e.UsersId)
                .HasMaxLength(450)
                .HasColumnName("USERS_ID");

            entity.HasOne(d => d.StaffType).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StaffTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STAFF_STAFF_TYPE_ID");

            entity.HasOne(d => d.Users).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK_STAFF_USERS_ID");
        });

        modelBuilder.Entity<StaffType>(entity =>
        {
            entity.ToTable("STAFF_TYPE");

            entity.Property(e => e.StaffTypeId).HasColumnName("STAFF_TYPE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("UNIT");

            entity.Property(e => e.UnitId).HasColumnName("UNIT_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.UnitName)
                .HasMaxLength(255)
                .HasColumnName("UNIT_NAME");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => new { e.StaffId, e.PartyId });

            entity.ToTable("WORK");

            entity.Property(e => e.StaffId).HasColumnName("STAFF_ID");
            entity.Property(e => e.PartyId).HasColumnName("PARTY_ID");
            entity.Property(e => e.Salary)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money")
                .HasColumnName("SALARY");

            entity.HasOne(d => d.Party).WithMany(p => p.Works)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_PARTY_ID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Works)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_STAFF_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
