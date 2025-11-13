using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skauts.Models;

[PrimaryKey("UserId", "OrgId")]
[Table("users_organizations")]
public partial class UsersOrganization
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Key]
    [Column("org_id")]
    public int OrgId { get; set; }

    [Column("admin")]
    public bool Admin { get; set; }

    public virtual Organization Org { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
