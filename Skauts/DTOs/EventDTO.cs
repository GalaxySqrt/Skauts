using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Event
/// </summary>
public class EventDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public int EventTypeId { get; set; }
    public DateTime EventTime { get; set; }
}
