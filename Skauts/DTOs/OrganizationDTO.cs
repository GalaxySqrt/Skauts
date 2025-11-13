using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Organization
/// </summary>
public class OrganizationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
}
