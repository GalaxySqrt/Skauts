using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade EventType
/// </summary>
public class EventTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
