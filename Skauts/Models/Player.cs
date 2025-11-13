using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[Table("players")]
public partial class Player
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("org_id")]
    public int OrgId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("skill")]
    public int? Skill { get; set; }

    [Column("physique")]
    public int? Physique { get; set; }

    [Column("phone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("image_path")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [Column("birth_date")]
    public DateOnly? BirthDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Organization Org { get; set; } = null!;

    public virtual ICollection<PlayersPrize> PlayersPrizes { get; set; } = new List<PlayersPrize>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();
}
