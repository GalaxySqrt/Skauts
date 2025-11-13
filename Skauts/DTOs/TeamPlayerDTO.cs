using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade TeamPlayer (relação N:N)
/// </summary>
public class TeamPlayerDto
{
    public Guid TeamId { get; set; }
    public int PlayerId { get; set; }
    public DateOnly JoinDate { get; set; }
}
