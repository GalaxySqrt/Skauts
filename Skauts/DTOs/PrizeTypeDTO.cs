using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade PrizeType
/// </summary>
public class PrizeTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}