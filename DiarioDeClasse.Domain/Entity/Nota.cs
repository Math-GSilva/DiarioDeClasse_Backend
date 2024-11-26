using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Notas")]
    public class Nota : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AlunoId")]
        public int AlunoId { get; set; }

        [ForeignKey("TurmaId")]
        public int TurmaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Avaliacao { get; set; }

        [Required]
        public decimal ValorNota { get; set; }

        [MaxLength(500)]
        public string? Observacao { get; set; }
    }
}
