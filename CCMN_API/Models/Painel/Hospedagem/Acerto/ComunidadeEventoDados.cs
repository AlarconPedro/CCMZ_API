using CCMN_API.Models.Painel.Hospedagem.EventoDespesas;

namespace CCMN_API.Models.Painel.Hospedagem.Acerto;

public class ComunidadeEventoDados
{
    public int ComCodigo { get; set; }
    public string? ComNome { get; set; }
    public PessoasPagantesCobrantes? PagantesCobrantes { get; set; }
}
