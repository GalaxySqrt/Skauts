using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

public class ChampionshipDto
{
    public int Id { get; set; }
    public int OrgId { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
