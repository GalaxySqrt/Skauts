using System;
using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar uma Match
    /// </summary>
    public class SalvarMatchDto
    {
        [Required(ErrorMessage = "A organização (OrgId) é obrigatória.")]
        public int OrgId { get; set; }

        [Required(ErrorMessage = "O TeamAId é obrigatório.")]
        public Guid TeamAId { get; set; }

        [Required(ErrorMessage = "O TeamBId é obrigatório.")]
        public Guid TeamBId { get; set; }

        [Required(ErrorMessage = "A data da partida é obrigatória.")]
        public DateTime Date { get; set; }

        // O ChampionshipId é opcional, pode ser um amistoso
        public int? ChampionshipId { get; set; }
    }
}