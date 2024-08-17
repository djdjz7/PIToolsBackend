using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PIToolsBackend.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Minigamelogin> Minigamelogins { get; set; }

    public virtual DbSet<Minigamescore> Minigamescores { get; set; }

    public virtual DbSet<Minigameuser> Minigameusers { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Packagename> Packagenames { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerTop30Ptt> PlayerTop30Ptts { get; set; }

    public virtual DbSet<Scenario> Scenarios { get; set; }

    public virtual DbSet<Scenarioalias> Scenarioaliases { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Submissionhistory> Submissionhistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=47.93.57.125;Username=Reader;Password=reader;Database=postgres;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Minigamelogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("minigamelogin_pkey");

            entity.ToTable("minigamelogin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Logindate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("logindate");
            entity.Property(e => e.Uid).HasColumnName("uid");
        });

        modelBuilder.Entity<Minigamescore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("minigamescore_pkey");

            entity.ToTable("minigamescore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Submissiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submissiondate");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Minigameuser>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("minigameuser_pkey");

            entity.ToTable("minigameuser");

            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Registrationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registrationdate");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.Opid).HasName("operator_pkey");

            entity.ToTable("operator");

            entity.Property(e => e.Opid)
                .ValueGeneratedNever()
                .HasColumnName("opid");
            entity.Property(e => e.Isadmin).HasColumnName("isadmin");
            entity.Property(e => e.Opqq)
                .HasColumnType("character varying")
                .HasColumnName("opqq");
        });

        modelBuilder.Entity<Packagename>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("packageName_pkey");

            entity.ToTable("packagename");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Packageid)
                .HasMaxLength(3)
                .HasColumnName("packageid");
            entity.Property(e => e.Packagename1)
                .HasMaxLength(32)
                .HasColumnName("packagename");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("player_pkey");

            entity.ToTable("player");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("userid");
            entity.Property(e => e.Banned).HasColumnName("banned");
            entity.Property(e => e.Completecount).HasColumnName("completecount");
            entity.Property(e => e.Fromfull).HasColumnName("fromfull");
            entity.Property(e => e.Goodcount).HasColumnName("goodcount");
            entity.Property(e => e.Greatcount).HasColumnName("greatcount");
            entity.Property(e => e.Nickname)
                .HasMaxLength(256)
                .HasColumnName("nickname");
            entity.Property(e => e.Perfectcount).HasColumnName("perfectcount");
            entity.Property(e => e.Potential).HasColumnName("potential");
            entity.Property(e => e.Qqnumber)
                .HasMaxLength(50)
                .HasColumnName("qqnumber");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Scoresum).HasColumnName("scoresum");
            entity.Property(e => e.Totalscore).HasColumnName("totalscore");
        });

        modelBuilder.Entity<PlayerTop30Ptt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("player_top30_ptt");

            entity.Property(e => e.AverageTop30Ptt).HasColumnName("average_top30_ptt");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Scenario>(entity =>
        {
            entity.HasKey(e => e.Scenarioid).HasName("scenario_pkey");

            entity.ToTable("scenario");

            entity.Property(e => e.Scenarioid).HasColumnName("scenarioid");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Constant).HasColumnName("constant");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Feature).HasColumnName("feature");
            entity.Property(e => e.Filename)
                .HasMaxLength(50)
                .HasColumnName("filename");
            entity.Property(e => e.Multiplier).HasColumnName("multiplier");
            entity.Property(e => e.Packid)
                .HasMaxLength(20)
                .HasColumnName("packid");
            entity.Property(e => e.Pinyininname)
                .HasMaxLength(50)
                .HasColumnName("pinyininname");
            entity.Property(e => e.Preload).HasColumnName("preload");
            entity.Property(e => e.Scenarioname)
                .HasMaxLength(50)
                .HasColumnName("scenarioname");
        });

        modelBuilder.Entity<Scenarioalias>(entity =>
        {
            entity.HasKey(e => e.Aliasid).HasName("ScenarioAlias_pkey");

            entity.ToTable("scenarioalias");

            entity.HasIndex(e => e.Alias, "uniqueAlias").IsUnique();

            entity.Property(e => e.Aliasid)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("aliasid");
            entity.Property(e => e.Alias)
                .HasColumnType("character varying")
                .HasColumnName("alias");
            entity.Property(e => e.Scenarioid).HasColumnName("scenarioid");

            entity.HasOne(d => d.Scenario).WithMany(p => p.Scenarioaliases)
                .HasForeignKey(d => d.Scenarioid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("scenid");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => new { e.Scenarioid, e.Userid }).HasName("score_pkey");

            entity.ToTable("score");

            entity.Property(e => e.Scenarioid).HasColumnName("scenarioid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Achievedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("achievedate");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Score1).HasColumnName("score");
            entity.Property(e => e.Submissionid).HasColumnName("submissionid");

            entity.HasOne(d => d.Scenario).WithMany(p => p.Scores)
                .HasForeignKey(d => d.Scenarioid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("score_scenarioid_fkey");

            entity.HasOne(d => d.Submission).WithMany(p => p.Scores)
                .HasForeignKey(d => d.Submissionid)
                .HasConstraintName("score_submissionid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Scores)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("score_userid_fkey");
        });

        modelBuilder.Entity<Submissionhistory>(entity =>
        {
            entity.HasKey(e => e.Submissionid).HasName("submissionhistory_pkey");

            entity.ToTable("submissionhistory");

            entity.Property(e => e.Submissionid)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("submissionid");
            entity.Property(e => e.Newpotential).HasColumnName("newpotential");
            entity.Property(e => e.Newrank).HasColumnName("newrank");
            entity.Property(e => e.Newtotalscore).HasColumnName("newtotalscore");
            entity.Property(e => e.Scenarioid).HasColumnName("scenarioid");
            entity.Property(e => e.Submitdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submitdate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Scenario).WithMany(p => p.Submissionhistories)
                .HasForeignKey(d => d.Scenarioid)
                .HasConstraintName("submissionhistory_scenarioid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Submissionhistories)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("submissionhistory_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
