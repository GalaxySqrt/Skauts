using System.ComponentModel.DataAnnotations;

namespace Skauts.DTOs
{
    /// <summary>
    /// DTO para criar ou atualizar um PrizeType
    /// </summary>
    public class SalvarPrizeTypeDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}