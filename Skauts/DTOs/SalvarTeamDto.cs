using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um Team
    /// </summary>
    public class SalvarTeamDto
    {
        [Required(ErrorMessage = "A organização (OrgId) é obrigatória.")]
        public int OrgId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}