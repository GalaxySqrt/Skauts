using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Player
/// </summary>
public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int OrgId { get; set; }
    public int RoleId { get; set; }
    public int? Skill { get; set; }
    public int? Physique { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ImagePath { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
