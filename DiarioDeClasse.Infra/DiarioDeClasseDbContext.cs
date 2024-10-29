using Microsoft.EntityFrameworkCore;
using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Infra
{
    public class DiarioDeClasseContext : DbContext
    {
        public DiarioDeClasseContext(DbContextOptions<DiarioDeClasseContext> options) : base(options)
        {
        }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Chamada> Chamadas { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nome).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Tipo).IsRequired();
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("Professores");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Disciplina).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("Alunos");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Nome).IsRequired().HasMaxLength(150);
                entity.Property(a => a.Matricula).IsRequired().HasMaxLength(50);
                entity.Property(a => a.DataNascimento).IsRequired();
            });

            modelBuilder.Entity<Turma>(entity =>
            {
                entity.ToTable("Turmas");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Nome).IsRequired().HasMaxLength(100);
                entity.Property(t => t.AnoLetivo).IsRequired();
                entity.HasOne<Professor>()
                      .WithMany()
                      .HasForeignKey(t => t.ProfessorId);
            });

            modelBuilder.Entity<AlunoTurma>(entity =>
            {
                entity.ToTable("AlunoTurma");
                entity.HasKey(at => at.Id);
                entity.HasOne<Aluno>()
                      .WithMany()
                      .HasForeignKey(at => at.AlunoId);
                entity.HasOne<Turma>()
                      .WithMany()
                      .HasForeignKey(at => at.TurmaId);
            });

            modelBuilder.Entity<Aula>(entity =>
            {
                entity.ToTable("Aulas");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Data).IsRequired();
                entity.HasOne<Turma>()
                      .WithMany()
                      .HasForeignKey(a => a.TurmaId);
            });

            modelBuilder.Entity<Chamada>(entity =>
            {
                entity.ToTable("Chamadas");
                entity.HasKey(c => c.Id);
                entity.HasOne<Aula>()
                      .WithMany()
                      .HasForeignKey(c => c.AulaId);
                entity.HasOne<Aluno>()
                      .WithMany()
                      .HasForeignKey(c => c.AlunoId);
                entity.Property(c => c.StatusPresenca).IsRequired();
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.ToTable("Notas");
                entity.HasKey(n => n.Id);
                entity.HasOne<Aluno>()
                      .WithMany()
                      .HasForeignKey(n => n.AlunoId);
                entity.HasOne<Turma>()
                      .WithMany()
                      .HasForeignKey(n => n.TurmaId);
                entity.Property(n => n.Avaliacao).IsRequired().HasMaxLength(100);
                entity.Property(n => n.ValorNota).IsRequired();
            });
        }
    }
}
