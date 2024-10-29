using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Professores")]
    public class Professor : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Disciplina { get; set; }

        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
    }
}
