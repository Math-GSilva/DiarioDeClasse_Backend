using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Usuarios")]
    public class User : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Tipo { get; set; }

    }
}
