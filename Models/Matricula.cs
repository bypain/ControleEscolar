namespace ControleEscolar.Models;

public class Matricula
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public Aluno? Aluno { get; set; }
    public Guid DisciplinaId { get; set; }
    public Disciplina? Disciplina { get; set; }
    public Guid ProfessorId { get; set; }
    public Professor? Professor { get; set; }
}
