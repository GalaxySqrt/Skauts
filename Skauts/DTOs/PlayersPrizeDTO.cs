using System;
using System.Collections.Generic;

namespace Skauts.DTOs;

/// <summary>
/// DTO para a entidade PlayersPrize
/// </summary>
public class PlayersPrizeDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int PrizeTypeId { get; set; }
    public DateOnly ReceiveDate { get; set; }
}
