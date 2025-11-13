using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[Table("championships")]
public partial class Championship
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("org_id")]
    public int OrgId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }
    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    public virtual Organization Org { get; set; } = null!;
}
