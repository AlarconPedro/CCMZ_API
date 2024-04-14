namespace CCMZ_API.Models.Painel.QuartoPessoa;

public class QuartoPessoas
{
    public int? QuaCodigo { get; set; }

    public string? QuaNome { get; set; }

    public string? BloNome { get; set; }

    public int? BloCodigo { get; set; }

    public int? Vagas { get; set; }

    public List<PessoaCheckin>? PessoasQuarto { get; set; }
}
