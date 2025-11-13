using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Skauts.Models;

namespace Skauts.Data.Context;

public partial class SkautsDbContext: DbContext
{
    private readonly int? _currentOrgId;
    public SkautsDbContext(DbContextOptions<SkautsDbContext> options, 
        IHttpContextAccessor httpContextAccessor): base(options)
    {
        var orgIdClaim = httpContextAccessor.HttpContext?.User
            .FindFirstValue("org_id");

        if (int.TryParse(orgIdClaim, out var orgId))
        {
            _currentOrgId = orgId;
        }
        else
        {
            _currentOrgId = null;
        }
    }

    public virtual DbSet<Championship> Championships { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayersPrize> PlayersPrizes { get; set; }

    public virtual DbSet<PrizeType> PrizeTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersOrganization> UsersOrganizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Guard clauses to apply global query filters based on _currentOrgId
        modelBuilder.Entity<Organization>()
            .HasQueryFilter(org => _currentOrgId == null || org.Id == _currentOrgId);

        modelBuilder.Entity<Player>()
            .HasQueryFilter(p => _currentOrgId == null || p.OrgId == _currentOrgId);

        modelBuilder.Entity<Team>()
            .HasQueryFilter(t => _currentOrgId == null || t.OrgId == _currentOrgId);

        modelBuilder.Entity<Match>()
            .HasQueryFilter(m => _currentOrgId == null || m.OrgId == _currentOrgId);

        modelBuilder.Entity<Championship>()
            .HasQueryFilter(c => _currentOrgId == null || c.OrgId == _currentOrgId);

        modelBuilder.Entity<Championship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__champion__3213E83FBCF333E6");

            entity.HasOne(d => d.Org).WithMany(p => p.Championships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__champions__org_i__5DCAEF64");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__events__3213E83F2B14DB10");

            entity.Property(e => e.EventTime).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__events__event_ty__66603565");

            entity.HasOne(d => d.Match).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__events__match_id__6477ECF3");

            entity.HasOne(d => d.Player).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__events__player_i__656C112C");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__event_ty__3213E83F78F538D9");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matches__3213E83F5537E76A");

            entity.HasOne(d => d.Championship).WithMany(p => p.Matches).HasConstraintName("FK__matches__champio__6383C8BA");

            entity.HasOne(d => d.Org).WithMany(p => p.Matches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matches__org_id__5CD6CB2B");

            entity.HasOne(d => d.TeamA).WithMany(p => p.MatchTeamAs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matches__team_a___619B8048");

            entity.HasOne(d => d.TeamB).WithMany(p => p.MatchTeamBs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matches__team_b___628FA481");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__organiza__3213E83F602687DF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__players__3213E83FE96945F2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.Physique).HasDefaultValue(6);
            entity.Property(e => e.Skill).HasDefaultValue(6);

            entity.HasOne(d => d.Org).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__players__org_id__5AEE82B9");

            entity.HasOne(d => d.Role).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__players__role_id__5EBF139D");
        });

        modelBuilder.Entity<PlayersPrize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__players___3213E83F63CEB117");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayersPrizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__players_p__playe__6754599E");

            entity.HasOne(d => d.PrizeType).WithMany(p => p.PlayersPrizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__players_p__prize__68487DD7");
        });

        modelBuilder.Entity<PrizeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prize_ty__3213E83FEB4156D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F0010D88D");

            entity.Property(e => e.Acronym).IsFixedLength();
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teams__3213E83F95B63308");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Org).WithMany(p => p.Teams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__teams__org_id__5BE2A6F2");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.PlayerId }).HasName("PK__team_pla__2C604C9C40110C4F");

            entity.Property(e => e.JoinDate).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamPlayers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__team_play__playe__5FB337D6");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamPlayers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__team_play__team___60A75C0F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F569A8781");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
        });

        modelBuilder.Entity<UsersOrganization>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.OrgId }).HasName("PK__users_or__86D4EF0E96E8092D");

            entity.HasOne(d => d.Org).WithMany(p => p.UsersOrganizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users_org__org_i__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.UsersOrganizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users_org__user___59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
