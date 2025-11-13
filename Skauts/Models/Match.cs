using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[Table("matches")]
public partial class Match
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("org_id")]
    public int OrgId { get; set; }

    [Column("team_a_id")]
    public Guid TeamAId { get; set; }

    [Column("team_b_id")]
    public Guid TeamBId { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("championship_id")]
    public int? ChampionshipId { get; set; }
    public virtual Championship? Championship { get; set; }
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual Organization Org { get; set; } = null!;
    public virtual Team TeamA { get; set; } = null!;
    public virtual Team TeamB { get; set; } = null!;
}
