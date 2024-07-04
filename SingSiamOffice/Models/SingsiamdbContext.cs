using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SingSiamOffice.Models;

public partial class SingsiamdbContext : DbContext
{
    public SingsiamdbContext()
    {
    }

    public SingsiamdbContext(DbContextOptions<SingsiamdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlackList> BlackLists { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Collateral> Collaterals { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EventLog> EventLogs { get; set; }

    public virtual DbSet<Guarantor> Guarantors { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Periodtran> Periodtrans { get; set; }

    public virtual DbSet<Promise> Promises { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Receiptdesc> Receiptdescs { get; set; }

    public virtual DbSet<ReceiptdescCancle> ReceiptdescCancles { get; set; }

    public virtual DbSet<Receipttran> Receipttrans { get; set; }

    public virtual DbSet<ReceipttranCancle> ReceipttranCancles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RunningNo> RunningNos { get; set; }

    public virtual DbSet<SubjectCost> SubjectCosts { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=103.91.204.106;Database=singsiamdb;user id=sqlserver;password=singsiamP@ssw0rd;Trusted_Connection=false;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlackList>(entity =>
        {
            entity.HasKey(e => e.BlackId);

            entity.ToTable("black_list");

            entity.Property(e => e.BlackId).HasColumnName("black_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CNatid)
                .HasMaxLength(13)
                .HasColumnName("c_natid");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Detial)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detial");

            entity.HasOne(d => d.Branch).WithMany(p => p.BlackLists)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_black_list_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.BlackLists)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_black_list_customer");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("branch");

            entity.HasIndex(e => e.BranchCode, "IX_branch").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.BlankType)
                .HasMaxLength(50)
                .HasColumnName("blank_type");
            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .HasColumnName("branch_code");
            entity.Property(e => e.BranchMap)
                .HasMaxLength(250)
                .HasColumnName("branch_map");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .HasColumnName("branch_name");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.NoBlank)
                .HasMaxLength(50)
                .HasColumnName("no_blank");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .HasColumnName("province");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");

            entity.HasOne(d => d.ProvinceNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_branch_province");
        });

        modelBuilder.Entity<Collateral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collater__3213E83FA43031DE");

            entity.ToTable("Collateral");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cancelfee)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cancelfee");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("code");
            entity.Property(e => e.Financemax)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("financemax");
            entity.Property(e => e.Financerate)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("financerate");
            entity.Property(e => e.Loanmax)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("loanmax");
            entity.Property(e => e.Loanrate)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("loanrate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("name");
            entity.Property(e => e.Refcode)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("refcode");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Bdate)
                .HasColumnType("date")
                .HasColumnName("bdate");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CardCreate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("card_create");
            entity.Property(e => e.CardExprite)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("card_exprite");
            entity.Property(e => e.CusImg)
                .IsUnicode(false)
                .HasColumnName("cus_img");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Job)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("job");
            entity.Property(e => e.JobAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("job_address");
            entity.Property(e => e.LocationLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_link");
            entity.Property(e => e.ManPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("man_phone");
            entity.Property(e => e.ManRef)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("man_ref");
            entity.Property(e => e.ManRelation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("man_relation");
            entity.Property(e => e.NatId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nat_id");
            entity.Property(e => e.OrtherDebt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orther_debt");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Religion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("religion");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Branch).WithMany(p => p.Customers)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_customer_branch");
        });

        modelBuilder.Entity<EventLog>(entity =>
        {
            entity.HasKey(e => e.Uuid);

            entity.ToTable("event_log");

            entity.Property(e => e.Uuid)
                .ValueGeneratedNever()
                .HasColumnName("uuid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Opertation)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("opertation");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .HasColumnName("table_name");
            entity.Property(e => e.UpdateBy).HasColumnName("update_by");
        });

        modelBuilder.Entity<Guarantor>(entity =>
        {
            entity.ToTable("guarantor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("address");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.GuarantorName)
                .HasMaxLength(50)
                .HasColumnName("guarantorName");
            entity.Property(e => e.GuarantorNatId)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("guarantorNatId");
            entity.Property(e => e.GuarantorRelation)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("guarantorRelation");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Guarantors)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_guarantor_customer");

            entity.HasOne(d => d.Promise).WithMany(p => p.Guarantors)
                .HasForeignKey(d => d.PromiseId)
                .HasConstraintName("FK_guarantor_promise");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("login");

            entity.HasIndex(e => e.Username, "IX_login").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.EmNickname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_nickname");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.Img)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salt)
                .HasMaxLength(10)
                .HasColumnName("salt");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Branch).WithMany(p => p.Logins)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_login_branch");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_login_role");
        });

        modelBuilder.Entity<Periodtran>(entity =>
        {
            entity.ToTable("periodtran");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Capital)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("capital");
            entity.Property(e => e.Cappaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cappaid");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientno");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deposit)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Inspaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("inspaid");
            entity.Property(e => e.Insurance)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("insurance");
            entity.Property(e => e.Interest)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("interest");
            entity.Property(e => e.Intpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intpaid");
            entity.Property(e => e.Ispaid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ispaid");
            entity.Property(e => e.Loanminus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanminus");
            entity.Property(e => e.Loanplus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanplus");
            entity.Property(e => e.Paidamount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("paidamount");
            entity.Property(e => e.Paidremain)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("paidremain");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("((0))")
                .HasColumnName("period");
            entity.Property(e => e.Periods)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periods");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");
            entity.Property(e => e.Ptype)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ptype");
            entity.Property(e => e.Service)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("service");
            entity.Property(e => e.Specialtaxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("specialtaxpromise");
            entity.Property(e => e.Srvpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("srvpaid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.Taxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("taxpromise");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdate");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdateformat");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("usercode");

            entity.HasOne(d => d.Branch).WithMany(p => p.Periodtrans)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_periodtran_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Periodtrans)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_periodtran_customer");

            entity.HasOne(d => d.Promise).WithMany(p => p.Periodtrans)
                .HasForeignKey(d => d.PromiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_periodtran_promise");
        });

        modelBuilder.Entity<Promise>(entity =>
        {
            entity.ToTable("promise", tb => tb.HasTrigger("UpdateRunningNo"));

            entity.HasIndex(e => e.Promiseno, "UQ_promiseno").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Cancelno)
                .HasDefaultValueSql("((0))")
                .HasColumnName("cancelno");
            entity.Property(e => e.Capital)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("capital");
            entity.Property(e => e.Chargeamt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("chargeamt");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientno");
            entity.Property(e => e.Closecase)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("closecase");
            entity.Property(e => e.Closedocno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("closedocno");
            entity.Property(e => e.Closeresult)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("closeresult");
            entity.Property(e => e.Coldata1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata1");
            entity.Property(e => e.Coldata2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata2");
            entity.Property(e => e.Coldata3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata3");
            entity.Property(e => e.Coldata4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata4");
            entity.Property(e => e.Coldata5)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata5");
            entity.Property(e => e.Coldata6)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata6");
            entity.Property(e => e.Coldata7)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata7");
            entity.Property(e => e.Coldata8)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata8");
            entity.Property(e => e.Coldata9)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("coldata9");
            entity.Property(e => e.Colname1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname1");
            entity.Property(e => e.Colname2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname2");
            entity.Property(e => e.Colname3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname3");
            entity.Property(e => e.Colname4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname4");
            entity.Property(e => e.Colname5)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname5");
            entity.Property(e => e.Colname6)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname6");
            entity.Property(e => e.Colname7)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname7");
            entity.Property(e => e.Colname8)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname8");
            entity.Property(e => e.Colname9)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("colname9");
            entity.Property(e => e.Compromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("compromise");
            entity.Property(e => e.ContractType).HasColumnName("contract_type");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Dateclose)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("dateclose");
            entity.Property(e => e.Dateclosecal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("dateclosecal");
            entity.Property(e => e.Dateclosecalformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("dateclosecalformat");
            entity.Property(e => e.Datecloseformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("datecloseformat");
            entity.Property(e => e.Datewarn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("datewarn");
            entity.Property(e => e.Datewarnformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("datewarnformat");
            entity.Property(e => e.Daypaid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("daypaid");
            entity.Property(e => e.Deposit)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Downamount)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("downamount");
            entity.Property(e => e.Firstdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("firstdate");
            entity.Property(e => e.Guarantor).HasColumnName("guarantor");
            entity.Property(e => e.Insurance)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("insurance");
            entity.Property(e => e.Insurance1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("insurance1");
            entity.Property(e => e.Insurance1relation)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("insurance1relation");
            entity.Property(e => e.Insurance2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("insurance2");
            entity.Property(e => e.Insurance2relation)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("insurance2relation");
            entity.Property(e => e.Intrate)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("intrate");
            entity.Property(e => e.JsonPrddesc).HasColumnName("json_prddesc");
            entity.Property(e => e.Latepc)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("latepc");
            entity.Property(e => e.Nampa)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("nampa");
            entity.Property(e => e.Periods)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periods");
            entity.Property(e => e.Person1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("person1");
            entity.Property(e => e.Person2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("person2");
            entity.Property(e => e.Person3)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("person3");
            entity.Property(e => e.Person4)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("person4");
            entity.Property(e => e.Person5)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("person5");
            entity.Property(e => e.Prddesc)
                .HasMaxLength(900)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("prddesc");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Promiseno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("promiseno");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Ptype)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ptype");
            entity.Property(e => e.Refcode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("refcode");
            entity.Property(e => e.Service)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("service");
            entity.Property(e => e.Specialtaxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("specialtaxpromise");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.Stockcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("stockcode");
            entity.Property(e => e.Sumcharge1)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sumcharge1");
            entity.Property(e => e.Sumcharge2)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sumcharge2");
            entity.Property(e => e.Sumstatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("sumstatus");
            entity.Property(e => e.Taxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("taxpromise");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdate");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdateformat");
            entity.Property(e => e.Tdatetime)
                .HasDefaultValueSql("('1900-01-01 00:00:00')")
                .HasColumnType("datetime")
                .HasColumnName("tdatetime");
            entity.Property(e => e.Totaldown)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totaldown");
            entity.Property(e => e.UploadImg).HasColumnName("upload_img");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("usercode");
            entity.Property(e => e.Warndesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("warndesc");

            entity.HasOne(d => d.Branch).WithMany(p => p.Promises)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_promise_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Promises)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_promise_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Promises)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_promise_Collateral");

            entity.HasOne(d => d.Province).WithMany(p => p.Promises)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_promise_province");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("province");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("is_active");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("province_name");
            entity.Property(e => e.ProvinceShortEn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("province_short_en");
            entity.Property(e => e.ProvinceShortTh)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("province_short_th");
        });

        modelBuilder.Entity<Receiptdesc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__receiptd__3213E83F899C633A");

            entity.ToTable("receiptdesc");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Cappaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cappaid");
            entity.Property(e => e.Chargeamt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("chargeamt");
            entity.Property(e => e.Clientbranch)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientbranch");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientno");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deposit)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Inspaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("inspaid");
            entity.Property(e => e.Intpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intpaid");
            entity.Property(e => e.Lateamt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("lateamt");
            entity.Property(e => e.Loanminus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanminus");
            entity.Property(e => e.Loanplus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanplus");
            entity.Property(e => e.Newint)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("newint");
            entity.Property(e => e.Oldint)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("oldint");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Periodchg)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periodchg");
            entity.Property(e => e.Perioddate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("perioddate");
            entity.Property(e => e.PeriodtranId).HasColumnName("periodtran_id");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");
            entity.Property(e => e.Receiptno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receiptno");
            entity.Property(e => e.ReceipttranId).HasColumnName("receipttran_id");
            entity.Property(e => e.Srvpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("srvpaid");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdate");
            entity.Property(e => e.Tdatecal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecal");
            entity.Property(e => e.Tdatecalformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecalformat");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdateformat");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usercode");

            entity.HasOne(d => d.Branch).WithMany(p => p.Receiptdescs)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Receiptdescs)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_customer");

            entity.HasOne(d => d.Periodtran).WithMany(p => p.Receiptdescs)
                .HasForeignKey(d => d.PeriodtranId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_periodtran");

            entity.HasOne(d => d.Promise).WithMany(p => p.Receiptdescs)
                .HasForeignKey(d => d.PromiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_promise");

            entity.HasOne(d => d.Receipttran).WithMany(p => p.Receiptdescs)
                .HasForeignKey(d => d.ReceipttranId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_receipttran");
        });

        modelBuilder.Entity<ReceiptdescCancle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__receiptd__3213E83F959944AD");

            entity.ToTable("receiptdesc_cancle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Cappaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cappaid");
            entity.Property(e => e.Chargeamt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("chargeamt");
            entity.Property(e => e.Clientbranch)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("clientbranch");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("clientno");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deposit)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Inspaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("inspaid");
            entity.Property(e => e.Intpaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intpaid");
            entity.Property(e => e.Lateamt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("lateamt");
            entity.Property(e => e.Loanminus)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanminus");
            entity.Property(e => e.Loanplus)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanplus");
            entity.Property(e => e.Newint)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("newint");
            entity.Property(e => e.Oldint)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("oldint");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Periodchg).HasColumnName("periodchg");
            entity.Property(e => e.Perioddate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("perioddate");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");
            entity.Property(e => e.Receiptno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receiptno");
            entity.Property(e => e.Srvpaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("srvpaid");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdate");
            entity.Property(e => e.Tdatecal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecal");
            entity.Property(e => e.Tdatecalformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecalformat");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdateformat");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usercode");

            entity.HasOne(d => d.Branch).WithMany(p => p.ReceiptdescCancles)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_cancle_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReceiptdescCancles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_cancle_customer");

            entity.HasOne(d => d.Promise).WithMany(p => p.ReceiptdescCancles)
                .HasForeignKey(d => d.PromiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receiptdesc_cancle_promise");
        });

        modelBuilder.Entity<Receipttran>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__receiptt__3213E83FB3733695");

            entity.ToTable("receipttran", tb => tb.HasTrigger("UpdateRunningNo_Receipt"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.Arbalance)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("arbalance");
            entity.Property(e => e.Arperiod)
                .HasDefaultValueSql("((0))")
                .HasColumnName("arperiod");
            entity.Property(e => e.Arremain)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("arremain");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Cappaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cappaid");
            entity.Property(e => e.Capremain)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("capremain");
            entity.Property(e => e.Cashpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("cashpaid");
            entity.Property(e => e.Charge1amt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("charge1amt");
            entity.Property(e => e.Charge2amt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("charge2amt");
            entity.Property(e => e.Clientbranch)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientbranch");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("clientno");
            entity.Property(e => e.Closecase)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("closecase");
            entity.Property(e => e.Closefee)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("closefee");
            entity.Property(e => e.Currentperiod)
                .HasDefaultValueSql("((0))")
                .HasColumnName("currentperiod");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deposit)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Discount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("discount");
            entity.Property(e => e.Inspaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("inspaid");
            entity.Property(e => e.Intdiscamt)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intdiscamt");
            entity.Property(e => e.Intpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intpaid");
            entity.Property(e => e.Intplus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intplus");
            entity.Property(e => e.Intremain)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intremain");
            entity.Property(e => e.Loanminus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanminus");
            entity.Property(e => e.Loanplus)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanplus");
            entity.Property(e => e.Netamount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("netamount");
            entity.Property(e => e.Newint)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("newint");
            entity.Property(e => e.Oldint)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("oldint");
            entity.Property(e => e.Otherpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("otherpaid");
            entity.Property(e => e.PaidBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("paid_by");
            entity.Property(e => e.Periodchg)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periodchg");
            entity.Property(e => e.Periodremain)
                .HasDefaultValueSql("((0))")
                .HasColumnName("periodremain");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");
            entity.Property(e => e.Ptype)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ptype");
            entity.Property(e => e.Receiptdesc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("receiptdesc");
            entity.Property(e => e.Receiptno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("receiptno");
            entity.Property(e => e.RemainingPrincipal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Resultamount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("resultamount");
            entity.Property(e => e.Specialtaxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("specialtaxpromise");
            entity.Property(e => e.Srvpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("srvpaid");
            entity.Property(e => e.Taxpromise)
                .HasDefaultValueSql("((0))")
                .HasColumnName("taxpromise");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdate");
            entity.Property(e => e.Tdatecal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdatecal");
            entity.Property(e => e.Tdatecalformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdatecalformat");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("tdateformat");
            entity.Property(e => e.Transferdate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("transferdate");
            entity.Property(e => e.Transferpaid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("transferpaid");
            entity.Property(e => e.UploadImg).HasColumnName("upload_img");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("usercode");

            entity.HasOne(d => d.Branch).WithMany(p => p.Receipttrans)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Receipttrans)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_customer");

            entity.HasOne(d => d.Promise).WithMany(p => p.Receipttrans)
                .HasForeignKey(d => d.PromiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_promise");
        });

        modelBuilder.Entity<ReceipttranCancle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__receiptt__3213E83F9090D904");

            entity.ToTable("receipttran_cancle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.Arbalance)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("arbalance");
            entity.Property(e => e.Arperiod).HasColumnName("arperiod");
            entity.Property(e => e.Arremain)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("arremain");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Cappaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("cappaid");
            entity.Property(e => e.Capremain)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("capremain");
            entity.Property(e => e.Cashpaid).HasColumnName("cashpaid");
            entity.Property(e => e.Charge1amt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("charge1amt");
            entity.Property(e => e.Charge2amt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("charge2amt");
            entity.Property(e => e.Clientbranch)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("clientbranch");
            entity.Property(e => e.Clientno)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("clientno");
            entity.Property(e => e.Closecase)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("closecase");
            entity.Property(e => e.Closefee)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("closefee");
            entity.Property(e => e.Currentperiod).HasColumnName("currentperiod");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deposit)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("deposit");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("discount");
            entity.Property(e => e.Inspaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("inspaid");
            entity.Property(e => e.Intdiscamt)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intdiscamt");
            entity.Property(e => e.Intpaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intpaid");
            entity.Property(e => e.Intplus)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intplus");
            entity.Property(e => e.Intremain)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("intremain");
            entity.Property(e => e.Loanminus)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanminus");
            entity.Property(e => e.Loanplus)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("loanplus");
            entity.Property(e => e.Netamount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("netamount");
            entity.Property(e => e.Newint)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("newint");
            entity.Property(e => e.Oldint)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("oldint");
            entity.Property(e => e.Otherpaid).HasColumnName("otherpaid");
            entity.Property(e => e.Periodchg).HasColumnName("periodchg");
            entity.Property(e => e.Periodremain).HasColumnName("periodremain");
            entity.Property(e => e.PromiseId).HasColumnName("promise_id");
            entity.Property(e => e.Ptype).HasColumnName("ptype");
            entity.Property(e => e.Receiptdesc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("receiptdesc");
            entity.Property(e => e.Receiptno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receiptno");
            entity.Property(e => e.Resultamount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("resultamount");
            entity.Property(e => e.Specialtaxpromise).HasColumnName("specialtaxpromise");
            entity.Property(e => e.Srvpaid)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("srvpaid");
            entity.Property(e => e.Taxpromise).HasColumnName("taxpromise");
            entity.Property(e => e.Tdate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdate");
            entity.Property(e => e.Tdatecal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecal");
            entity.Property(e => e.Tdatecalformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdatecalformat");
            entity.Property(e => e.Tdateformat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tdateformat");
            entity.Property(e => e.Transferdate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("transferdate");
            entity.Property(e => e.Transferpaid).HasColumnName("transferpaid");
            entity.Property(e => e.Usercode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usercode");

            entity.HasOne(d => d.Branch).WithMany(p => p.ReceipttranCancles)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_cancle_branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReceipttranCancles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_cancle_customer");

            entity.HasOne(d => d.Promise).WithMany(p => p.ReceipttranCancles)
                .HasForeignKey(d => d.PromiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_receipttran_cancle_promise");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83FF8DD49E5");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<RunningNo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("running_no");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CurrentNo)
                .HasMaxLength(50)
                .HasColumnName("current_no");
            entity.Property(e => e.NextNo)
                .HasMaxLength(50)
                .HasColumnName("next_no");
            entity.Property(e => e.Type)
                .HasMaxLength(250)
                .HasColumnName("type");
        });

        modelBuilder.Entity<SubjectCost>(entity =>
        {
            entity.HasKey(e => e.SubjectId);

            entity.ToTable("Subject_cost");

            entity.Property(e => e.SubjectId).HasColumnName("Subject_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Subject_code");
            entity.Property(e => e.SubjectDeital)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Subject_deital");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Subject_name");
            entity.Property(e => e.SubjectType).HasColumnName("Subject_type");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("Transaction_history");

            entity.Property(e => e.TransactionId).HasColumnName("Transaction_id");
            entity.Property(e => e.BranchId).HasColumnName("Branch_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_at");
            entity.Property(e => e.Detial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LoginId).HasColumnName("login_id");
            entity.Property(e => e.PaymentMethod).HasColumnName("Payment_Method");
            entity.Property(e => e.SlipUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Slip_url");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_id");
            entity.Property(e => e.TransectionRef)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transection_ref");
            entity.Property(e => e.TransectionRemark)
                .HasColumnType("text")
                .HasColumnName("Transection_remark");

            entity.HasOne(d => d.Branch).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_history_branch");

            entity.HasOne(d => d.Login).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_history_login");

            entity.HasOne(d => d.Subject).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_history_Subject_cost");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
