using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar uma Role
    /// </summary>
    public class SalvarRoleDto
    {
        [Required(ErrorMessage = "A sigla (Acronym) é obrigatória.")]
        [StringLength(5)]
        public string Acronym { get; set; } = null!;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}