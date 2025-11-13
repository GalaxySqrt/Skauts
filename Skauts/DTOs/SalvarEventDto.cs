using System;
using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um Event
    /// </summary>
    public class SalvarEventDto
    {
        [Required(ErrorMessage = "O MatchId é obrigatório.")]
        public int MatchId { get; set; }

        [Required(ErrorMessage = "O PlayerId é obrigatório.")]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "O EventTypeId é obrigatório.")]
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "O horário do evento (EventTime) é obrigatório.")]
        public DateTime EventTime { get; set; }
    }
}