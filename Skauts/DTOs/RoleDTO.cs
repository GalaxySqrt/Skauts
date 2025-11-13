using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade Role
/// </summary>
public class RoleDto
{
    public int Id { get; set; }
    public string Acronym { get; set; } = null!;
    public string Name { get; set; } = null!;
}
