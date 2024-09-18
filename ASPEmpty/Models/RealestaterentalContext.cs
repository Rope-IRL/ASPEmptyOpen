using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPEmpty.Models;

public partial class RealestaterentalContext : DbContext
{
    public RealestaterentalContext()
    {
    }

    public RealestaterentalContext(DbContextOptions<RealestaterentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flat> Flats { get; set; }

    public virtual DbSet<FlatsContract> FlatsContracts { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelsRoom> HotelsRooms { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<HousesContract> HousesContracts { get; set; }

    public virtual DbSet<LandLord> LandLords { get; set; }

    public virtual DbSet<LandLordsAdditionalInfo> LandLordsAdditionalInfos { get; set; }

    public virtual DbSet<Lessee> Lessees { get; set; }

    public virtual DbSet<LesseesAdditionalInfo> LesseesAdditionalInfos { get; set; }

    public virtual DbSet<RoomsContract> RoomsContracts { get; set; }

    public virtual DbSet<SelectallLandlordInfoView> SelectallLandlordInfoViews { get; set; }

    public virtual DbSet<SelectallLesseeInfoView> SelectallLesseeInfoViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConfigurationBuilder builder = new();

        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot configuration = builder.AddUserSecrets<Program>().Build();

        string connectionString = "";
        connectionString = configuration.GetConnectionString("SQLConnection");

        /// Задание опций подключения
        _ = optionsBuilder
            .UseSqlServer(connectionString)
            .Options;
        optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flat>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PK__Flats__C1D1314A037B5BCD");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.CostPerDay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Header).HasMaxLength(100);
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.Ll).WithMany(p => p.Flats)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__Flats__LLid__48CFD27E");
        });

        modelBuilder.Entity<FlatsContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlatsCon__3214EC07DD81DD64");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.FlatsContracts)
                .HasForeignKey(d => d.Fid)
                .HasConstraintName("FK__FlatsContra__Fid__4D94879B");

            entity.HasOne(d => d.LidNavigation).WithMany(p => p.FlatsContracts)
                .HasForeignKey(d => d.Lid)
                .HasConstraintName("FK__FlatsContra__Lid__4BAC3F29");

            entity.HasOne(d => d.Ll).WithMany(p => p.FlatsContracts)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__FlatsContr__LLid__4CA06362");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Hid).HasName("PK__Hotels__C750193FFB0EDCD9");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Header).HasMaxLength(100);
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.Ll).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__Hotels__LLid__5070F446");
        });

        modelBuilder.Entity<HotelsRoom>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__HotelsRo__CAF055CAC52A9CD5");

            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CostPerDay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Header).HasMaxLength(100);

            entity.HasOne(d => d.HidNavigation).WithMany(p => p.HotelsRooms)
                .HasForeignKey(d => d.Hid)
                .HasConstraintName("FK__HotelsRooms__Hid__534D60F1");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__Houses__C57059389CC87CA9");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.CostPerDay).HasColumnType("money");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Header).HasMaxLength(100);
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.Ll).WithMany(p => p.Houses)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__Houses__LLid__412EB0B6");
        });

        modelBuilder.Entity<HousesContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HousesCo__3214EC078D3C1490");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.LidNavigation).WithMany(p => p.HousesContracts)
                .HasForeignKey(d => d.Lid)
                .HasConstraintName("FK__HousesContr__Lid__440B1D61");

            entity.HasOne(d => d.Ll).WithMany(p => p.HousesContracts)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__HousesCont__LLid__44FF419A");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.HousesContracts)
                .HasForeignKey(d => d.Pid)
                .HasConstraintName("FK__HousesContr__Pid__45F365D3");
        });

        modelBuilder.Entity<LandLord>(entity =>
        {
            entity.HasKey(e => e.Llid).HasName("PK__LandLord__77BE170E87930B01");

            entity.Property(e => e.Llid).HasColumnName("LLid");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<LandLordsAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LandLord__3214EC070805EFB1");

            entity.ToTable("LandLordsAdditionalInfo");

            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Llid).HasColumnName("LLid");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PassportId).HasMaxLength(70);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Telephone).HasMaxLength(20);

            entity.HasOne(d => d.Ll).WithMany(p => p.LandLordsAdditionalInfos)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__LandLordsA__LLid__3E52440B");
        });

        modelBuilder.Entity<Lessee>(entity =>
        {
            entity.HasKey(e => e.Lid).HasName("PK__Lessees__C6505B39CFD1C6CC");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<LesseesAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LesseesA__3214EC072BB0F616");

            entity.ToTable("LesseesAdditionalInfo");

            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PassportId).HasMaxLength(70);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Telephone).HasMaxLength(20);

            entity.HasOne(d => d.LidNavigation).WithMany(p => p.LesseesAdditionalInfos)
                .HasForeignKey(d => d.Lid)
                .HasConstraintName("FK__LesseesAddi__Lid__398D8EEE");
        });

        modelBuilder.Entity<RoomsContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomsCon__3214EC071DBA45A5");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Llid).HasColumnName("LLid");

            entity.HasOne(d => d.LidNavigation).WithMany(p => p.RoomsContracts)
                .HasForeignKey(d => d.Lid)
                .HasConstraintName("FK__RoomsContra__Lid__5629CD9C");

            entity.HasOne(d => d.Ll).WithMany(p => p.RoomsContracts)
                .HasForeignKey(d => d.Llid)
                .HasConstraintName("FK__RoomsContr__LLid__571DF1D5");

            entity.HasOne(d => d.RidNavigation).WithMany(p => p.RoomsContracts)
                .HasForeignKey(d => d.Rid)
                .HasConstraintName("FK__RoomsContra__Rid__5812160E");
        });

        modelBuilder.Entity<SelectallLandlordInfoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("selectall_landlord_info_view");

            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PassportId).HasMaxLength(70);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<SelectallLesseeInfoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("selectall_lessee_info_view");

            entity.Property(e => e.AvgMark).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PassportId).HasMaxLength(70);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
