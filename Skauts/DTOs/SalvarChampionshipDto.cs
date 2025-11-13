using System;
using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um Championship
    /// </summary>
    public class SalvarChampionshipDto
    {
        [Required(ErrorMessage = "A organização (OrgId) é obrigatória.")]
        public int OrgId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}