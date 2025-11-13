using System;
using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um Player
    /// </summary>
    public class SalvarPlayerDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "A organização (OrgId) é obrigatória.")]
        public int OrgId { get; set; }

        [Required(ErrorMessage = "A função (RoleId) é obrigatória.")]
        public int RoleId { get; set; }

        public int? Skill { get; set; }
        public int? Physique { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? ImagePath { get; set; }

        public DateOnly? BirthDate { get; set; }
    }
}