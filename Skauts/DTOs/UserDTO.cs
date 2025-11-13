using System;
using System.Collections.Generic;
namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade User
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}