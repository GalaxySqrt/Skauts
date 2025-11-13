using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um EventType
    /// </summary>
    public class SalvarEventTypeDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}