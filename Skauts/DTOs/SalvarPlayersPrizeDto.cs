using System;
using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um PlayersPrize
    /// </summary>
    public class SalvarPlayersPrizeDto
    {
        [Required(ErrorMessage = "O PlayerId é obrigatório.")]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "O PrizeTypeId é obrigatório.")]
        public int PrizeTypeId { get; set; }

        [Required(ErrorMessage = "A data de recebimento é obrigatória.")]
        public DateOnly ReceiveDate { get; set; }
    }
}