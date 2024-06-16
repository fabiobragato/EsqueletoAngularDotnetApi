using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegistroDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Senha { get; set; }

        [Required]
        [MinLength(14)]
        [MaxLength(15)]
        public string Celular { get; set; }

        public bool EhAdmin { get; set; } = false;
    }
}