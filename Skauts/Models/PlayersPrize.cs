using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[Table("players_prizes")]
public partial class PlayersPrize
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("player_id")]
    public int PlayerId { get; set; }

    [Column("prize_type_id")]
    public int PrizeTypeId { get; set; }

    [Column("receive_date")]
    public DateOnly ReceiveDate { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual PrizeType PrizeType { get; set; } = null!;
}
