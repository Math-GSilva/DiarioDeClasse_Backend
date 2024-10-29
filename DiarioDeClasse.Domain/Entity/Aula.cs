using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("Aulas")]
    public class Aula : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TurmaId")]
        public int TurmaId { get; set; }

        public DateTime Data { get; set; }

        [MaxLength(500)]
        public string? Conteudo { get; set; }
    }
}
