using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Team
/// </summary>
public class TeamDto
{
    public Guid Id { get; set; }
    public int OrgId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
