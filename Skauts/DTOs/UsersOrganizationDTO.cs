using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade UsersOrganization (relação N:N)
/// </summary>
public class UsersOrganizationDto
{
    public int UserId { get; set; }
    public int OrgId { get; set; }
    public bool Admin { get; set; }
}