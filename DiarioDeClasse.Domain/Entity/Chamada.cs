using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Chamadas")]
    public class Chamada : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AulaId")]
        public int AulaId { get; set; }

        [ForeignKey("AlunoId")]
        public int AlunoId { get; set; }

        [Required]
        public string StatusPresenca { get; set; }

        [MaxLength(500)]
        public string? Observacao { get; set; }
    }
}
