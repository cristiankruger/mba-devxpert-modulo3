using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.Conteudo.Domain;

public class Curso : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public bool PermitirMatricula { get; private set; }
    public TimeSpan CargaHoraria { get; private set; }
    public ConteudoProgramatico ConteudoProgramatico { get; private set; }
    private readonly List<Aula> _aulas;
    public IReadOnlyCollection<Aula> Aulas => _aulas;

    protected Curso()
    {
        _aulas = [];
    }

    public Curso(string nome, ConteudoProgramatico conteudoProgramatico)
    {
        Nome = nome;
        PermitirMatricula = false;
        ConteudoProgramatico = conteudoProgramatico;
        CargaHoraria = TimeSpan.Zero;
        _aulas = [];
        Ativar();
        Validar();
    }

    public void AlterarNome(string nome)
    {
        Validacoes.ValidarSeVazio(Nome, NomeVazioMsgErro);
        Nome = nome;
    }

    public void DesativarCurso() => Desativar();

    public void AtivarCurso() => Ativar();

    public void AlterarConteudoProgramatico(ConteudoProgramatico conteudoProgramatico)
    {
        ConteudoProgramatico = conteudoProgramatico;
    }

    public void CadastrarAula(Aula aula)
    {
        _aulas.Add(aula);
        CargaHoraria += aula.Duracao;
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, NomeVazioMsgErro);
        Validacoes.ValidarSeMenorQue(CargaHoraria.Ticks, 0, CargaHorariaMenorQueMsgErro);
    }

    //MENSAGENS VALIDACAO
    public const string NomeVazioMsgErro = "O campo Nome do curso não pode estar vazio.";
    public const string CargaHorariaMenorQueMsgErro = "O campo Carga Horária do curso não pode ser menor que 0.";
}