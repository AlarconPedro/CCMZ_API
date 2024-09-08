namespace CCMN_API.Models.Painel.Hospedagem.Pessoas;

public class PessoaEvento
{
    public int PesCodigo { get; set; }
    public string? PesNome { get; set; }
    public string? PesGenero { get; set; }
    public string? Comunidade { get; set; }
    public int EvpCodigo { get; set; }
    public bool? EvpPagante { get; set; }
    public bool? EvpCobrante { get; set; }
}
