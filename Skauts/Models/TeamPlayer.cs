using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[PrimaryKey("TeamId", "PlayerId")]
[Table("team_players")]
public partial class TeamPlayer
{
    [Key]
    [Column("team_id")]
    public Guid TeamId { get; set; }

    [Key]
    [Column("player_id")]
    public int PlayerId { get; set; }

    [Column("join_date")]
    public DateOnly JoinDate { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
