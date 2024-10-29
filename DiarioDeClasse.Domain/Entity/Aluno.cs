using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Alunos")]
    public class Aluno : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Matricula { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
