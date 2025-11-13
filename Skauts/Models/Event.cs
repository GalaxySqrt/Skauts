using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[Table("events")]
public partial class Event
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("match_id")]
    public int MatchId { get; set; }

    [Column("player_id")]
    public int PlayerId { get; set; }

    [Column("event_type_id")]
    public int EventTypeId { get; set; }

    [Column("event_time")]
    public DateTime EventTime { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;
    public virtual Player Player { get; set; } = null!;
}
