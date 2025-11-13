using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Match
/// </summary>
public class MatchDto
{
    public int Id { get; set; }
    public int OrgId { get; set; }
    public Guid TeamAId { get; set; }
    public Guid TeamBId { get; set; }
    public DateTime Date { get; set; }
    public int? ChampionshipId { get; set; }
}
