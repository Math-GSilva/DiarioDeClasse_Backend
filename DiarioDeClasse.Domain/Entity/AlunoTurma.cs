using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiarioDeClasse.Domain.Interface;

namespace DiarioDeClasse.Domain.Entity
{
    [Table("AlunoTurma")]
    public class AlunoTurma : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AlunoId")]
        public int AlunoId { get; set; }

        [ForeignKey("TurmaId")]
        public int TurmaId { get; set; }

        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}
